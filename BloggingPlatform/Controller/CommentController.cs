using BloggingPlatform.Helper;
using BusinessLogic;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace BloggingPlatform.Controller
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly ICommentBL _commentBL;

        public CommentController(ICommentBL commentBL)
        {
            _commentBL = commentBL;
        }

        [Authorize]
        [HttpPost("addcomment")]
        public async Task<IActionResult> AddCommentToPostAsync(CommentDto commentDto)
        {
            var loggedInUserDetail = (UserDto)HttpContext.Items["User"]!;

            CommentDto response = await _commentBL.AddCommentToPostAsync(commentDto, loggedInUserDetail.Id);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("retrieveallcommentforpost/{id}")]
        public async Task<IActionResult> RetrieveAllCommentsForPostAsync(int id)
        {
            var loggedInUserDetail = (UserDto)HttpContext.Items["User"]!;

            List<CommentDto> response = await _commentBL.RetrieveAllCommentsForPostAsync(id);
            return Ok(response);
        }


        [Authorize]
        [HttpGet("editcommentbyid/{id}/{content}")]
        public async Task<IActionResult> EditCommentByIdAsync(int id, string content)
        {
            var loggedInUserDetail = (UserDto)HttpContext.Items["User"]!;
            CommentDto response = new();
            var comment = await _commentBL.GetCommentByIdAsync(id);

            if (loggedInUserDetail.Id == comment.UserId)
                response = await _commentBL.EditCommentByIdAsync(id, content);
            else
                return BadRequest(new { message = "You are not authorized" });
            return Ok(response);
        }


        [Authorize]
        [HttpGet("deletecomment/{id}")]
        public async Task<IActionResult> DeleteCommentByIdAsync(int id)
        {
            var loggedInUserDetail = (UserDto)HttpContext.Items["User"]!;
            var comment = await _commentBL.GetCommentByIdAsync(id);
            var response = "";
            if (loggedInUserDetail.Id == comment.UserId)
                response = await _commentBL.DeleteCommentByIdAsync(id);
            else return BadRequest(new { message = "You are not authorized" });
            return Ok(response);
        }


    }
}
