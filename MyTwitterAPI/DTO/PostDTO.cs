namespace MyTwitterAPI.DTO
{
    public class PostDTO
    {
        public int PostId { get; set; }
        public string? UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Img { get; set; }
        public DateTime DateandTime { get; set; }
        public string? UserName { get; set; }
        public string? UserType { get; set; }
    }
}
