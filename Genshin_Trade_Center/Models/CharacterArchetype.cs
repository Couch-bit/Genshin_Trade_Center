using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Genshin_Trade_Center.Models
{
    public enum EnumVision
    {
        Dendro,
        Hydro,
        Cryo,
        Geo,
        Anemo,
        Pyro,
        Electro
    }

    public class CharacterArchetype
    {
        private static int currentId;
        private int id;
        private string name;
        private int quality;
        private EnumWeapon weaponType;
        private EnumVision visionType;

        [Key]
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Quality { get => quality; set => quality = value; }
        public EnumWeapon WeaponType { get => weaponType; set => weaponType = value; }
        public EnumVision VisionType { get => visionType; set => visionType = value; }
        public virtual List<Character> Characters { get; set; }

        static CharacterArchetype()
        {
            currentId = 1;
        }

        public CharacterArchetype(string name, int quality, EnumWeapon weaponType, EnumVision visionType)
        {
            id = currentId;
            this.name = name;
            this.quality = quality;
            this.weaponType = weaponType;
            this.visionType = visionType;
        }
    }
}