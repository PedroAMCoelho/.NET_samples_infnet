using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]        
        [Display(Name = "Owner Name")]
        public string GardenOwnerName { get; set; }

        //gender and birthday may be added in future versions

        //below - profile info:
        [Display(Name = "Garden Picture URL")]        
        public HttpPostedFileBase UploadPictureUrl { get; set; }

        [Display(Name = "Garden Picture URL")]
        public string PictureUrl { get; set; }

        [Display(Name = "Garden Name")]
        public string GardenName { get; set; }

        [Display(Name = "Garden Description")]
        public string GardenDescription { get; set; }

        [Display(Name = "Main Garden Type")]
        public string MainGarden { get; set; }

        [Display(Name = "Sub Garden Type")]
        public string SubGarden { get; set; }

        [Display(Name = "Garden Location")]
        public string GardenLocation { get; set; }

    }
}