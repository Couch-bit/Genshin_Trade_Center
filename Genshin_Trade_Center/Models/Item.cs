using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Genshin_Trade_Center.Models
{

    public class Item : Product
    {
        private int refinement;

        [Required]
        [Range(1, 5)]
        public int Refinement { get => refinement; set => refinement = value; }
        [Required]
        [DisplayName("Type")]
        public int TypeId { get; set; }
        [Required]
        public virtual Weapon Type { get; set; }

        public Item() : base() {}
        public Item(string name, decimal price, int level,
            int refinement) : base(name, price, level) 
        {
            this.refinement = refinement;
        }
    }
}