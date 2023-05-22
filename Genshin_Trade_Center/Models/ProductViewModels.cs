using System.ComponentModel.DataAnnotations;

namespace Genshin_Trade_Center.Models
{
    /// <summary>
    /// View model used for
    /// <see cref="Controllers.CharactersController.Edit" />.
    /// </summary>
    /// <remarks></remarks>
    public class EditCharacterViewModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        /// <remarks></remarks>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// It must be from 5 to 64 characters long.
        /// This is a required property.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        /// <remarks></remarks>
        [Required]
        [StringLength(64, ErrorMessage = "Name must be between" +
                    " 5 and 64 characters", MinimumLength = 5)]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the price.
        /// It must be between 0.1 and 200.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        /// <remarks></remarks>
        [Range(0.1, 200)]
        public decimal Price { get; set; }
        /// <summary>
        /// Gets or sets the level.
        /// It must be from 1 to 90.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        /// <remarks></remarks>
        [Range(1, 90)]
        public int Level { get; set; }
        /// <summary>
        /// Gets or sets the friendship level.
        /// It must be from 1 to 10.
        /// </summary>
        /// <value>
        /// the friendship level.
        /// </value>
        /// <remarks></remarks>
        [Range(1, 10)]
        public int Friendship { get; set; }
        /// <summary>
        /// Gets or sets the constellation level.
        /// It must be from 1 to 6.
        /// </summary>
        /// <value>
        /// The constellation level.
        /// </value>
        /// <remarks></remarks>
        [Range(1, 6)]
        public int Constellation { get; set; }
    }

    /// <summary>
    /// View model used for
    /// <see cref="Controllers.ItemsController.Edit" />.
    /// </summary>
    /// <remarks></remarks>
    public class EditItemViewModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        /// <remarks></remarks>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// It must be from 5 to 64 characters long.
        /// This is a required property.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        /// <remarks></remarks>
        [Required]
        [StringLength(64, ErrorMessage = "Name must be between" +
            " 5 and 64 characters", MinimumLength = 5)]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the price.
        /// It must be between 0.1 and 200.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        /// <remarks></remarks>
        [Range(0.1, 200)]
        public decimal Price { get; set; }
        /// <summary>
        /// Gets or sets the level.
        /// It must be from 1 to 90.
        /// </summary>
        /// <value>
        /// the level.
        /// </value>
        /// <remarks></remarks>
        [Range(1, 90)]
        public int Level { get; set; }
        /// <summary>
        /// Gets or sets the refinement level.
        /// It must be from 1 to 5.
        /// </summary>
        /// <value>
        /// the refinement level.
        /// </value>
        /// <remarks></remarks>
        [Range(1, 5)]
        public int Refinement { get; set; }
    }
}
