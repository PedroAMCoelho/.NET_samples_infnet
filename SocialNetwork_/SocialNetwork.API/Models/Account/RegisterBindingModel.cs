using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.API.Models.Account
{
    //for creating an user in the system
    public class RegisterBindingModel
    {
        [Required(ErrorMessage = "Please enter the user's email.")]
        [EmailAddress]
        public string Email { get; set; }

        
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        public string GardenOwnerName { get; set; }
        //for rendering a garden profile:
        [Required(ErrorMessage = "Please enter the user's first pictureUrl")]
        public string PictureUrl { get; set; }

        [Required(ErrorMessage = "Please enter the user's garden name.")]
        public string GardenName { get; set; }

        [Required(ErrorMessage = "Please enter the user's garden description.")]
        public string GardenDescription { get; set; }

        [Required(ErrorMessage = "Please enter the user's main garden name.")]
        public string MainGarden { get; set; }

        [Required(ErrorMessage = "Please enter the user's main sub garden name.")]
        public string SubGarden { get; set; }

        [Required(ErrorMessage = "Please enter the user's garden location.")]
        public string GardenLocation { get; set; }
    }
}