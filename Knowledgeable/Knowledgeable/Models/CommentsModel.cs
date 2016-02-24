using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knowledgeable.Models
{
    public class CommentsModel
    {
        public List<Comment> listComment { get; set; }
        public List<SubComment> listSubcomment { get; set; }
    }
}
