﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KnowledgeableDBEntities : DbContext
    {
        public KnowledgeableDBEntities()
            : base("name=KnowledgeableDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Share> Shares { get; set; }
        public virtual DbSet<SubComment> SubComments { get; set; }
        public virtual DbSet<Colour> Colours { get; set; }
        public virtual DbSet<ResetPassword> ResetPasswords { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
