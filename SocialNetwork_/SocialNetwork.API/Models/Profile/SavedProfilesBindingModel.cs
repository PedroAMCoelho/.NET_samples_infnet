using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SocialNetwork.API.Models.Profile
{
    public class SavedProfilesBindingModel
    {
        [Required]
        public string SavedProfileGardenName { get; set; }
    }
}