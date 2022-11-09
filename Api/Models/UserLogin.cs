using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class UserLogin
    {
        [Required]
        [Display(Name = "Phone")]
        //[DataType(DataType.PhoneNumber)]
        [RegularExpression("^01[0-2,5]{1}[0-9]{8}$", ErrorMessage = "Entered Mobile No is not valid.")]
        public string Phone { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        public string UserType { get; set; }
    }
}