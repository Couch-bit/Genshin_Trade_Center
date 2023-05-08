using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Genshin_Trade_Center.Models
{
    public class User
    {
        private static int currentId;
        private int id;
        private string email;
        private string password;
        private string nickname;

        [Key]
        public int Id { get => id; set => id = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public virtual List<Product> Products { get; set; }
        public virtual List<Resource> Resources { get; set; }

        static User()
        {
            currentId = 1;
        }

        public User(string email, string password, string nickname) 
        {
            id = currentId++;
            this.email = email;
            this.password = password;
            this.nickname = nickname;
        }
    }
}