using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity>GenerateUserIdentityAsync(UserManager<ApplicationUser>manager,
            string authenticationType)
        {
            return await manager.CreateIdentityAsync(this, authenticationType);
        }

        public string GardenOwnerName { get; set; }
        public virtual UProfile Profile { get; set; }

    }

    public class UProfile
    {
        [Key]
        public int ProfileId { get; set; }
        public string PictureUrl { get; set; }
        public string GardenName { get; set; }
        public string GardenDescription { get; set; }
        public string MainGarden { get; set; }
        public string SubGarden { get; set; }
        public string GardenLocation { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<UProfile> Profile { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}