﻿using System;
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
        public int Constellation { get => constellation; set => constellation = value; }
        public int ArchetypeId { get; set; }
        public virtual CharacterArchetype Archetype { get; set; }

        public Character() : base() {}
        public Character(string name, int price, int level,
            int friendship, int constellation) : 
            base(name, price, level) 
        {
            this.friendship = friendship;
            this.constellation = constellation;
        }
    }
}