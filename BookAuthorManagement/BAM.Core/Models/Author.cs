using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Models
{
    public class Author
    {
        
        [Key]
        public int AuthorId { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string ElectronicMail { get; set; }
        public DateTime BirthDate { get; set; }

        public int IdBook;
        
    }
}
