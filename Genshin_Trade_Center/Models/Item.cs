using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genshin_Trade_Center.Models
{

    public class Item : Product
    {
        private int refinement;

        public int Refinement { get => refinement; set => refinement = value; }
        public int TypeId { get; set; }
        public virtual Weapon Type { get; set; }

        public Item(string name, decimal price, int level,
            int refinement) : base(name, price, level) 
        {
            this.refinement = refinement;
        }
    }
}