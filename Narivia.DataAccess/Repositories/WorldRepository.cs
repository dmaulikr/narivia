using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

using TiledSharp;

using Narivia.Common.Extensions;
using Narivia.DataAccess.DataObjects;
using Narivia.DataAccess.Exceptions;
using Narivia.DataAccess.IO;
using Narivia.DataAccess.Repositories.Interfaces;

namespace Narivia.DataAccess.Repositories
{
    /// <summary>
    /// World repository implementation.
    /// </summary>
    public class WorldRepository : IWorldRepository
    {
        readonly string worldsDirectory;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldRepository"/> class.
        /// </summary>
        /// <param name="worldsDirectory">File name.</param>
        public WorldRepository(string worldsDirectory)
        {
            this.worldsDirectory = worldsDirectory;
        }

        /// <summary>
        /// Adds the specified world.
        /// </summary>
        /// <param name="worldEntity">World.</param>
        public void Add(WorldEntity worldEntity)
        {
            // TODO: Implement this
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the world with the specified identifier.
        /// </summary>
        /// <returns>The world.</returns>
        /// <param name="id">Identifier.</param>
        public WorldEntity Get(string id)
        {
            WorldEntity worldEntity;
            string worldFile = Path.Combine(worldsDirectory, id, "world.xml");

            using (TextReader reader = new StreamReader(worldFile))
            {
                XmlSerializer xml = new XmlSerializer(typeof(WorldEntity));
                worldEntity = (WorldEntity)xml.Deserialize(reader);
            }

            worldEntity.Tiles = LoadWorldTiles(id);
            worldEntity.Layers = LoadWorldGeoLayers(id);
            
            return worldEntity;
        }

        /// <summary>
        /// Gets all the worlds.
        /// </summary>
        /// <returns>The worlds</returns>
        public IEnumerable<WorldEntity> GetAll()
        {
            ConcurrentBag<WorldEntity> worldEntities = new ConcurrentBag<WorldEntity>();

            Parallel.ForEach(Directory.GetDirectories(worldsDirectory),
                             worldId => worldEntities.Add(Get(worldId)));

            return worldEntities;
        }

        /// <summary>
        /// Updates the specified world.
        /// </summary>
        /// <param name="worldEntity">World.</param>
        public void Update(WorldEntity worldEntity)
        {
            string worldFile = Path.Combine(worldsDirectory, worldEntity.Id, "world.xml");

            using (TextWriter writer = new StreamWriter(worldFile))
            {
                XmlSerializer xml = new XmlSerializer(typeof(WorldEntity));
                xml.Serialize(writer, worldEntity);
            }

            // TODO: Save the ProvinceMap and BiomeMap as well
        }

        /// <summary>
        /// Removes the world with the specified identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public void Remove(string id)
        {
            Directory.Delete(Path.Combine(worldsDirectory, id));
        }

        WorldTileEntity[,] LoadWorldTiles(string worldId)
        {
            ConcurrentDictionary<int, string> provinceColourIds = new ConcurrentDictionary<int, string>();
            ConcurrentDictionary<int, string> biomeColourIds = new ConcurrentDictionary<int, string>();

            IBiomeRepository biomeRepository = new BiomeRepository(Path.Combine(worldsDirectory, worldId, "biomes.xml"));
            IProvinceRepository provinceRepository = new ProvinceRepository(Path.Combine(worldsDirectory, worldId, "provinces.xml"));

            Parallel.ForEach(biomeRepository.GetAll(), b => biomeColourIds.AddOrUpdate(ColorTranslator.FromHtml(b.ColourHexadecimal).ToArgb(), b.Id));
            Parallel.ForEach(provinceRepository.GetAll(), r => provinceColourIds.AddOrUpdate(ColorTranslator.FromHtml(r.ColourHexadecimal).ToArgb(), r.Id));

            FastBitmap biomeBitmap = new FastBitmap(Path.Combine(worldsDirectory, worldId, "biomes_map.png"));
            FastBitmap provinceBitmap = new FastBitmap(Path.Combine(worldsDirectory, worldId, "map.png"));

            Point worldSize = new Point(Math.Max(biomeBitmap.Width, provinceBitmap.Width),
                                        Math.Max(biomeBitmap.Height, provinceBitmap.Height));

            WorldTileEntity[,] tiles = new WorldTileEntity[provinceBitmap.Width, provinceBitmap.Height];

            Parallel.For(0, worldSize.Y, y => Parallel.For(0, worldSize.X, x => tiles[x, y] = new WorldTileEntity()));

            biomeBitmap.LockBits();
            provinceBitmap.LockBits();

            Parallel.For(0, biomeBitmap.Height, y => Parallel.For(0, biomeBitmap.Width, x =>
            {
                int argb = biomeBitmap.GetPixel(x, y).ToArgb();
                tiles[x, y].BiomeId = biomeColourIds[argb];
            }));
            
            Parallel.For(0, provinceBitmap.Height, y => Parallel.For(0, provinceBitmap.Width, x =>
            {
                int argb = provinceBitmap.GetPixel(x, y).ToArgb();
                tiles[x, y].ProvinceId = provinceColourIds[argb];
            }));
            
            biomeBitmap.Dispose();
            provinceBitmap.Dispose();

            return tiles;
        }

        List<WorldGeoLayerEntity> LoadWorldGeoLayers(string worldId)
        {
            TmxMap tmxMap = new TmxMap(Path.Combine(worldsDirectory, worldId, "world.tmx"));
            ConcurrentBag<WorldGeoLayerEntity> layers = new ConcurrentBag<WorldGeoLayerEntity>();

            Parallel.ForEach(tmxMap.Layers, tmxLayer => layers.Add(ProcessTmxLayer(tmxMap, tmxLayer)));

            return layers.OrderBy(l => tmxMap.Layers.IndexOf(tmxMap.Layers.FirstOrDefault(x => x.Name == l.Name)))
                         .ToList();
        }

        WorldGeoLayerEntity ProcessTmxLayer(TmxMap tmxMap, TmxLayer tmxLayer)
        {
            string tilesetName = tmxLayer.Properties["tileset"];

            if (string.IsNullOrWhiteSpace(tilesetName) ||
                tmxMap.Tilesets.ToList().FindIndex(t => t.Name == tilesetName) < 0)
            {
                throw new InvalidEntityFieldException(nameof(WorldGeoLayerEntity.Tileset), tmxLayer.Name, nameof(WorldGeoLayerEntity));
            }

            TmxTileset tmxTileset = tmxMap.Tilesets[tilesetName];

            WorldGeoLayerEntity layer = new WorldGeoLayerEntity
            {
                Name = tmxLayer.Name,
                Tiles = new int[tmxMap.Width, tmxMap.Height],
                Tileset = tmxTileset.Name,
                Opacity = (float)tmxLayer.Opacity,
                Visible = tmxLayer.Visible
            };

            Parallel.ForEach(tmxLayer.Tiles, tile => layer.Tiles[tile.X, tile.Y] = Math.Max(-1, tile.Gid - tmxTileset.FirstGid));

            return layer;
        }
    }
}
