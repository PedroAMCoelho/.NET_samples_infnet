using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Web.Models.Authorr
{
    public class AuthorViewModel
    {
        [Key]
        public int AuthorId { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string ElectronicMail { get; set; }
        public DateTime BirthDate { get; set; }
    }
}