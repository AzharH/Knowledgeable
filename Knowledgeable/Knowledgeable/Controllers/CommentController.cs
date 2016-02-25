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


            List<Comment> comment = db.Comments.Where(x => x.ArticleID == id).ToList();
            List<CommentModel> commentModel = new List<CommentModel>();
            foreach(var item in comment)
            {
                List<SubComment> subComment = db.SubComments.Where(x => x.CommentID == item.CommentID).ToList();
                List<SubCommentModel> subCommentModel = new List<SubCommentModel>();
                User user = new User();
                string name;
                foreach (var sub in subComment)
                {
                    user = db.Users.Find(sub.UserID);
                    name = user.Name + " " + user.Surname;

                    subCommentModel.Add(new SubCommentModel
                    {

                        CommentID = sub.CommentID,
                        DatePosted = sub.DatePosted,
                        DownVote = sub.DownVote,
                        SubComment1 = sub.SubComment1,
                        SubCommentID = sub.CommentID,
                        UpVote = sub.UpVote,
                        UserID = sub.UserID,
                        userName = name
                    });
                }

                user = db.Users.Find(item.UserID);
                name = user.Name + " " + user.Surname;

                commentModel.Add(new CommentModel
                {
                    ArticleID = item.ArticleID,
                    UserID = item.UserID,
                    userName = name,
                    Comment1 = item.Comment1,
                    CommentID = item.CommentID,
                    DatePosted = item.DatePosted,
                    DownVote = item.DownVote,
                    UpVote = item.UpVote,
                    listSubcomment = subCommentModel
                });

            }

            return PartialView("_LoadComment", commentModel);
        }
    }
}