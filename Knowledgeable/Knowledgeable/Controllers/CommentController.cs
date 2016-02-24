using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Knowledgeable;
using Knowledgeable.Models;

namespace Knowledgeable.Controllers
{
    public class CommentController : Controller
    {
        private KnowledgeableDBEntities db = new KnowledgeableDBEntities();

        // GET: Comment
        public PartialViewResult LoadComment(Guid id)
        {
            Guid UserID = new Guid(User.Identity.Name);

            Comment comment = db.Comments.Find(id);

            CommentsModel commentsModel = new CommentsModel();

            List<Comment> listComment = db.Comments.Where(x => x.ArticleID == id).ToList();
            List<SubComment> listSubcomment = db.SubComments.Where(x => x.CommentID == comment.CommentID).ToList();



            List<Comment> newlistcomment = new List<Comment>();
            foreach (Comment item in listComment)
            {
                newlistcomment.Add(new Comment(){
                    CommentID = item.CommentID,
                    ArticleID = item.ArticleID,
                    UserID = item.UserID,
                    Comment1 = item.Comment1,
                    DatePosted = item.DatePosted,
                    UpVote = item.UpVote,
                    DownVote = item.DownVote
                });
            }

            List<SubComment> newlistSubcomment = new List<SubComment>();
            foreach (SubComment item in listSubcomment)
            {
                newlistSubcomment.Add(new SubComment()
                {
                    SubCommentID = item.SubCommentID,
                    CommentID = item.CommentID,
                    UserID = item.UserID,
                    SubComment1 = item.SubComment1,
                    DatePosted = item.DatePosted,
                    UpVote = item.UpVote,
                    DownVote = item.DownVote
                });
            }

            commentsModel.listComment = newlistcomment;
            commentsModel.listSubcomment = newlistSubcomment;

                return PartialView("_LoadComment", commentsModel);
        }
    }
}