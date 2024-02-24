using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IPostBL
    {
        Task<PostDto> CreateNewPostAsync(int id, PostContentsDto postContentsDto);

        Task<PostDto> UpdatePostAsync(int id, PostContentsDto postContentsDto);

        Task<PostDto> GetPostAsyncById(int id);

        Task<string> DeletePostAsyncById(int id);

        Task<List<PostDto>> GetAllPostsAsync();

    }
}
