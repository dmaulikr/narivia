﻿using System;
using System.Collections.Generic;

using Narivia.Models;

namespace Narivia.BusinessLogic.GameManagers.Interfaces
{
    interface IWorldManager
    {
        /// <summary>
        /// Gets or sets the biomes.
        /// </summary>
        /// <value>The biomes.</value>
        Dictionary<string, Biome> Biomes { get; set; }

        /// <summary>
        /// Gets or sets the cultures.
        /// </summary>
        /// <value>The cultures.</value>
        Dictionary<string, Culture> Cultures { get; set; }

        /// <summary>
        /// Gets or sets the factions.
        /// </summary>
        /// <value>The factions.</value>
        Dictionary<string, Faction> Factions { get; set; }

        /// <summary>
        /// Gets or sets the holdings.
        /// </summary>
        /// <value>The holdings.</value>
        Dictionary<string, Holding> Holdings { get; set; }

        /// <summary>
        /// Gets or sets the regions.
        /// </summary>
        /// <value>The regions.</value>
        Dictionary<string, Region> Regions { get; set; }

        /// <summary>
        /// Gets or sets the resources.
        /// </summary>
        /// <value>The resources.</value>
        Dictionary<string, Resource> Resources { get; set; }

        /// <summary>
        /// Gets or sets the units.
        /// </summary>
        /// <value>The units.</value>
        Dictionary<string, Unit> Units { get; set; }

        /// <summary>
        /// Gets or sets the armies.
        /// </summary>
        /// <value>The armies.</value>
        Dictionary<Tuple<string, string>, Army> Armies { get; set; }

        /// <summary>
        /// Gets or sets the borders.
        /// </summary>
        /// <value>The borders.</value>
        Dictionary<Tuple<string, string>, Border> Borders { get; set; }

        /// <summary>
        /// Gets or sets the relations.
        /// </summary>
        /// <value>The relations.</value>
        Dictionary<Tuple<string, string>, Relation> Relations { get; set; }

        /// <summary>
        /// Gets or sets the world tiles.
        /// </summary>
        /// <value>The world tiles.</value>
        string[,] WorldTiles { get; set; }

        /// <summary>
        /// Gets the width of the world.
        /// </summary>
        /// <value>The width of the world.</value>
        int WorldWidth { get; }

        /// <summary>
        /// Gets the height of the world.
        /// </summary>
        /// <value>The height of the world.</value>
        int WorldHeight { get; }

        /// <summary>
        /// Gets the name of the world.
        /// </summary>
        /// <value>The name of the world.</value>
        string WorldName { get; }

        /// <summary>
        /// Gets the world identifier.
        /// </summary>
        /// <value>The world identifier.</value>
        string WorldId { get; }

        /// <summary>
        /// Gets or sets the base region income.
        /// </summary>
        /// <value>The base region income.</value>
        int BaseRegionIncome { get; set; }

        /// <summary>
        /// Gets or sets the base region recruitment.
        /// </summary>
        /// <value>The base region recruitment.</value>
        int BaseRegionRecruitment { get; set; }

        /// <summary>
        /// Gets or sets the base faction recruitment.
        /// </summary>
        /// <value>The base faction recruitment.</value>
        int BaseFactionRecruitment { get; set; }

        /// <summary>
        /// Gets or sets the starting wealth.
        /// </summary>
        /// <value>The starting wealth.</value>
        int StartingWealth { get; set; }

        /// <summary>
        /// Gets or sets the starting troops.
        /// </summary>
        /// <value>The starting troops.</value>
        int StartingTroops { get; set; }

        /// <summary>
        /// Loads the world.
        /// </summary>
        /// <param name="worldId">World identifier.</param>
        void LoadWorld(string worldId);

        /// <summary>
        /// Checks wether the specified regions share a border.
        /// </summary>
        /// <returns><c>true</c>, if the specified regions share a border, <c>false</c> otherwise.</returns>
        /// <param name="region1Id">First region identifier.</param>
        /// <param name="region2Id">Second region identifier.</param>
        bool RegionHasBorder(string region1Id, string region2Id);

        /// <summary>
        /// Checks wether the specified regions share a border.
        /// </summary>
        /// <returns><c>true</c>, if the specified regions share a border, <c>false</c> otherwise.</returns>
        /// <param name="faction1Id">First faction identifier.</param>
        /// <param name="faction2Id">Second faction identifier.</param>
        bool FactionHasBorder(string faction1Id, string faction2Id);

        /// <summary>
        /// Returns the faction identifier at the given position.
        /// </summary>
        /// <returns>The faction identifier.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        string FactionIdAtPosition(int x, int y);

        /// <summary>
        /// Transfers the specified region to the specified faction.
        /// </summary>
        /// <param name="regionId">Region identifier.</param>
        /// <param name="factionId">Faction identifier.</param>
        void TransferRegion(string regionId, string factionId);

        /// <summary>
        /// Gets the faction capital.
        /// </summary>
        /// <returns>Region identifier.</returns>
        /// <param name="factionId">Faction identifier.</param>
        string GetFactionCapital(string factionId);
    }
}