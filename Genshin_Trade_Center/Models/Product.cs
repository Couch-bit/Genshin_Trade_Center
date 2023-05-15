using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        public string Name { get => name; set => name = value; }
        public decimal Price { get => price; set => price = value; }
        public int Level { get => level; set => level = value; }
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