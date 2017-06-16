﻿using System;
using System.Collections.Generic;
using System.Linq;

using Narivia.GameLogic.Enumerations;
using Narivia.GameLogic.Events;
using Narivia.GameLogic.GameManagers.Interfaces;
using Narivia.Models;
using Narivia.Models.Enumerations;

namespace Narivia.GameLogic.GameManagers
{
    /// <summary>
    /// Game manager.
    /// </summary>
    public class GameManager : IGameManager
    {
        IWorldManager world;
        IAttackManager attack;

        const int HOLDING_CASTLE_INCOME = 5;
        const int HOLDING_CASTLE_RECRUITMENT = 15;
        const int HOLDING_CITY_INCOME = 15;
        const int HOLDING_CITY_RECRUITMENT = 5;
        const int HOLDING_TEMPLE_INCOME = 10;
        const int HOLDING_TEMPLE_RECRUITMENT = 10;

        /// <summary>
        /// Occurs when a player region is attacked.
        /// </summary>
        public event RegionAttackEventHandler PlayerRegionAttacked;

        /// <summary>
        /// Gets or sets the world tiles.
        /// </summary>
        /// <value>The world tiles.</value>
        public string[,] WorldTiles
        {
            get { return world.WorldTiles; }
            set { world.WorldTiles = value; }
        }

        /// <summary>
        /// Gets the width of the world.
        /// </summary>
        /// <value>The width of the world.</value>
        public int WorldWidth => world.WorldWidth;

        /// <summary>
        /// Gets the height of the world.
        /// </summary>
        /// <value>The height of the world.</value>
        public int WorldHeight => world.WorldHeight;

        /// <summary>
        /// Gets the name of the world.
        /// </summary>
        /// <value>The name of the world.</value>
        public string WorldName => world.WorldName;

        /// <summary>
        /// Gets the world identifier.
        /// </summary>
        /// <value>The world identifier.</value>
        public string WorldId => world.WorldId;

        /// <summary>
        /// Gets the base region income.
        /// </summary>
        /// <value>The base region income.</value>
        public int BaseRegionIncome => world.BaseRegionIncome;

        /// <summary>
        /// Gets the base region recruitment.
        /// </summary>
        /// <value>The base region recruitment.</value>
        public int BaseRegionRecruitment => world.BaseRegionRecruitment;

        /// <summary>
        /// Gets the base faction recruitment.
        /// </summary>
        /// <value>The base faction recruitment.</value>
        public int BaseFactionRecruitment => world.BaseFactionRecruitment;

        /// <summary>
        /// Gets the minimum number of troops required to attack.
        /// </summary>
        /// <value>The minimum troops to attack.</value>
        public int MinTroopsPerAttack => world.MinTroopsPerAttack;

        /// <summary>
        /// Gets the starting wealth.
        /// </summary>
        /// <value>The starting wealth.</value>
        public int StartingWealth => world.StartingWealth;

        /// <summary>
        /// Gets the starting troops per unit.
        /// </summary>
        /// <value>The starting troops per unit.</value>
        public int StartingTroopsPerUnit => world.StartingTroops;

        /// <summary>
        /// Gets the player faction identifier.
        /// </summary>
        /// <value>The player faction identifier.</value>
        public string PlayerFactionId { get; private set; }

        /// <summary>
        /// Gets the turn.
        /// </summary>
        /// <value>The turn.</value>
        public int Turn { get; private set; }

        /// <summary>
        /// Starts a new game.
        /// </summary>
        /// <param name="worldId">World identifier.</param>
        /// <param name="factionId">Faction identifier.</param>
        public void NewGame(string worldId, string factionId)
        {
            world = new WorldManager();
            world.LoadWorld(worldId);

            attack = new AttackManager(world);

            InitializeGame(factionId);
        }

        /// <summary>
        /// Advances the game by one turn.
        /// </summary>
        public void NextTurn()
        {
            foreach (Faction faction in world.Factions.Values.Where(f => f.Alive))
            {
                if (GetFactionRegionsCount(faction.Id) == 0)
                {
                    faction.Alive = false;
                    continue;
                }

                // Economy
                faction.Wealth += GetFactionIncome(faction.Id);
                faction.Wealth -= GetFactionOutcome(faction.Id);

                // Build

                // Recruit
                // TODO: Find a way around the hardcoded "militia" unit identifier
                world.Armies.Values.FirstOrDefault(u => u.FactionId == faction.Id &&
                                                        u.UnitId == "militia")
                                   .Size += GetFactionRecruitment(faction.Id);

                // A.I.
                if (faction.Id == PlayerFactionId ||
                    GetFactionTroopsCount(faction.Id) < MinTroopsPerAttack)
                {
                    continue;
                }

                string regionId = attack.ChooseRegionToAttack(faction.Id);
                string regionFactionId = world.Regions[regionId].FactionId;
                BattleResult result = attack.AttackRegion(faction.Id, regionId);

                if (regionFactionId == PlayerFactionId)
                {
                    RegionAttackEventArgs e = new RegionAttackEventArgs(regionId, faction.Id, result);

                    if (PlayerRegionAttacked != null)
                    {
                        PlayerRegionAttacked(this, e);
                    }
                }
            }

            Turn += 1;
        }

        /// <summary>
        /// Checks wether the specified regions share a border.
        /// </summary>
        /// <returns><c>true</c>, if the specified regions share a border, <c>false</c> otherwise.</returns>
        /// <param name="sourceRegionId">Source region identifier.</param>
        /// <param name="targetRegionId">Target region identifier.</param>
        public bool RegionBordersRegion(string sourceRegionId, string targetRegionId)
        => world.RegionBordersRegion(sourceRegionId, targetRegionId);

        /// <summary>
        /// Checks wether the specified factions share a border.
        /// </summary>
        /// <returns><c>true</c>, if the specified factions share a border, <c>false</c> otherwise.</returns>
        /// <param name="sourceFactionId">Source faction identifier.</param>
        /// <param name="targetFactionId">Target faction identifier.</param>
        public bool FactionBordersFaction(string sourceFactionId, string targetFactionId)
        => world.FactionBordersFaction(sourceFactionId, targetFactionId);

        /// <summary>
        /// Checks wether the specified faction shares a border with the specified region.
        /// </summary>
        /// <returns><c>true</c>, if the faction share a border with that region, <c>false</c> otherwise.</returns>
        /// <param name="factionId">Faction identifier.</param>
        /// <param name="regionId">Region identifier.</param>
        public bool FactionBordersRegion(string factionId, string regionId)
        => world.FactionBordersRegion(factionId, regionId);

        /// <summary>
        /// Returns the faction identifier at the given position.
        /// </summary>
        /// <returns>The faction identifier.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public string FactionIdAtPosition(int x, int y)
        => world.FactionIdAtPosition(x, y);

        /// <summary>
        /// Transfers the specified region to the specified faction.
        /// </summary>
        /// <param name="regionId">Region identifier.</param>
        /// <param name="factionId">Faction identifier.</param>
        public void TransferRegion(string regionId, string factionId)
        => world.TransferRegion(regionId, factionId);

        /// <summary>
        /// Gets the faction army size.
        /// </summary>
        /// <returns>The faction army size.</returns>
        /// <param name="factionId">Faction identifier.</param>
        /// <param name="unitId">Unit identifier.</param>
        public int GetFactionArmySize(string factionId, string unitId)
        => world.Armies.Values.FirstOrDefault(a => a.FactionId == factionId && a.UnitId == unitId).Size;

        /// <summary>
        /// Gets the culture of a faction.
        /// </summary>
        /// <returns>The culture.</returns>
        /// <param name="factionId">Faction identifier.</param>
        public Culture GetFactionCulture(string factionId)
        => world.Cultures[world.Factions[factionId].CultureId];

        /// <summary>
        /// Gets the name of the faction.
        /// </summary>
        /// <returns>The faction name.</returns>
        /// <param name="factionId">Faction identifier.</param>
        public string GetFactionName(string factionId)
        => world.Factions[factionId].Name;

        /// <summary>
        /// Returns the colour of a faction.
        /// </summary>
        /// <returns>The colour.</returns>
        /// <param name="factionId">Faction identifier.</param>
        public Colour GetFactionColour(string factionId)
        => world.Factions[factionId].Colour;

        /// <summary>
        /// Gets the faction income.
        /// </summary>
        /// <returns>The faction income.</returns>
        /// <param name="factionId">Faction identifier.</param>
        public int GetFactionIncome(string factionId)
        {
            int income = 0;

            income += GetFactionRegionsCount(factionId) * BaseRegionIncome;
            income += world.GetFactionHoldings(factionId).Count(h => h.Type == HoldingType.Castle) * HOLDING_CASTLE_INCOME;
            income += world.GetFactionHoldings(factionId).Count(h => h.Type == HoldingType.City) * HOLDING_CITY_INCOME;
            income += world.GetFactionHoldings(factionId).Count(h => h.Type == HoldingType.Temple) * HOLDING_TEMPLE_INCOME;

            return income;
        }

        /// <summary>
        /// Gets the faction outcome.
        /// </summary>
        /// <returns>The faction outcome.</returns>
        /// <param name="factionId">Faction identifier.</param>
        public int GetFactionOutcome(string factionId)
        {
            int outcome = 0;

            outcome += world.Armies.Values.Where(x => x.FactionId == factionId)
                                         .Sum(x => x.Size * world.Units[x.UnitId].Maintenance);

            return outcome;
        }

        /// <summary>
        /// Gets the faction recruitment.
        /// </summary>
        /// <returns>The faction recruitment.</returns>
        /// <param name="factionId">Faction identifier.</param>
        public int GetFactionRecruitment(string factionId)
        {
            int recruitment = 0;

            recruitment += world.Regions.Values.Count(r => r.FactionId == factionId) * BaseRegionRecruitment;
            recruitment += BaseFactionRecruitment;
            // TODO: Also calculate the holdings recruitment

            return recruitment;
        }

        /// <summary>
        /// Gets the regions count of a faction.
        /// </summary>
        /// <returns>The number of regions.</returns>
        /// <param name="factionId">Faction identifier.</param>
        public int GetFactionRegionsCount(string factionId)
        => world.GetFactionRegions(factionId).ToList().Count;

        /// <summary>
        /// Gets the holdings count of a faction.
        /// </summary>
        /// <returns>The number of holdings.</returns>
        /// <param name="factionId">Faction identifier.</param>
        public int GetFactionHoldingsCount(string factionId)
        => world.GetFactionHoldings(factionId).ToList().Count;

        /// <summary>
        /// Gets the faction wealth.
        /// </summary>
        /// <returns>The faction wealth.</returns>
        /// <param name="factionId">Faction identifier.</param>
        public int GetFactionWealth(string factionId)
        => world.Factions[factionId].Wealth;

        /// <summary>
        /// Gets the faction troops count.
        /// </summary>
        /// <returns>The faction troops count.</returns>
        /// <param name="factionId">Faction identifier.</param>
        public int GetFactionTroopsCount(string factionId)
        => world.GetFactionTroopsCount(factionId);

        /// <summary>
        /// Gets the faction capital.
        /// </summary>
        /// <returns>The faction capital.</returns>
        /// <param name="factionId">Faction identifier.</param>
        public string GetFactionCapital(string factionId)
        => world.GetFactionCapital(factionId);

        /// <summary>
        /// Gets the faction idenfifier of a region.
        /// </summary>
        /// <returns>The faction identifier.</returns>
        /// <param name="regionId">Region identifier.</param>
        public string GetRegionFaction(string regionId)
        => world.Regions[regionId].FactionId;

        /// <summary>
        /// Gets the name of a region.
        /// </summary>
        /// <returns>The name.</returns>
        /// <param name="regionId">Region identifier.</param>
        public string GetRegionName(string regionId)
        => world.Regions[regionId].Name;

        /// <summary>
        /// Gets the factions.
        /// </summary>
        /// <returns>The factions.</returns>
        public IEnumerable<Faction> GetFactions()
        => world.Factions.Values;

        /// <summary>
        /// Gets the regions of a faction.
        /// </summary>
        /// <returns>The regions.</returns>
        /// <param name="factionId">Faction identifier.</param>
        public IEnumerable<Region> GetFactionRegions(string factionId)
        => world.GetFactionRegions(factionId);

        /// <summary>
        /// Gets the holdings of a faction.
        /// </summary>
        /// <returns>The holdings.</returns>
        /// <param name="factionId">Faction identifier.</param>
        public IEnumerable<Holding> GetFactionHoldings(string factionId)
        => world.GetFactionHoldings(factionId);

        /// <summary>
        /// Gets the holdings of a region.
        /// </summary>
        /// <returns>The holdings.</returns>
        /// <param name="regionId">Region identifier.</param>
        public IEnumerable<Holding> GetRegionHoldings(string regionId)
        => world.GetRegionHoldings(regionId);

        /// <summary>
        /// Gets the units.
        /// </summary>
        /// <returns>The units.</returns>
        public IEnumerable<Unit> GetUnits()
        => world.Units.Values;

        /// <summary>
        /// Adds the specified amount of troops of a unit for a faction.
        /// </summary>
        /// <param name="factionId">Faction identifier.</param>
        /// <param name="unitId">Unit identifier.</param>
        /// <param name="amount">Amount.</param>
        public void AddUnits(string factionId, string unitId, int amount)
        {
            world.Armies.Values.FirstOrDefault(a => a.FactionId == factionId &&
                                                    a.UnitId == unitId).Size += amount;
        }

        /// <summary>
        /// Recruits the specified amount of troops of a unit for a faction.
        /// </summary>
        /// <param name="factionId">Faction identifier.</param>
        /// <param name="unitId">Unit identifier.</param>
        /// <param name="amount">Amount.</param>
        public void RecruitUnits(string factionId, string unitId, int amount)
        {
            int costPerTroop = world.Units[unitId].Price;
            int factionWealth = world.Factions[factionId].Wealth;

            if (factionWealth < costPerTroop * amount)
            {
                // TODO: Maybe log a warning or something
                amount = factionWealth / costPerTroop;
            }

            AddUnits(factionId, unitId, amount);
        }

        /// <summary>
        /// The player faction will attack the specified region.
        /// </summary>
        /// <param name="regionId">Region identifier.</param>
        public BattleResult PlayerAttackRegion(string regionId)
        {
            BattleResult result = attack.AttackRegion(PlayerFactionId, regionId);

            NextTurn();

            return result;
        }

        void InitializeGame(string factionId)
        {
            PlayerFactionId = factionId;
            Turn = 0;
        }

        /// <summary>
        /// Changes the relations between two factions.
        /// </summary>
        /// <param name="sourceFactionId">Source faction identifier.</param>
        /// <param name="targetFactionId">Target faction identifier.</param>
        /// <param name="delta">Relations value delta.</param>
        void ChangeRelations(string sourceFactionId, string targetFactionId, int delta)
        {
            Relation sourceRelation = world.Relations.Values.FirstOrDefault(r => r.SourceFactionId == sourceFactionId &&
                                                                                 r.TargetFactionId == targetFactionId);
            Relation targetRelation = world.Relations.Values.FirstOrDefault(r => r.SourceFactionId == targetFactionId &&
                                                                                 r.TargetFactionId == sourceFactionId);

            int oldRelations = sourceRelation.Value;
            sourceRelation.Value = Math.Max(-100, Math.Min(sourceRelation.Value + delta, 100));
            targetRelation.Value = sourceRelation.Value;
        }
    }
}
