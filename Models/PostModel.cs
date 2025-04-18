namespace backend.Models;

public class Post
{
    public string Id { get; set; } = $"u-{Guid.NewGuid().ToString().Substring(0, 6)}";
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string[] Attachments { get; set; } = Array.Empty<string>();
    public string AuthorId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
