using Entities;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CommentDAL : DatabaseContext, ICommentDAL
    {

        private readonly DatabaseContext _dbContext;

        public CommentDAL(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommentDto> AddCommentToPostAsync(CommentDto commentDto, int userId)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    Comment comment = new()
                    {
                        Content = commentDto.Content,
                        PostId = commentDto.PostId,
                        UserId = userId,
                        DateCreated = DateTime.UtcNow
                    };

                    _dbContext.Comment.Add(comment);
                    await _dbContext.SaveChangesAsync();

                    var savedComment = _dbContext.Comment.FirstOrDefault(c => c.UserId.Equals(commentDto.UserId)
                    && c.PostId.Equals(commentDto.PostId) && c.Content.Equals(commentDto.Content));

                    CommentDto commentDto1 = new()
                    {
                        Id = savedComment!.Id,
                        Content = savedComment.Content,
                        UserId = savedComment.UserId,
                        PostId = savedComment.PostId,
                        DateCreated = savedComment.DateCreated
                    };

                    transaction.Commit();

                    return commentDto1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    transaction.Rollback();
                }
            }

            return null;
        }

        public async Task<string> DeleteCommentByIdAsync(int commentId)
        {
            var comment = await _dbContext.Comment.FirstOrDefaultAsync(c => c.Id == commentId);
            if (comment == null) return null;

            _dbContext.Comment.Remove(comment!);
            await _dbContext.SaveChangesAsync();

            return "Deleted Successfully";
        }

        public async Task<CommentDto> EditCommentByIdAsync(int commentId, string content)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    Comment? comment = await _dbContext.Comment.FindAsync(commentId);
                    if (comment == null) return null;

                    comment.Content = content;
                    comment.DateCreated = DateTime.UtcNow;

                    _dbContext.Comment.Update(comment);
                    await _dbContext.SaveChangesAsync();

                    var updatedComment = _dbContext.Comment.FirstOrDefault(c => c.Id == commentId 
                    && c.Content.Equals(content));

                    CommentDto commentDto = new()
                    {
                        Id = updatedComment!.Id,
                        Content = updatedComment.Content,
                        DateCreated = updatedComment.DateCreated,
                        PostId = updatedComment.PostId,
                        UserId = updatedComment.UserId
                    };

                    transaction.Commit();

                    return commentDto;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    transaction.Rollback();
                }
            }

            return null;
        }

        public async Task<CommentDto> GetCommentByIdAsync(int commentId)
        {
            var comment = await _dbContext.Comment.FindAsync(commentId);
            if (comment == null) return null;

            CommentDto commentDto = new()
            {
                Id = comment.Id,
                Content = comment.Content,
                DateCreated = comment.DateCreated,
                PostId = comment.PostId,
                UserId = comment.UserId
            };

            return commentDto;
        }

        public async Task<List<CommentDto>> RetrieveAllCommentsForPostAsync(int postId)
        {
            var comments = await _dbContext.Comment.Where(c => c.PostId.Equals(postId)).ToListAsync();

            List<CommentDto> commentListDto = new();

            foreach (var comment in comments)
            {
                CommentDto commentDto = new()
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    UserId = comment.UserId,
                    PostId = comment.PostId,
                    DateCreated = comment.DateCreated
                };

                commentListDto.Add(commentDto);
            }

            return commentListDto;
        }
    }
}
