using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CWSStart.Web.Models
{
    public class ContactFormViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(20, ErrorMessage = "Your message is less than 20 characters. Please provide more details to your message.")]
        public string Message { get; set; }

    }
}