using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Genshin_Trade_Center.Models
{
    public class UserResourceVM
    {
        [Required(ErrorMessage = "Input the Seller")]
        [DisplayName("Seller")]
        public int SellerId { get; set; }
        [Required(ErrorMessage = "Input the Resource")]
        [DisplayName("Resource")]
        public int ResourceId { get; set; }
    }
}