using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SocialNetwork.Core.Models
{
    public class Book
    {
        
        [Key]
        public int BookId { get; set; }
        public int Year { get; set; }
        public int ISBN { get; set; }
        public string Title { get; set; }

        public virtual Author Author { get; set; }

        
        public int IdAuthor { get; set; }

    }
}
