using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Genshin_Trade_Center.Models
{
    public class EditCharacterViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(64, ErrorMessage = "Name must be between" +
            " 5 and 64 characters", MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        [Range(0.1, 200)]
        public decimal Price { get; set; }
        [Required]
        [Range(1, 90)]
        public int Level { get; set; }
        [Required]
        [Range(1, 10)]
        public int Friendship { get; set; }
        [Required]
        [Range(1, 6)]
        public int Constellation { get; set; }
    }
}