using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knowledgeable.Models
{
    public class SubCommentModel
    {
        public System.Guid SubCommentID { get; set; }
        public System.Guid CommentID { get; set; }
        public System.Guid UserID { get; set; }
        public string userName { get; set; }

        public string ProfilePicture { get; set; }

        public string SubComment1 { get; set; }
        public System.DateTime DatePosted { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }
    }
}