using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Genshin_Trade_Center.Models
{
    public enum EnumStat
    {
        ATK,
        DEF,
        HP,
        CRITDMG,
        CRITRate,
        ElementalMastery,
        EnergyRecharge,
        PhysicalDMG
    }
    public enum EnumWeapon
    {
        Sword,
        Claymore,
        Catalyst,
        Bow,
        Spear
    }

    public class Weapon
    {
        private static int currentId;
        private int id;
        private string name;
        private EnumStat mainStat;
        private EnumWeapon type;
        private string description;
        private int quality;

        [Key]
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public EnumStat MainStat { get => mainStat; set => mainStat = value; }
        public EnumWeapon Type { get => type; set => type = value; }
        public string Description { get => description; set => description = value; }
        public int Quality { get => quality; set => quality = value; }
        public virtual List<Item> Items { get; set; }

        static Weapon()
        {
            currentId = 1;
        }

        public Weapon()
        {
            id = currentId++;
            name = "";
            mainStat = EnumStat.ATK;
            type = EnumWeapon.Sword;
            description = "";
            quality = 0;
        }

        public Weapon(string name, EnumStat mainStat, EnumWeapon type,
            string description, int quality) : this()
        {
            this.name = name;
            this.mainStat = mainStat;
            this.type = type;
            this.description = description;
            this.quality = quality;
        }
    }
}