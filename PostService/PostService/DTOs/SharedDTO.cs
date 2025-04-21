namespace PostService.DTOs
{
    public class PostDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public class CommentDto
    {
        public string Id { get; set; }
        public string PostId { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
    }

    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public class CreateCommentDto
    {
        public string PostId { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
    }
}