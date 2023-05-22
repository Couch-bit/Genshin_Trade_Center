using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Genshin_Trade_Center.Models
{
    /// <summary>
    /// Represents resources sold in the store.
    /// </summary>
    /// <remarks></remarks>
    public class Resource
    {
        private static int currentId;
        private int id;
        private string name;
        private decimal price;

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
        /// The name must be from 5 to 64 characters long.
        /// This is a required field.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        /// <remarks></remarks>
        [Required]
        [StringLength(64, MinimumLength = 5,
                    ErrorMessage = "Name must be between 5" +
            " and 64 characters")]
        public string Name { get => name; set => name = value; }
        /// <summary>
        /// Gets or sets the price.
        /// It must be between 0.1 and 200.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        /// <remarks></remarks>
        [DataType(DataType.Currency)]
        [Range(0.1, 200,
                    ErrorMessage = "Price must be between 0.1 € and 200 €")]
        public decimal Price { get => price; set => price = value; }
        /// <summary>
        /// Gets or sets the list containing all the <see cref="User" />
        /// objects selling the resource on the market.
        /// </summary>
        /// <value>
        /// The list of sellers.
        /// </value>
        /// <remarks></remarks>
        public virtual List<User> Sellers { get; set; }

        static Resource()
        {
            currentId = 1;
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Resource" /> class. 
        /// </summary>
        /// <remarks></remarks>
        public Resource()
        {
            id = currentId++;
            name = "";
            price = 0;
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Resource" /> class.
        /// </summary>
        /// <param name="name">the name</param>
        /// <param name="price">the price</param>
        /// <remarks></remarks>
        public Resource(string name, decimal price) : this()
        {
            this.name = name;
            this.price = price;
        }
    }
}
