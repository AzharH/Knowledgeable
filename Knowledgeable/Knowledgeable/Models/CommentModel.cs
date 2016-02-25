using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knowledgeable.Models
{
    public class CommentModel
    {
        public System.Guid CommentID { get; set; }
        public System.Guid ArticleID { get; set; }
        public System.Guid UserID { get; set; }
        public string userName { get; set; }
        public string ProfilePicture { get; set; }
        public string Comment1 { get; set; }
        public System.DateTime DatePosted { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }
        public List<SubCommentModel> listSubcomment { get; set; }

    }
}