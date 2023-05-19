using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Genshin_Trade_Center.Models
{
    public class User : IdentityUser
    {
        [DisplayName("Seller")]
        public override string UserName { get; set; }
        public virtual List<Resource> Resources { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync
            (UserManager<User> manager)
        {
            ClaimsIdentity userIdentity = await manager
                .CreateIdentityAsync
                (this, DefaultAuthenticationTypes.ApplicationCookie);
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
        public DbSet<CharacterArchetype> CharacterArchetypes
        { get; set; }

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
            // Adds Data.
            List<CharacterArchetype> characterArchetypes = new
                List<CharacterArchetype>
            {
                new CharacterArchetype("Raiden Shogun", 5,
                EnumWeapon.Spear, EnumVision.Electro),
                new CharacterArchetype("Zhongli", 5,
                EnumWeapon.Spear, EnumVision.Geo),
                new CharacterArchetype("Wanderer", 5,
                EnumWeapon.Catalyst, EnumVision.Anemo),
                new CharacterArchetype("Layla", 4,
                EnumWeapon.Sword, EnumVision.Cryo)
            };
            List<Weapon> weapons = new
                List<Weapon>
            {
                new Weapon("Polar Star", EnumStat.CRITRate,
                EnumWeapon.Bow, "\"I was once a wounded wolf," +
                " betrayed by the whole world,\"", 5),
                new Weapon("Wandering Evenstar",
                EnumStat.ElementalMastery, EnumWeapon.Catalyst,
                "\"The rainforest trail road was" +
                " so treacherous that mortals could only tell the way" +
                " ahead by the moonlight breaking" +
                " through the leaves.\"", 4),
                new Weapon("Redhorn Stonethresher", EnumStat.CRITDMG,
                EnumWeapon.Claymore, "The full name of this weapon is" +
                " the \"Mighty Redhorn Stoic Stonethreshing" +
                " Gilded Goldcrushing Lion Lord.\"", 5),
                new Weapon("Dragon's Bane", EnumStat.ElementalMastery,
                EnumWeapon.Spear, "Rumored to be a" +
                " legendary polearm of Liyue.", 4),
            };
            List<Resource> resources = new
                List<Resource>
            {
                new Resource("Mora 10000", 2),
                new Resource("Mora 100000", 20),
                new Resource("Mora 1000000", 200)
            };

            characterArchetypes.ForEach(archetype => context
            .CharacterArchetypes.Add(archetype));
            weapons.ForEach(weapon => context
            .Weapons.Add(weapon));

            // Adds Admin Account.
            RoleManager<IdentityRole> _roleManager = 
                new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            UserManager<User> _userManager =
                new UserManager<User>
                (new UserStore<User>(context));

            IdentityRole role = new IdentityRole
            {
                Name = "Admin"
            };
            _roleManager.Create(role);

            User user = new User
            {
                UserName = "Admin",
                Email = "admin@admin.com",
            };
            string password = "jRs#vYtuctt#5$CF";
            _userManager.Create(user, password);

            _userManager.AddToRole(user.Id, "Admin");

            context.SaveChanges();
        }
    }
}