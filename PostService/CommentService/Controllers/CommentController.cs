using Microsoft.AspNetCore.Mvc;
using PostService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CommentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private static readonly List<CommentDto> _comments = new List<CommentDto>();
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CommentsController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<CommentDto>> CreateComment(CreateCommentDto createCommentDto)
        {
            // Validate that the post exists before creating a comment
            var postServiceUrl = _configuration["PostServiceUrl"];
            var httpClient = _httpClientFactory.CreateClient();

            try
            {
                var response = await httpClient.GetAsync($"{postServiceUrl}/posts/validate/{createCommentDto.PostId}");
                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest($"Post with ID {createCommentDto.PostId} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(503, $"Unable to validate post: {ex.Message}");
            }

            var comment = new CommentDto
            {
                Id = Guid.NewGuid().ToString(),
                PostId = createCommentDto.PostId,
                Author = createCommentDto.Author,
                Text = createCommentDto.Text
            };

            _comments.Add(comment);
            return comment;
        }

        [HttpGet("{postId}")]
        public ActionResult<IEnumerable<CommentDto>> GetCommentsByPostId(string postId)
        {
            var comments = _comments.Where(c => c.PostId == postId).ToList();
            return comments;
        }
    }
}