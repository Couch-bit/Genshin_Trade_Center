using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Genshin_Trade_Center.Models
{
    /// <summary>
    /// Represents a product sold on the market.
    /// </summary>
    /// <remarks></remarks>
    public abstract class Product
    {
        private static int currentId;
        private int id;
        private string name;
        private decimal price;
        private int level;

        /// <summary>
        /// Gets or sets the id.
        /// This is the primary key.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        /// <remarks></remarks>
        [Key]
        public int Id { get => id; set => id = value; }
        /// <summary>
        /// Gets or sets the name.
        /// It must be between 5 and 64 characters long.
        /// This is a required field.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        /// <remarks></remarks>
        [Required]
        [StringLength(64, ErrorMessage = "Name must be between" +
                    " 5 and 64 characters", MinimumLength = 5)]
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
                    ErrorMessage = "The Price must be between" +
            " 0.1 € and 200 €")]
        public decimal Price { get => price; set => price = value; }
        /// <summary>
        /// Gets or sets the level of the product.
        /// It must be from 1 to 90.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        [Range(1, 90, ErrorMessage = "Level must be between 1 and 90")]
        public int Level { get => level; set => level = value; }
        /// <summary>
        /// Gets or sets the id of the user selling the product.
        /// </summary>
        /// <value>
        /// the id of the seller.
        /// </value>
        [DisplayName("Seller")]
        public string SellerId { get; set; }
        /// <summary>
        /// Gets or sets the user selling the product.
        /// </summary>
        /// <value>
        /// The seller.
        /// </value>
        /// <remarks></remarks>
        public virtual User Seller { get; set; }

        static Product()
        {
            currentId = 1;
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Product" /> class. 
        /// </summary>
        /// <remarks></remarks>
        public Product()
        {
            id = currentId++;
            name = "";
            price = 0;
            level = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product" /> class. 
        /// </summary>
        /// <param name="name">the name</param>
        /// <param name="price">the price</param>
        /// <param name="level">the level</param>
        /// <remarks></remarks>
        public Product(string name, decimal price, int level) : this()
        {
            this.name = name;
            this.price = price;
            this.level = level;
        }
    }
}
