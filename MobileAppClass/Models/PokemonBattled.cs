using System;
namespace MobileAppClass
{
    /// <summary>
    /// Records the EV yields of a Pokemon that was battled against
    /// </summary>
    public class PokemonBattled
    {
        public string Name { get; }
        public int AttackEV { get; }
        public int DefenseEV { get; }
        public int SpAttackEV { get; }
        public int SpDefenseEV { get; }
        public int HpEV { get; }
        public int SpeedEV { get; }

        public PokemonBattled(string name, int attack, int defense, int spAttack, int spDefense, int hp, int speed)
        {
            this.Name = name;
            this.AttackEV = attack;
            this.DefenseEV = defense;
            this.SpAttackEV = spAttack;
            this.SpDefenseEV = spDefense;
            this.HpEV = hp;
            this.SpeedEV = speed;
        }
    }
}
