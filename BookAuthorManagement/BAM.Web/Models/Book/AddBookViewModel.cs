using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Web.Models.Book
{
    public class AddBookViewModel
    {
        [Key]
        public int BookId { get; set; }
        [Display(Name = "Year")]
        public int Year { get; set; }
        [Display(Name = "ISBN")]
        public int ISBN { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        
        public virtual Author Author { get; set; }
        [Display(Name = "Author Name")]
        public string Name { get; set; }
        [Display(Name = "Author Last Name")]
        public string LastName { get; set; }
        public string ElectronicMail { get; set; }
        public DateTime BirthDate { get; set; }
    }
}