using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genshin_Trade_Center.Models
{
    public class Character : Product
    {
        private int friendship;
        private int constellation;

        public int Friendship { get => friendship; set => friendship = value; }
        public int ArchetypeId { get; set; }
        public int Constellation { get => constellation; set => constellation = value; }
        public virtual CharacterArchetype Archetype { get; set; }

        public Character() : base() {}
        public Character(string name, int price, int level,
            int friendship) : 
            base(name, price, level) 
        {
            this.friendship = friendship;
        }
    }
}