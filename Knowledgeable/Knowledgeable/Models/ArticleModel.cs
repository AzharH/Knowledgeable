using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knowledgeable.Models
{
    public class ArticleModel
    {
        public System.Guid ArticleID { get; set; }
        public System.Guid UserID { get; set; }
        public System.Guid CategoryID { get; set; }
        public string Title { get; set; }
        public System.DateTime DatePosted { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string Article1 { get; set; }
    }
}