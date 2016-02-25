using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Knowledgeable.Models
{
    public class UsersModel
    {
        public System.Guid UserID { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Salt { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        public bool Active { get; set; }
        public string ProfilePicture { get; set; }
    }

    public class ProfileModel
    {
        public System.Guid UserID { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        public string ProfilePicture { get; set; }
        [Display(Name = "Upload Picture")]
        public HttpPostedFileBase PictureFile { get; set; }

        [Display(Name = "Articles posted")]
        public int NumArticles { get; set; }
        [Display(Name = "Shared Articles")]
        public int NumShared { get; set; }
    }

    public class LoginModel
    {
        public System.Guid UserID { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        
    }

    public class UserRegModel
    {
        public System.Guid UserID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(25, MinimumLength = 6, ErrorMessage = "Password length is betwween 6 and 25")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
    }


}