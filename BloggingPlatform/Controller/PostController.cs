using BloggingPlatform.Helper;
using BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace BloggingPlatform.Controller
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly IPostBL _postBL;

        public PostController(IPostBL postBL)
        {
            _postBL = postBL;
        }


        [Authorize]
        [HttpPost("createpost")]
        public async Task<IActionResult> CreateNewPostAsync(PostContentsDto postContentsDto)
        {
            var loggedInUserDetail = (UserDto)HttpContext.Items["User"]!;
                        
            PostDto response = await _postBL.CreateNewPostAsync(loggedInUserDetail.Id, postContentsDto);
            return Ok(response);
        }


        [Authorize]
        [HttpGet("getallpost")]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            var loggedInUserDetail = (UserDto)HttpContext.Items["User"]!;
            List<PostDto> response = new();

            if (loggedInUserDetail.Id == 1) response = await _postBL.GetAllPostsAsync();
            else return BadRequest(new { message = "You are not authorized" });
            return Ok(response);
        }

        [Authorize]
        [HttpGet("getpostbyid/{id}")]
        public async Task<IActionResult> GetPostAsyncById(int id)
        {
            var loggedInUserDetail = (UserDto)HttpContext.Items["User"]!;
            PostDto response = new();

            var post = await _postBL.GetPostAsyncById(id);
            if (post == null) return BadRequest(new { message = "Post not exist" });

            if (loggedInUserDetail.Id == 1 || loggedInUserDetail.Id == post.UserId) response = post;
            else return BadRequest(new { message = "You are not authorized" });
            return Ok(response);
        }


        [Authorize]
        [HttpPut("updatepostbyid/{id}")]
        public async Task<IActionResult> UpdatePostAsync(int id, PostContentsDto postContentsDto)
        {
            var loggedInUserDetail = (UserDto)HttpContext.Items["User"]!;
            PostDto response = new();

            var post = await _postBL.GetPostAsyncById(id);
            if (post == null) return BadRequest(new { message = "Post not exist" });

            if (loggedInUserDetail.Id == 1 || loggedInUserDetail.Id == post.UserId)
                response = await _postBL.UpdatePostAsync(id, postContentsDto);
            else return BadRequest(new { message = "You are not authorized" });
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("deletepostbyid/{id}")]
        public async Task<IActionResult> DeletePostAsyncById(int id)
        {
            var loggedInUserDetail = (UserDto)HttpContext.Items["User"]!;
            var response = "";

            var post = await _postBL.GetPostAsyncById(id);
            if (post == null) return BadRequest(new { message = "Post not exist" });

            if (loggedInUserDetail.Id == 1 || loggedInUserDetail.Id == post.UserId)
                response = await _postBL.DeletePostAsyncById(id);
            else return BadRequest(new { message = "You are not authorized" });
            return Ok(response);
        }

    }
}
