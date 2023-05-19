using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Genshin_Trade_Center.Models
{
    public abstract class Product
    {
        private static int currentId;
        private int id;
        private string name;
        private decimal price;
        private int level;

        [Key]
        public int Id { get => id; set => id = value; }
        [Required]
        [StringLength(64, ErrorMessage = "Name must be between" +
            " 5 and 64 characters", MinimumLength = 5)]
        public string Name { get => name; set => name = value; }
        [Required]
        [Range(0.1, 200,
            ErrorMessage = "The Price must be between 0.1 and 200")]
        public decimal Price { get => price; set => price = value; }
        [Required]
        [Range(1, 90, ErrorMessage = "Level must be between 1 and 90")]
        public int Level { get => level; set => level = value; }
        [DisplayName("Seller")]
        public string SellerId { get; set; }
        public virtual User Seller { get; set; }

        static Product()
        {
            currentId = 1;
        }

        public Product()
        {
            id = currentId++;
            name = "";
            price = 0;
            level = 0;
        }

        public Product(string name, decimal price, int level) : this()
        {
            this.name = name;
            this.price = price;
            this.level = level;
        }
    }
}