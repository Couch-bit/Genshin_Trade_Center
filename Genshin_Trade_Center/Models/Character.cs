using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Genshin_Trade_Center.Models
{
    /// <summary>
    /// Class representing characters currently on the market.
    /// </summary>
    public class Character : Product
    {
        private int friendship;
        private int constellation;

        /// <summary>
        /// Gets or sets the friendship level.
        /// It must be an integer from 1 to 10.
        /// </summary>
        /// <value>
        /// The friendship level.
        /// </value>
        /// <remarks></remarks>
        [Range(1, 10)]
        public int Friendship
        {
            get => friendship;
            set => friendship = value;
        }
        /// <summary>
        /// Gets or sets the constellation level.
        /// It must be an integer from 1 to 6.
        /// </summary>
        /// <value>
        /// The constellation level.
        /// </value>
        /// <remarks></remarks>
        [Range(1, 6)]
        public int Constellation
        {
            get => constellation;
            set => constellation = value;
        }
        /// <summary>
        /// Gets or sets the id of the <see cref="CharacterArchetype" />
        /// the character belongs to.
        /// </summary>
        /// <value>
        /// the archetype id of the <see cref="CharacterArchetype" />.
        /// </value>
        public int ArchetypeId { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="CharacterArchetype" />
        /// the character belongs to.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public virtual CharacterArchetype Archetype { get; set; }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Character" /> class. 
        /// </summary>
        /// <remarks></remarks>
        public Character() : base() { }
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Character" /> class. 
        /// </summary>
        /// <param name="name">the name of the character</param>
        /// <param name="price">the price of the character</param>
        /// <param name="level">the level of the character</param>
        /// <param name="friendship">
        /// the friendship level of the character
        /// </param>
        /// <param name="constellation">
        /// the constellation level of the character
        /// </param>
        /// <remarks></remarks>
        public Character(string name, int price, int level,
                    int friendship, int constellation) :
                    base(name, price, level)
        {
            this.friendship = friendship;
            this.constellation = constellation;
        }
    }
}
