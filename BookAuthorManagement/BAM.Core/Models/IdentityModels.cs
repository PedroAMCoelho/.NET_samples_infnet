using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using SocialNetwork.Core.Models;
using System.Data.Entity;

namespace SocialNetwork.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity>GenerateUserIdentityAsync(UserManager<ApplicationUser>manager,
            string authenticationType)
        {
            return await manager.CreateIdentityAsync(this, authenticationType);
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Author> Author { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }        
    }
}