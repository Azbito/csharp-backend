using backend.DTOs;
using backend.Interfaces;
using backend.Models;

namespace backend.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _repository;

    public PostService(IPostRepository repository)
    {
        _repository = repository;
    }

    public Post Create(DTOCreatePost dto)
    {
        var post = new Post
        {
            Title = dto.Title,
            Description = dto.Description,
            AuthorId = dto.AuthorId,
            Attachments = dto.Attachments,
        };

        _repository.Add(post);

        return post;
    }
}
