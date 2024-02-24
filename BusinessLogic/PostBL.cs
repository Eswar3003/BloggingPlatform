using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class PostBL : IPostBL
    {

        private readonly IPostDAL _postDAL;

        public PostBL(IPostDAL postDAL)
        {
            _postDAL = postDAL;
        }

        public async Task<PostDto> CreateNewPostAsync(int id, PostContentsDto postContentsDto)
        {
            var post = await _postDAL.CreateNewPostAsync(id, postContentsDto);
            return post;
        }

        public async Task<string> DeletePostAsyncById(int id)
        {
            var post = await _postDAL.DeletePostAsyncById(id);
            return post;
        }

        public async Task<List<PostDto>> GetAllPostsAsync()
        {
            var listPost = await _postDAL.GetAllPostsAsync();
            return listPost;
        }

        public async Task<PostDto> GetPostAsyncById(int id)
        {
            var post = await _postDAL.GetPostAsyncById(id);
            return post;
        }

        public async Task<PostDto> UpdatePostAsync(int id, PostContentsDto postContentsDto)
        {
            var post = await _postDAL.UpdatePostAsync(id, postContentsDto);
            return post;
        }
    }
}
