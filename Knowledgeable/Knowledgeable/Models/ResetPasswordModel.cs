using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Knowledgeable.Models
{
    public class ResetPasswordModel
    {
        public System.Guid UserID { get; set; }
        public System.Guid ResetID { get; set; }
    }

    public class ResetModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}