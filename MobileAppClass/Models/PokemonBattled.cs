﻿using System;
namespace MobileAppClass
{
    /// <summary>
    /// Records the EV yields of a Pokemon that was battled against
    /// </summary>
    public class PokemonBattled
    {
        public string Name { get; set; }
        public int AttackEV { get; set; }
        public int DefenseEV { get; set; }
        public int SpAttackEV { get; set; }
        public int SpDefenseEV { get; set; }
        public int HpEV { get; set; }
        public int SpeedEV { get; set; }

        public PokemonBattled() 
        {
            Name = string.Empty;
            AttackEV = 0;
            DefenseEV = 0;
            SpAttackEV = 0;
            SpDefenseEV = 0;
            HpEV = 0;
            SpeedEV = 0;
        }

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

        internal void Clear()
        {
            Name = string.Empty;
            AttackEV = 0;
            DefenseEV = 0;
            SpAttackEV = 0;
            SpDefenseEV = 0;
            HpEV = 0;
            SpeedEV = 0;
        }
    }
}
