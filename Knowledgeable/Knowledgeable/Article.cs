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
    
    public partial class Article
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
