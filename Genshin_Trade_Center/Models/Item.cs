using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Genshin_Trade_Center.Models
{

    /// <summary>
    /// A class representing the items sold in the store.
    /// </summary>
    /// <remarks></remarks>
    public class Item : Product
    {
        private int refinement;

        /// <summary>
        /// Gets or sets the refinement level of the item.
        /// It must be from 1 to 5.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        [Range(1, 5)]
        public int Refinement 
        { get => refinement; set => refinement = value; }
        /// <summary>
        /// Gets or sets the id of the weapon.
        /// </summary>
        /// <value>
        /// the id of the weapom.
        /// </value>
        [DisplayName("Weapon")]
        public int TypeId { get; set; }
        /// <summary>
        /// Gets or sets the weapon.
        /// </summary>
        /// <value>
        /// The weapon.
        /// </value>
        public virtual Weapon Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item" /> class. 
        /// </summary>
        /// <remarks></remarks>
        public Item() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item" /> class. 
        /// </summary>
        /// <param name="name">the name</param>
        /// <param name="price">the price</param>
        /// <param name="level">the level</param>
        /// <param name="refinement">the refinement</param>
        /// <remarks></remarks>
        public Item(string name, decimal price, int level,
                    int refinement) : base(name, price, level)
        {
            this.refinement = refinement;
        }
    }
}
