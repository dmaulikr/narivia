using System.Collections.Generic;

using Narivia.Models;
using Narivia.Models.Enumerations;

namespace Narivia.GameLogic.GameManagers.Interfaces
{
    public interface IWorldManager
    {
        /// <summary>
        /// Loads the world.
        /// </summary>
        /// <param name="worldId">World identifier.</param>
        void LoadWorld(string worldId);

        /// <summary>
        /// Checks wether the specified provinces share a border.
        /// </summary>
        /// <returns><c>true</c>, if the specified provinces share a border, <c>false</c> otherwise.</returns>
        /// <param name="province1Id">First province identifier.</param>
        /// <param name="province2Id">Second province identifier.</param>
        bool ProvinceBordersProvince(string province1Id, string province2Id);

        /// <summary>
        /// Checks wether a province has empty holding slots.
        /// </summary>
        /// <returns><c>true</c>, if the province has empty holding slots, <c>false</c> otherwise.</returns>
        /// <param name="provinceId">Province identifier.</param>
        bool ProvinceHasEmptyHoldingSlots(string provinceId);

        /// <summary>
        /// Checks wether the specified provinces share a border.
        /// </summary>
        /// <returns><c>true</c>, if the specified provinces share a border, <c>false</c> otherwise.</returns>
        /// <param name="faction1Id">First faction identifier.</param>
        /// <param name="faction2Id">Second faction identifier.</param>
        bool FactionBordersFaction(string faction1Id, string faction2Id);

        /// <summary>
        /// Checks wether the specified faction shares a border with the specified province.
        /// </summary>
        /// <returns><c>true</c>, if the faction share a border with that province, <c>false</c> otherwise.</returns>
        /// <param name="factionId">Faction identifier.</param>
        /// <param name="provinceId">Province identifier.</param>
        bool FactionBordersProvince(string factionId, string provinceId);

        /// <summary>
        /// Returns the faction identifier at the given location.
        /// </summary>
        /// <returns>The faction identifier.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        string FactionIdAtLocation(int x, int y);

        /// <summary>
        /// Transfers the specified province to the specified faction.
        /// </summary>
        /// <param name="provinceId">Province identifier.</param>
        /// <param name="factionId">Faction identifier.</param>
        void TransferProvince(string provinceId, string factionId);

        /// <summary>
        /// Gets the armies.
        /// </summary>
        /// <returns>The armies.</returns>
        IEnumerable<Army> GetArmies();

        /// <summary>
        /// Gets the biomes.
        /// </summary>
        /// <returns>The biomes.</returns>
        IEnumerable<Biome> GetBiomes();

        /// <summary>
        /// Gets the borders.
        /// </summary>
        /// <returns>The borders.</returns>
        IEnumerable<Border> GetBorders();

        /// <summary>
        /// Gets the cultures.
        /// </summary>
        /// <returns>The cultures.</returns>
        IEnumerable<Culture> GetCultures();

        /// <summary>
        /// Gets the factions.
        /// </summary>
        /// <returns>The factions.</returns>
        IEnumerable<Faction> GetFactions();

        /// <summary>
        /// Gets the faction troops amount.
        /// </summary>
        /// <returns>The faction troops amount.</returns>
        /// <param name="factionId">Faction identifier.</param>
        int GetFactionTroopsAmount(string factionId);

        /// <summary>
        /// Gets the faction capital.
        /// </summary>
        /// <returns>Province.</returns>
        /// <param name="factionId">Faction identifier.</param>
        Province GetFactionCapital(string factionId);

        /// <summary>
        /// Gets or sets the X map coordinate of the centre of the faction territoriy.
        /// </summary>
        /// <value>The X coordinate.</value>
        /// <param name="factionId">Faction identifier.</param>
        int GetFactionCentreX(string factionId);

        /// <summary>
        /// Gets or sets the Y map coordinate of the centre of the faction territoriy.
        /// </summary>
        /// <value>The Y coordinate.</value>
        /// <param name="factionId">Faction identifier.</param>
        int GetFactionCentreY(string factionId);
        
        /// <summary>
        /// Gets the armies of a faction.
        /// </summary>
        /// <returns>The armies.</returns>
        /// <param name="factionId">Faction identifier.</param>
        IEnumerable<Army> GetFactionArmies(string factionId);

        /// <summary>
        /// Gets the holdings of a faction.
        /// </summary>
        /// <returns>The holdings.</returns>
        /// <param name="factionId">Faction identifier.</param>
        IEnumerable<Holding> GetFactionHoldings(string factionId);

        /// <summary>
        /// Gets the provinces of a faction.
        /// </summary>
        /// <returns>The provinces.</returns>
        /// <param name="factionId">Faction identifier.</param>
        IEnumerable<Province> GetFactionProvinces(string factionId);

        /// <summary>
        /// Gets the relations of a faction.
        /// </summary>
        /// <returns>The relations of a faction.</returns>
        /// <param name="factionId">Faction identifier.</param>
        IEnumerable<Relation> GetFactionRelations(string factionId);

        /// <summary>
        /// Gets the flags.
        /// </summary>
        /// <returns>The flags.</returns>
        IEnumerable<Flag> GetFlags();

        /// <summary>
        /// Gets the holdings.
        /// </summary>
        /// <returns>The holdings.</returns>
        IEnumerable<Holding> GetHoldings();

        /// <summary>
        /// Gets the provinces.
        /// </summary>
        /// <returns>The provinces.</returns>
        IEnumerable<Province> GetProvinces();

        /// <summary>
        /// Gets the province holdings.
        /// </summary>
        /// <returns>The province holdings.</returns>
        /// <param name="provinceId">Province identifier.</param>
        IEnumerable<Holding> GetProvinceHoldings(string provinceId);

        /// <summary>
        /// Gets the relations.
        /// </summary>
        /// <returns>The relations.</returns>
        IEnumerable<Relation> GetRelations();

        /// <summary>
        /// Gets the resources.
        /// </summary>
        /// <returns>The resources.</returns>
        IEnumerable<Resource> GetResources();

        /// <summary>
        /// Gets the units.
        /// </summary>
        /// <returns>The units.</returns>
        IEnumerable<Unit> GetUnits();

        /// <summary>
        /// Gets the world.
        /// </summary>
        /// <returns>The world.</returns>
        World GetWorld();

        /// <summary>
        /// Adds the specified holding type in a province.
        /// </summary>
        /// <param name="provinceId">Province identifier.</param>
        /// <param name="holdingType">Holding type.</param>
        void AddHolding(string provinceId, HoldingType holdingType);

        /// <summary>
        /// Changes the relations between two factions.
        /// </summary>
        /// <param name="sourceFactionId">Source faction identifier.</param>
        /// <param name="targetFactionId">Target faction identifier.</param>
        /// <param name="delta">Relations value delta.</param>
        void ChangeRelations(string sourceFactionId, string targetFactionId, int delta);

        /// <summary>
        /// Sets the relations between two factions.
        /// </summary>
        /// <param name="sourceFactionId">Source faction identifier.</param>
        /// <param name="targetFactionId">Target faction identifier.</param>
        /// <param name="value">Relations value.</param>
        void SetRelations(string sourceFactionId, string targetFactionId, int value);
    }
}
