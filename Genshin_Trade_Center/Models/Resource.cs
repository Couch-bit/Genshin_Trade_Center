﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Genshin_Trade_Center.Models
{
    public class Resource
    {
        private static int currentId;
        private int id;
        private string name;
        private decimal price;

        [Key]
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public decimal Price { get => price; set => price = value; }
        public virtual List<User> Sellers { get; set; }

        static Resource()
        {
            currentId = 1;
        }

        public Resource() 
        {
            id = currentId++;
            name = "";
            price = 0;
        }

        public Resource(string name, decimal price) : this()
        {
            this.name = name;
            this.price = price;
        }
    }
}