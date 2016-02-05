using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knowledgeable.Models
{
    public class ShareModel
    {
        public System.Guid ShareID { get; set; }
        public System.Guid ArticleID { get; set; }
        public System.Guid UserID { get; set; }
    }
}