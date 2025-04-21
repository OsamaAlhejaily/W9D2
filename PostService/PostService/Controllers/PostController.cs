using Microsoft.AspNetCore.Mvc;
using PostService.DTOs;
using System;
using System.Collections.Generic;

namespace PostService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private static readonly List<PostDto> _posts = new List<PostDto>();

        [HttpPost]
        public ActionResult<PostDto> CreatePost(CreatePostDto createPostDto)
        {
            var post = new PostDto
            {
                Id = Guid.NewGuid().ToString(),
                Title = createPostDto.Title,
                Content = createPostDto.Content
            };

            _posts.Add(post);
            return post;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PostDto>> GetPosts()
        {
            return _posts;
        }

        [HttpGet("{id}")]
        public ActionResult<PostDto> GetPost(string id)
        {
            var post = _posts.Find(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return post;
        }

        // Additional endpoint for CommentService to validate posts
        [HttpGet("validate/{id}")]
        public ActionResult ValidatePost(string id)
        {
            var post = _posts.Find(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}