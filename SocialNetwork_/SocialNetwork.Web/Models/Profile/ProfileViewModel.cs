using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SocialNetwork.Web.Models.Profile
{
    public class ProfileViewModel
    {
        [Display(Name = "Picture Url")]
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