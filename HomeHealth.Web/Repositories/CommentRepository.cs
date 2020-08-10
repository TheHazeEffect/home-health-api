using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HomeHealth.Web.Data.Tables;
using HomeHealth.Web.Interfaces;
using HomeHealth.Web.Data;
using Serilog;


namespace HomeHealth.Web.Repositories
{
    public class CommentRepository : ICommentsRepository
    {
        private readonly HomeHealthDbContext _context;

        public async Task<ICollection<Comments>> getAllComments()
        {
            try{

                Log.Debug("Entered GetAllComments Repository");
                Log.Debug("All Comments being Queried");
                var comments = await _context.Comments.ToListAsync();

                Log.Debug("Query Complete");
                return comments;

            }catch(Exception ex){

                var LogMsg = "GetAllComments Repository Failed";
                Log.Error(LogMsg, ex);
                throw new Exception(LogMsg, innerException: ex);

            }
        }

        public async Task<Comments> GetComment(int id)
        {
            try {
                Log.Debug("Entered Get Comment Repository");
                Log.Debug("Comment queries by ID");

                var comment = await _context.Comments.FindAsync(id);

                return comment;

            }catch(Exception ex){

                var logMsg = "GetComment Repository failed with ID of " + id;
                Log.Error(logMsg,ex);
                throw new Exception(logMsg,innerException: ex);

            }
        }

        public async Task<Comments> AddComment(Comments newComment)
        {
            try{
                Log.Debug("Add Comment Entered");

                await _context.Comments.AddAsync(newComment);

                Log.Debug("Comment Marked to be added");

                await _context.SaveChangesAsync();

                Log.Debug("Changes Saved");

                return newComment;

            }catch(Exception ex)
            {
                var logMsg = "Add Comment Repository Failed";
                Log.Error(logMsg,ex);
                throw new Exception(logMsg,innerException: ex);

            }
        }

        public async Task<bool> DeleteComment(int id)
        {
            try{

                Log.Debug("Entered Delete Comment");

                var deletedComment = await _context.Comments.FindAsync(id);

                Log.Debug("Comment to delete Found");

                _context.Comments.Remove(deletedComment);

                Log.Debug("Comment Tagged for deletion");

                await _context.SaveChangesAsync();

                Log.Debug("Changes Saved");

                return true;


            }catch(Exception ex)
            {

                var logMsg = "Delete Comment Repository Failed with Id" + id;
                Log.Error(logMsg,ex);
                throw new Exception(logMsg,innerException: ex);

            }
        }

        public async Task<bool> UpdateComment(int id,Comments Update)
        {
            try{

                Log.Debug("Entered Update Repository");
                Log.Debug("Find COmment to Update");

                var comment = await _context.Comments.FindAsync(id);

                if(comment == null) {

                    Log.Debug("Comment does not exist with id " + id);

                    throw new Exception("Comment Does not Exis with id " + id);
                }

                Log.Debug("Comment Found");

                comment.Content = Update.Content;

                Log.Debug("Comment updated");

                _context.Entry(comment).State = EntityState.Modified;

                Log.Debug("Comment marked for update");

                await _context.SaveChangesAsync();

                Log.Debug("Changes commited");

                return true;

            }catch(Exception ex)
            {
                var logMsg = "Update Comment Repository Failed with Id" + id;
                Log.Error(logMsg,ex);
                throw new Exception(logMsg,innerException: ex);

            }

        }
    }
                   
}
    
