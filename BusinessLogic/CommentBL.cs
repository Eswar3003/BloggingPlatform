using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class CommentBL : ICommentBL
    {

        private readonly ICommentDAL _commentDAL;

        public CommentBL(ICommentDAL commentDAL)
        {
            _commentDAL = commentDAL;
        }

        public async Task<CommentDto> AddCommentToPostAsync(CommentDto commentDto, int userId)
        {
            var comment = await _commentDAL.AddCommentToPostAsync(commentDto, userId); 
            return comment;
        }

        public async Task<string> DeleteCommentByIdAsync(int commentId)
        {
            var comment = await _commentDAL.DeleteCommentByIdAsync(commentId);
            return comment;
        }

        public async Task<CommentDto> EditCommentByIdAsync(int commentId, string content)
        {
            var comment = await _commentDAL.EditCommentByIdAsync(commentId, content);
            return comment;
        }

        public async Task<CommentDto> GetCommentByIdAsync(int commentId)
        {
            var comment = await _commentDAL.GetCommentByIdAsync(commentId);
            return comment;
        }

        public async Task<List<CommentDto>> RetrieveAllCommentsForPostAsync(int postId)
        {
            var commentListDto = await _commentDAL.RetrieveAllCommentsForPostAsync(postId);
            return commentListDto;
        }
    }
}
