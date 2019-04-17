using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Sawa2ly.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("UserRule", this.UserRule.ToString()));
            userIdentity.AddClaim(new Claim("FName", this.FName.ToString()));
            userIdentity.AddClaim(new Claim("LName", this.LName.ToString()));
            userIdentity.AddClaim(new Claim("UserImageUrl", this.UserImageUrl.ToString()));
            return userIdentity;
        }
        public string FName { get; set; }
        public string LName { get; set; }
        public string UserRule { get; set; }
        public string UserImageUrl { get; set; }
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
        public System.Data.Entity.DbSet<Sawa2ly.Models.Project> Project { get; set; }
        public System.Data.Entity.DbSet<Sawa2ly.Models.ProjectModule> ProjectModule { get; set; }
        public System.Data.Entity.DbSet<Sawa2ly.Models.ProjectTrainees> ProjectTrainees { get; set; }
        public System.Data.Entity.DbSet<Sawa2ly.Models.ProjectRequestsMD> ProjectRequestsMD { get; set; }
        public System.Data.Entity.DbSet<Sawa2ly.Models.ProjectsJoinRequests> ProjectsJoinRequests { get; set; }

    }
}