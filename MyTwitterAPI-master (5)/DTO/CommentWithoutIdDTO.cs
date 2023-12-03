namespace MyTwitterAPI.DTO
{
    public class CommentWithoutIdDTO
    {
        public string? CommentText { get; set; }
        public string? UserId { get; set; }
        public int PostId { get; set; }
    }
}
