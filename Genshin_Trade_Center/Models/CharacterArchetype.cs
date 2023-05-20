using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        [DisplayName("Character")]
        [StringLength(64, MinimumLength = 1,
            ErrorMessage =" Name cannot be longer than 64 characters")]
        public string Name { get => name; set => name = value; }
        [Range(4, 5)]
        public int Quality { get => quality; set => quality = value; }
        [Required]
        [DisplayName("Weapon Type")]
        public EnumWeapon WeaponType { get => weaponType;
            set => weaponType = value; }
        [Required]
        [DisplayName("Vision Type")]
        public EnumVision VisionType { get => visionType;
            set => visionType = value; }
        public virtual List<Character> Characters { get; set; }

        static CharacterArchetype()
        {
            currentId = 1;
        }

        public CharacterArchetype()
        {
            id = currentId++;
            name = "";
            quality = 0;
            weaponType = EnumWeapon.Claymore;
            visionType = EnumVision.Dendro;
        }

        public CharacterArchetype(string name, int quality,
            EnumWeapon weaponType, EnumVision visionType) : this()
        {
            id = currentId;
            this.name = name;
            this.quality = quality;
            this.weaponType = weaponType;
            this.visionType = visionType;
        }
    }
}
