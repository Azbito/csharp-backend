namespace backend.DTOs;

public class DTOCreatePost
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string[] Attachments { get; set; } = Array.Empty<string>();
    public string AuthorId { get; set; } = string.Empty;
}
