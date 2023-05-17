using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Genshin_Trade_Center.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public virtual List<Resource> Resources { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer
                (new DBInitializer<ApplicationDbContext>());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<CharacterArchetype> CharacterArchetypes { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    internal class DBInitializer<T> :
        CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            List<CharacterArchetype> characterArchetypes = new List<CharacterArchetype>
            {
                new CharacterArchetype("Raiden Shogun", 5, EnumWeapon.Spear, EnumVision.Electro),
                new CharacterArchetype("Zhongli", 5, EnumWeapon.Spear, EnumVision.Geo),
                new CharacterArchetype("Wanderer", 5, EnumWeapon.Catalyst, EnumVision.Anemo),
                new CharacterArchetype("Layla", 4, EnumWeapon.Sword, EnumVision.Cryo)
            };
            characterArchetypes.ForEach(archetype => context.CharacterArchetypes.Add(archetype));
            context.SaveChanges();
        }
    }
}