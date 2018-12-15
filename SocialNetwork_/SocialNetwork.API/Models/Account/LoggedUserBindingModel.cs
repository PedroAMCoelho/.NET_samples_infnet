using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.API.Models.Account
{
    public class LoggedUserBindingModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
               
        public string GardenOwnerName { get; set; }

        //for rendering a garden profile:
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string GardenName { get; set; }
        [Required]
        public string GardenDescription { get; set; }
        [Required]
        public string MainGarden { get; set; }
        [Required]
        public string SubGarden { get; set; }
        [Required]
        public string GardenLocation { get; set; }
    }
}