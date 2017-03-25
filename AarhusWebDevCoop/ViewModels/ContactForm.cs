using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AarhusWebDevCoop.ViewModels
{
    public class ContactForm
    {
        [Required] // bruges til validering
        public string Name { get; set; }

        [Required] // bruges til validering
        [EmailAddress(ErrorMessage = "Dette er ikke en valid email adresse")] // bruges til validering af email
        public string Email { get; set; }

        [Required] // bruges til validering
        public string Subject { get; set; }

        [Required] // bruges til validering
        public string Message { get; set; }
    }
}