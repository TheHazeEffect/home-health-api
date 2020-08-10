using  System.Threading.Tasks;
using System.Collections.Generic;
using HomeHealth.Web.Data.Tables;

namespace HomeHealth.Web.Interfaces
{
    public interface ICommentsRepository
    {
        Task<ICollection<Comments>> getAllComments();
        Task<Comments> GetComment(int id);
        Task<Comments> AddComment(Comments newComment);
        Task<bool> DeleteComment(int id);
        Task<bool> UpdateComment(int id,Comments update);
        
    }
    
}