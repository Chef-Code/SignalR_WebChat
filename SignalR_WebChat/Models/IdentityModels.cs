using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SignalR_WebChat.Models.DataModels;

namespace SignalR_WebChat.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        //public virtual AppUser AppUser { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clubs>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Clubs");
            });

            modelBuilder.Entity<Diamonds>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Diamonds");
            });

            modelBuilder.Entity<Hearts>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Hearts");
            });

            modelBuilder.Entity<Spades>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Spades");
            });
        }

    }
}