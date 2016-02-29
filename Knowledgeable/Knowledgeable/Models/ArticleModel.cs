using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Knowledgeable.Models
{
    public class ArticleModel
    {
        public System.Guid ArticleID { get; set; }
        public System.Guid UserID { get; set; }
        public string Owner { get; set; }
        public System.Guid CategoryID { get; set; }
        public string category { get; set; }
        public string Title { get; set; }
        public System.DateTime DatePosted { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        [DisplayName("Content")]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Article1 { get; set; }
    }
}