using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Facebook.CustomValidation;

namespace Facebook.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
        // to add new properties or methods

        [Display(Name = "Confirm Email")]
        [Required]
        [Compare("Email", ErrorMessage = "Email is not Matched")]
        public string ConfirmEmail { get; set; }


        [Display(Name = "Confirm Password")]
        [Required]
        [Compare("Password", ErrorMessage = "Password is not Matched")]
        public string ConfirmPassword { get; set; }
    }

    // meta data clas to asociate with data class model
    public class UserMetaData
    {
        // edit existed properties

        [Display(Name ="First Name")]
        [Required]
        public string Fname { get; set; }

        [Display(Name = "Last Name")]
        public string Lname { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        [RegularExpression(@"\b[\w\.-]+@[\w\.-]+\w{2,4}\b", ErrorMessage = "Invalid Email Format !")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        //[DataType(DataType.PhoneNumber)]
        public Nullable<int> Phone { get; set; }


        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat( DataFormatString = "{0:D}", ApplyFormatInEditMode = false)]
        public Nullable<System.DateTime> BirthDate { get; set; }

        [Required]
        public string Gender { get; set; }

        public string Photo { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Password Should be between 8-15 chracters")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\w]).{8,64})", ErrorMessage = "Password Should contain number, capital, litter and special character")]
        public string Password { get; set; }
    }
}
