using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Genshin_Trade_Center.Models
{
    public class Character : Product
    {
        private int friendship;
        private int constellation;

        [Required]
        [Range(1, 10)]
        public int Friendship { get => friendship;
            set => friendship = value; }
        [Required]
        [Range(1, 6)]
        public int Constellation { get => constellation;
            set => constellation = value; }
        [Required]
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