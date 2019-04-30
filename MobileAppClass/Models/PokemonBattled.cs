using System;
namespace MobileAppClass
{
    /// <summary>
    /// Records the EV yields of a Pokemon that was battled against
    /// </summary>
    public class PokemonBattled
    {
        public String name { get; set; }
        public int attackEV { get; set; }
        public int defenseEV { get; set; }
        public int spAttackEV { get; set; }
        public int spDefenseEV { get; set; }
        public int hpEV { get; set; }
        public int speedEV { get; set; }

        public PokemonBattled()
        {
        }
    }
}
