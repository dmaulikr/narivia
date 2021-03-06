using System.Collections.Generic;
using System.Linq;

using Narivia.DataAccess.DataObjects;
using Narivia.DataAccess.Exceptions;
using Narivia.DataAccess.Repositories.Interfaces;

namespace Narivia.DataAccess.Repositories
{
    /// <summary>
    /// Border repository implementation.
    /// </summary>
    public class BorderRepository : IBorderRepository
    {
        readonly XmlDatabase<BorderEntity> xmlDatabase;

        /// <summary>
        /// Initializes a new instance of the <see cref="BorderRepository"/> class.
        /// </summary>
        public BorderRepository(string fileName)
        {
            xmlDatabase = new XmlDatabase<BorderEntity>(fileName);
        }

        /// <summary>
        /// Adds the specified border.
        /// </summary>
        /// <param name="borderEntity">Border.</param>
        public void Add(BorderEntity borderEntity)
        {
            List<BorderEntity> borderEntities = xmlDatabase.LoadEntities().ToList();
            borderEntities.Add(borderEntity);

            try
            {
                xmlDatabase.SaveEntities(borderEntities);
            }
            catch
            {
                throw new DuplicateEntityException($"{borderEntity.Province1Id}-{borderEntity.Province2Id}", nameof(BorderEntity).Replace("Entity", ""));
            }
        }

        /// <summary>
        /// Gets the border with the specified faction and unit identifiers.
        /// </summary>
        /// <returns>The border.</returns>
        /// <param name="province1Id">First province identifier.</param>
        /// <param name="province2Id">Second province identifier.</param>
        public BorderEntity Get(string province1Id, string province2Id)
        {
            List<BorderEntity> borderEntities = xmlDatabase.LoadEntities().ToList();
            BorderEntity borderEntity = borderEntities.FirstOrDefault(x => x.Province1Id == province1Id &&
                                                                           x.Province2Id == province2Id);

            if (borderEntity == null)
            {
                throw new EntityNotFoundException($"{borderEntity.Province1Id}-{borderEntity.Province2Id}", nameof(BorderEntity).Replace("Entity", ""));
            }

            return borderEntity;
        }

        /// <summary>
        /// Gets all the borders.
        /// </summary>
        /// <returns>The borders.</returns>
        public IEnumerable<BorderEntity> GetAll()
        {
            List<BorderEntity> borderEntities = xmlDatabase.LoadEntities().ToList();

            return borderEntities;
        }

        /// <summary>
        /// Removes the border with the specified faction and unit identifiers.
        /// </summary>
        /// <param name="province1Id">First province identifier.</param>
        /// <param name="province2Id">Second province identifier.</param>
        public void Remove(string province1Id, string province2Id)
        {
            List<BorderEntity> borderEntities = xmlDatabase.LoadEntities().ToList();
            borderEntities.RemoveAll(x => x.Province1Id == province1Id &&
                                         x.Province2Id == province2Id);

            try
            {
                xmlDatabase.SaveEntities(borderEntities);
            }
            catch
            {
                throw new DuplicateEntityException($"{province1Id}-{province2Id}", nameof(BorderEntity).Replace("Entity", ""));
            }
        }
    }
}
