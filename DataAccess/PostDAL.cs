using Entities;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PostDAL : DatabaseContext, IPostDAL
    {

        private readonly DatabaseContext _dbContext;

        public PostDAL(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<PostDto> CreateNewPostAsync(int id, PostContentsDto postContentsDto)
        {
            using(var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    Post post = new()
                    {
                        Title = postContentsDto.Title,
                        Content = postContentsDto.Content,
                        DateCreated = DateTime.UtcNow,
                        UserId = id
                    };

                    _dbContext.Post.Add(post);
                    await _dbContext.SaveChangesAsync();

                    var savedPost = _dbContext.Post.FirstOrDefault(p => p.UserId == id
                    && p.Title == postContentsDto.Title);

                    if (savedPost == null) return null;

                    PostDto postDto = new()
                    {
                        Id = savedPost!.Id,
                        Title = savedPost.Title,
                        Content = savedPost.Content,
                        UserId = savedPost.UserId,
                        DateCreated = savedPost.DateCreated
                    };

                    transaction.Commit();

                    return postDto;

                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    transaction.Rollback();
                }
               
            }

            return null;

        }

        public async Task<string> DeletePostAsyncById(int id)
        {
            Post? post = await _dbContext.Post.FindAsync(id)!;
            if (post == null) return null;

            _dbContext.Post.Remove(post!);
            await _dbContext.SaveChangesAsync();

            return "Deleted Successfully";
        }

        public async Task<List<PostDto>> GetAllPostsAsync()
        {
            var postList = _dbContext.Post.ToList();

            List<PostDto> postListDto = new();

            foreach(var post in postList)
            {
                PostDto postDto = new()
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    UserId = post.UserId,
                    DateCreated = post.DateCreated,
                };
                postListDto.Add(postDto);
            }

            await Task.CompletedTask;
            return postListDto;
            
        }

        public async Task<PostDto> GetPostAsyncById(int id)
        {
            Post? post = await _dbContext.Post.FindAsync(id)!;
            if (post == null) return null;

            PostDto postDto = new()
            {
                Id = post!.Id,
                Title = post!.Title,
                Content = post.Content,
                DateCreated = post.DateCreated,
                UserId = post.UserId,
            };

            return postDto;
        }

        public async Task<PostDto> UpdatePostAsync(int id, PostContentsDto postContentsDto)
        {
            using(var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    Post? post = await _dbContext.Post.FindAsync(id)!;
                    if (post == null) return null;

                    post.Title = postContentsDto.Title;
                    post.Content = postContentsDto.Content;

                    _dbContext.Post.Update(post);
                    await _dbContext.SaveChangesAsync();

                    var updatedPost = _dbContext.Post.FirstOrDefault(p => p.Id ==  id && p.Content.Equals(postContentsDto.Content));

                    PostDto postDto = new()
                    {
                        Id = updatedPost!.Id,
                        Title = updatedPost!.Title,
                        Content = updatedPost.Content,
                        DateCreated = updatedPost.DateCreated,
                        UserId = updatedPost.UserId,
                    };

                    transaction.Commit();

                    return postDto;
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    transaction.Rollback();
                }
            }

            return null;
        }
    }
}
