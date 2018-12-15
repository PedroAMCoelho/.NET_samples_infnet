using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Core.Models;
using SocialNetwork.Web.Models.Authorr;
using SocialNetwork.Web.Models.Book;

namespace SocialNetwork.Core.Repository
{
    public class BookRepo
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public bool Add(AddBookViewModel bookVM)
        {
            try
            {                
                Book book = new Book();
                book.BookId = bookVM.BookId;
                book.ISBN = bookVM.ISBN;
                book.Title = bookVM.Title;
                book.Year = bookVM.Year;
                book.Author = new Author()
                {
                    Name = bookVM.Name,
                    LastName = bookVM.LastName,
                    ElectronicMail = bookVM.ElectronicMail,
                    BirthDate = bookVM.BirthDate
                };
                
                db.Book.Add(book);               
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return true;
        }

        public List<Book> List()
        {
            return db.Book.ToList();
        }

        public async Task <bool> Delete(Book b)
        {
            try
            {
                Book book = db.Book.FirstOrDefault(c => c.BookId == b.BookId);
               db.Book.Remove(book);
               db.SaveChanges();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return true;
        }

        public bool Update(AddBookViewModel bookVM)
        {
            try
            {                
                Book book = db.Book.FirstOrDefault(c => c.BookId == bookVM.BookId);
                book.BookId = bookVM.BookId;
                book.ISBN = bookVM.ISBN;
                book.Title = bookVM.Title;
                book.Year = bookVM.Year;

                book.Author = new Author
                {
                    Name = bookVM.Name,
                    LastName = bookVM.LastName,
                    ElectronicMail = bookVM.ElectronicMail,
                    BirthDate = bookVM.BirthDate
                };
                
                db.SaveChanges();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return true;
        }

        public AddBookViewModel Details(Book b)
        {
            try
            {
                Book book = db.Book.FirstOrDefault(c => c.BookId == b.BookId);
                return new AddBookViewModel
            {                
                ISBN = book.ISBN,
                Title = book.Title,
                Year = book.Year,
                Name = book.Author.Name,
                LastName = book.Author.LastName,
                ElectronicMail = book.Author.ElectronicMail,
                BirthDate = book.Author.BirthDate
            };

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            
        }        
        /*
        public Author GetAuthor()
        {
            AddBookViewModel book = new AddBookViewModel();
            int bookId = book.BookId; // will be the same as AuthorId
            var author = db.Author.FirstOrDefault(a => a.AuthorId == bookId);
            
            return author;
        } */
    }
}
