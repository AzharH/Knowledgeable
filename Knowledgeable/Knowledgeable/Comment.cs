//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Knowledgeable
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public System.Guid CommentID { get; set; }
        public System.Guid ArticleID { get; set; }
        public System.Guid UserID { get; set; }
        public string Comment1 { get; set; }
        public System.DateTime DatePosted { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }
    }
}
