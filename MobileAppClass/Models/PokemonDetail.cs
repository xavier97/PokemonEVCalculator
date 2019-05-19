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

        [JsonProperty]
        private readonly Dictionary<string, PokemonBattled> PokemonBattleDictionary;

        public PokemonDetail()
        {
            PokemonBattleDictionary = new Dictionary<string, PokemonBattled>
            {
                ["pokemonButton1"] = new PokemonBattled(),
                ["pokemonButton2"] = new PokemonBattled(),
                ["pokemonButton3"] = new PokemonBattled()
            };
        }

        public Dictionary<string, PokemonBattled> GetAllButtons()
        {
            return PokemonBattleDictionary;
        }

        public PokemonBattled GetAButton(int buttonNumber)
        {
            return PokemonBattleDictionary["pokemonButton" + buttonNumber];
        }

        public void SetPokemonBattled(int buttonNumber, PokemonBattled pokemonBattled)
        {
            string buttonString = "pokemonButton" + buttonNumber;
            PokemonBattleDictionary[buttonString] = pokemonBattled;
        }

        public override string ToString() => string.Format("[PokemonDetail: breed={0}, nickname={1}, heldItem={2}, " +
                                 "pokerus={3}, attackEV={4}, defenseEV={5}, spAttackEV={6}, " +
                                 "spDefenseEV={7}, hpEV={8}, speedEV={9}, \n pokemonButton1={10}, " +
                                 "pokemonButton2={11}, pokemonButton3={12}", breed, nickname,
                                 heldItem, pokerus, attackEV, defenseEV, spAttackEV,
                                 spDefenseEV, hpEV, speedEV,
                                 PokemonBattleDictionary["pokemonButton1"] != null ? PokemonBattleDictionary["pokemonButton1"].Name : "null",
                                 PokemonBattleDictionary["pokemonButton2"] != null ? PokemonBattleDictionary["pokemonButton2"].Name : "null",
                                 PokemonBattleDictionary["pokemonButton3"] != null ? PokemonBattleDictionary["pokemonButton3"].Name : "null");
    }
}