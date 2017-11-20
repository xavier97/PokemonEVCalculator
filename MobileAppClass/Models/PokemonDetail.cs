using System;
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

        public PokemonDetail()
        {
        }

        // Testing
        public override string ToString()
        {
            return string.Format("[PokemonDetail: breed={0}, nickname={1}, heldItem={2}, " +
                                 "pokerus={3}, attackEV={4}, defenseEV={5}, spAttackEV={6}, " +
                                 "spDefenseEV={7}, hpEV={8}, speedEV={9}]", breed, nickname, 
                                 heldItem, pokerus, attackEV, defenseEV, spAttackEV, 
                                 spDefenseEV, hpEV, speedEV);
        }
    }
}
