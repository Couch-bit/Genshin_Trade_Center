using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Genshin_Trade_Center.Models
{
    /// <summary>
    /// Enum for the vision types present in Genshin Impact.
    /// </summary>
    /// <remarks></remarks>
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

    /// <summary>
    /// Class representing characters playable in Genshin Impact.
    /// </summary>
    /// <remarks></remarks>
    public class CharacterArchetype
    {
        private static int currentId;
        private int id;
        private string name;
        private int quality;
        private EnumWeapon weaponType;
        private EnumVision visionType;

        /// <summary>
        /// Gets or sets the id.
        /// It serves as the primary key in the database.
        /// </summary>
        /// <value>
        /// the id.
        /// </value>
        /// <remarks></remarks>
        [Key]
        public int Id { get => id; set => id = value; }
        /// <summary>
        /// Gets or sets the name of the character.
        /// It must be of length from 1 to 64.
        /// This is a required property.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        /// <remarks></remarks>
        [Required]
        [DisplayName("Character")]
        [StringLength(64, MinimumLength = 1,
                    ErrorMessage = " Name cannot be longer than 64 characters")]
        public string Name { get => name; set => name = value; }
        /// <summary>
        /// Gets or sets the quality of the character.
        /// It must 4 or 5.
        /// </summary>
        /// <value>
        /// the quality.
        /// </value>
        /// <remarks></remarks>
        [Range(4, 5, ErrorMessage = "Quality must be 4 or 5")]
        public int Quality { get => quality; set => quality = value; }
        /// <summary>
        /// Gets or sets the weapon type used by the character.
        /// Displays as "Weapon Type".
        /// This is a required property.
        /// </summary>
        /// <value>
        /// The weapon type.
        /// </value>
        /// <remarks></remarks>
        [Required]
        [DisplayName("Weapon Type")]
        public EnumWeapon WeaponType
        {
            get => weaponType;
            set => weaponType = value;
        }
        /// <summary>
        /// Gets or sets the vision type used by the character.
        /// Displays as "Vision Type".
        /// This is a required property.
        /// </summary>
        /// <value>
        /// The vision type.
        /// </value>
        /// <remarks></remarks>
        [Required]
        [DisplayName("Vision Type")]
        public EnumVision VisionType
        {
            get => visionType;
            set => visionType = value;
        }
        /// <summary>
        /// Gets or sets the list of characters of this archetype
        /// currently sold on the market.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public virtual List<Character> Characters { get; set; }

        static CharacterArchetype()
        {
            currentId = 1;
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="CharacterArchetype" /> class. 
        /// </summary>
        /// <remarks></remarks>
        public CharacterArchetype()
        {
            id = currentId++;
            name = "";
            quality = 0;
            weaponType = EnumWeapon.Claymore;
            visionType = EnumVision.Dendro;
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="CharacterArchetype" /> class. 
        /// </summary>
        /// <param name="name">the name of the character.</param>
        /// <param name="quality">the quality of the character.</param>
        /// <param name="weaponType">
        /// the weapon type used by the character.
        /// </param>
        /// <param name="visionType">
        /// the vision type used by the character.
        /// </param>
        /// <remarks></remarks>
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
