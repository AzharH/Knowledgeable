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
        public string Hex { get; set; }
    }
    public class CategorySharedModel
    {
        public System.Guid CategoryID { get; set; }
        public string Name { get; set; }
        public System.Guid ColourID { get; set; }
        public string Hex { get; set; }
    }

    public class indexCategory
    {
        public List<CategoryModel> Category { get; set; }
        public List<CategorySharedModel> CategoryShared { get; set; }
    }
}