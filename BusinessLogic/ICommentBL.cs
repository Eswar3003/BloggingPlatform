using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface ICommentBL
    {
        Task<CommentDto> AddCommentToPostAsync(CommentDto commentDto, int userId);

        Task<List<CommentDto>> RetrieveAllCommentsForPostAsync(int postId);

        Task<CommentDto> EditCommentByIdAsync(int commentId, string content);

        Task<string> DeleteCommentByIdAsync(int commentId);

        Task<CommentDto> GetCommentByIdAsync(int commentId);

    }
}
