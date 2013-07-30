using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWSStart.Web.Models
{
    public class ProfileViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int MemberID { get; set; }

        [Required]
        public string Name { get; set; }

        [DisplayName("Email Address")]
        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Remote("CheckEmailIsUsed", "ProfileSurface", ErrorMessage = "The email address has already been registered")]
        public string EmailAddress { get; set; }
        
        [Required]
        [Remote("CheckProfileURLAvailable", "ProfileSurface", ErrorMessage = "The profile URL is already in use")]
        [DisplayName("Profile URL")]
        public string ProfileURL { get; set; }

        public string Description { get; set; }

        public string Twitter { get; set; }

        [Url(ErrorMessage = "This is not a valid URL")]
        public string LinkedIn { get; set; }

        [MinLength(1, ErrorMessage = "It's unlikey your Skype username is a single letter")]
        public string Skype { get; set; }
    }

    public class ViewProfileViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int MemberID { get; set; }

        public string Name { get; set; }

        [DisplayName("Email address")]
        public string EmailAddress { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string MemberType { get; set; }

        public string Description { get; set; }

        public string Twitter { get; set; }

        public string LinkedIn { get; set; }

        public string Skype { get; set; }

        public DateTime LastLoginDate { get; set; }

        public int NumberOfLogins { get; set; }

        public int NumberOfProfileViews { get; set; }
    }
}