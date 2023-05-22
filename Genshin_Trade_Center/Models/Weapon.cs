using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Genshin_Trade_Center.Models
{
    /// <summary>
    /// Enum for the stat types present in Genshin Impact.
    /// </summary>
    /// <remarks></remarks>
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
    /// <summary>
    /// Enum for the weapon types present in Genshin Impact.
    /// </summary>
    /// <remarks></remarks>
    public enum EnumWeapon
    {
        Sword,
        Claymore,
        Catalyst,
        Bow,
        Spear
    }

    /// <summary>
    /// Represents weapons present in Genshin Impact.
    /// </summary>
    /// <remarks></remarks>
    public class Weapon
    {
        private static int currentId;
        private int id;
        private string name;
        private EnumStat mainStat;
        private EnumWeapon type;
        private string description;
        private int quality;

        /// <summary>
        /// Gets or sets the id.
        /// This is a primary key.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        /// <remarks></remarks>
        [Key]
        public int Id { get => id; set => id = value; }
        /// <summary>
        /// Gets or sets the name.
        /// It must be from 5 to 64 characters long.
        /// Displays as "Weapon".
        /// This is a required field.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        /// <remarks></remarks>
        [Required]
        [DisplayName("Weapon")]
        [StringLength(64, MinimumLength = 5,
                    ErrorMessage = "Name must be between 5 and 64 characters")]
        public string Name { get => name; set => name = value; }
        /// <summary>
        /// Gets or sets the main stat.
        /// Displays as "Main Stat".
        /// This is a required property.
        /// </summary>
        /// <value>
        /// The main stat.
        /// </value>
        /// <remarks></remarks>
        [Required]
        [DisplayName("Main Stat")]
        public EnumStat MainStat
        {
            get => mainStat;
            set => mainStat = value;
        }
        /// <summary>
        /// Gets or sets the weapon type.
        /// This is a required property.
        /// </summary>
        /// <value>
        /// The weapon type.
        /// </value>
        /// <remarks></remarks>
        [Required]
        public EnumWeapon Type { get => type; set => type = value; }
        /// <summary>
        /// Gets or sets the description.
        /// It must be from 5 to 1024 characters long.
        /// This is a required property.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        /// <remarks></remarks>
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(1024, MinimumLength = 5,
                    ErrorMessage = "Name must be between 5 and 64 characters")]
        public string Description
        {
            get => description;
            set => description = value;
        }
        /// <summary>
        /// Gets or sets the quality.
        /// It must be from 1 to 5.
        /// </summary>
        /// <value>
        /// The quality.
        /// </value>
        /// <remarks></remarks>
        [Range(1, 5)]
        public int Quality { get => quality; set => quality = value; }
        /// <summary>
        /// Gets or sets the list containing the <see cref="Item" />
        /// objects being sold which are this weapon.
        /// </summary>
        /// <value>
        /// The items which are this weapon.
        /// </value>
        /// <remarks></remarks>
        public virtual List<Item> Items { get; set; }

        static Weapon()
        {
            currentId = 1;
        }

        /// <summary>
        /// Initializes a new instance of
        /// the <see cref="Weapon" /> class. 
        /// </summary>
        /// <remarks></remarks>
        public Weapon()
        {
            id = currentId++;
            name = "";
            mainStat = EnumStat.ATK;
            type = EnumWeapon.Sword;
            description = "";
            quality = 0;
        }

        /// <summary>
        /// Initializes a new instance of
        /// the <see cref="Weapon" /> class. 
        /// </summary>
        /// <param name="name">the name</param>
        /// <param name="mainStat">the main stat</param>
        /// <param name="type">the weapon type</param>
        /// <param name="description">the description</param>
        /// <param name="quality">the quality</param>
        /// <remarks></remarks>
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
