using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Genshin_Trade_Center.Models
{
    public class CharacterViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Level { get; set; }
        public int Friendship { get; set; }
        public int Constellation { get; set; }
        public int ArchetypeId { get; set; }
    }
}