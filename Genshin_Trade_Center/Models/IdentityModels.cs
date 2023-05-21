using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Genshin_Trade_Center.Models
{
    /// <summary>
    /// Class representing the market users.
    /// </summary>
    /// <remarks></remarks>
    public class User : IdentityUser
    {
        /// <summary>
        /// Gets or sets the username.
        /// It displays as "Seller".
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        [DisplayName("Seller")]
        public override string UserName { get; set; }
        /// <summary>
        /// Gets or sets the list containing the Resources
        /// sold by the user.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
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

    /// <summary>
    /// The database context for the App.
    /// </summary>
    /// <remarks></remarks>
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ApplicationDbContext" /> class with
        /// a database initializer. 
        /// </summary>
        /// <remarks></remarks>
        public ApplicationDbContext()
                    : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer
                (new DBInitializer<ApplicationDbContext>());
        }

        /// <summary>
        /// Gets or sets the products dataset.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        /// <remarks></remarks>
        public DbSet<Product> Products { get; set; }
        /// <summary>
        /// Gets or sets the resources dataset.
        /// </summary>
        /// <value>
        /// The resources.
        /// </value>
        /// <remarks></remarks>
        public DbSet<Resource> Resources { get; set; }
        /// <summary>
        /// Gets or sets the characters dataset.
        /// </summary>
        /// <value>
        /// The characters.
        /// </value>
        /// <remarks></remarks>
        public DbSet<Character> Characters { get; set; }
        /// <summary>
        /// Gets or sets the weapons dataset.
        /// </summary>
        /// <value>
        /// The weapons.
        /// </value>
        /// <remarks></remarks>
        public DbSet<Weapon> Weapons { get; set; }
        /// <summary>
        /// Gets or sets the weapons dataset.
        /// </summary>
        /// <value>
        /// The weapons.
        /// </value>
        /// <remarks></remarks>
        public DbSet<CharacterArchetype> CharacterArchetypes
        { get; set; }

        /// <summary>
        /// Creates and returns the database context.
        /// </summary>
        /// <returns>
        /// the database context.
        /// </returns>
        /// <remarks></remarks>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    /// <summary>
    /// Class representing database initializer
    /// which seeds the database on creation.
    /// </summary>
    /// <typeparam name="T">The Context</typeparam>
    /// <remarks></remarks>
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
                new Resource("10000 Mora", 2),
                new Resource("100000 Mora", 20),
                new Resource("1000000 Mora", 200)
            };

            characterArchetypes.ForEach(archetype => context
            .CharacterArchetypes.Add(archetype));
            weapons.ForEach(weapon => context
            .Weapons.Add(weapon));
            resources.ForEach(resource => context
            .Resources.Add(resource));

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
