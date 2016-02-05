using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knowledgeable.Models
{
    public class CategoryModel
    {
        public System.Guid CategoryID { get; set; }
        public System.Guid UserID { get; set; }
        public string Name { get; set; }
        public System.Guid ColourID { get; set; }
    }
}