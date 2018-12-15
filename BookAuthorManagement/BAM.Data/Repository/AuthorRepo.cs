using SocialNetwork.Core.Models;
using SocialNetwork.Web.Models.Authorr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Repository
{
    public class AuthorRepo
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public IQueryable<Author> IQueryList()
        {
            return db.Author;
        }

        public AuthorViewModel Details(Author a)
        {
            try
            {
                Author author = db.Author.FirstOrDefault(c => c.AuthorId == a.AuthorId);
                return new AuthorViewModel
                {
                    Name = author.Name,
                    LastName = author.LastName,
                    ElectronicMail = author.ElectronicMail,
                    BirthDate = author.BirthDate                    
                };

            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public bool Update(AuthorViewModel authorVM)
        {
            try
            {
                Author author = db.Author.FirstOrDefault(c => c.AuthorId == authorVM.AuthorId);
                author.AuthorId = authorVM.AuthorId;
                author.Name = authorVM.Name;
                author.LastName = authorVM.LastName;
                author.ElectronicMail = authorVM.ElectronicMail;                              

                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return true;
        }

        public async Task<bool> Delete(Author a)
        {
            try
            {
                Author author = db.Author.FirstOrDefault(c => c.AuthorId == a.AuthorId);
                
                db.Author.Remove(author);
                db.SaveChanges();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return true;
        }
    }
}
