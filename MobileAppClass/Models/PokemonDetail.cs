using System;
using System.Collections.Generic;
using MobileAppClass;
using Newtonsoft.Json;
namespace PKMNEVCalc
{
    /// <summary>
    /// Keeps a record of an individual Pokémon's data which we will save.
    /// This includes its breed, nickname, held item, Pokérus status, and EVs (v1.0).
    /// </summary>
    public class PokemonDetail
    {
        public string breed { get; set; }
        public string nickname { get; set; }
        public string heldItem { get; set; }
        public bool pokerus { get; set; }

        // EV Stats
        public int attackEV { get; set; }
        public int defenseEV { get; set; }
        public int spAttackEV { get; set; }
        public int spDefenseEV { get; set; }
        public int hpEV { get; set; }
        public int speedEV { get; set; }

        // Pokemon battling
        [JsonProperty]
        private readonly Dictionary<string, PokemonBattled> pokemonBattleDictionary =
            new Dictionary<string, PokemonBattled>
            {
                ["pokemonButton1"] = null,
                ["pokemonButton2"] = null,
                ["pokemonButton3"] = null
            };

        /// <summary>
        /// Gets the buttons.
        /// </summary>
        /// <returns>The pokemonBattleDictionary.</returns>
        public Dictionary<string, PokemonBattled> GetAllButtons()
        {
            return pokemonBattleDictionary;
        }

        /// <summary>
        /// Gets the "pokemon that is being battled"'s button.
        /// </summary>
        /// <returns>The pokemon battled.</returns>
        /// <param name="buttonNumber">Button number.</param>
        public PokemonBattled GetAButton(int buttonNumber)
        {
            return pokemonBattleDictionary["pokemonButton" + buttonNumber];
        }

        /// <summary>
        /// Sets the "pokemon that is being battled"'s button.
        /// </summary>
        /// <param name="buttonNumber">Button number.</param>
        /// <param name="pokemonBattled">Pokemon battled.</param>
        public void SetPokemonBattled(int buttonNumber, PokemonBattled pokemonBattled)
        {
            string buttonString = "pokemonButton" + buttonNumber;
            pokemonBattleDictionary[buttonString] = pokemonBattled;
        }

        // Testing
        public override string ToString() => string.Format("[PokemonDetail: breed={0}, nickname={1}, heldItem={2}, " +
                                 "pokerus={3}, attackEV={4}, defenseEV={5}, spAttackEV={6}, " +
                                 "spDefenseEV={7}, hpEV={8}, speedEV={9}, /n pokemonButton1={10}, " +
                                 "pokemonButton2={11}, pokemonButton3={12}", breed, nickname,
                                 heldItem, pokerus, attackEV, defenseEV, spAttackEV,
                                 spDefenseEV, hpEV, speedEV,
                                 pokemonBattleDictionary["pokemonButton1"] != null ? pokemonBattleDictionary["pokemonButton1"].Name : "null",
                                 pokemonBattleDictionary["pokemonButton2"] != null ? pokemonBattleDictionary["pokemonButton2"].Name : "null",
                                 pokemonBattleDictionary["pokemonButton3"] != null ? pokemonBattleDictionary["pokemonButton3"].Name : "null");
    }
}