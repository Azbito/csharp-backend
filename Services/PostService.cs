using backend.DTOs;
using backend.Interfaces;
using backend.Models;

namespace backend.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public PostService(IPostRepository repository, IUserRepository userRepository)
    {
        _postRepository = repository;
        _userRepository = userRepository;
    }

    public Post? Create(DTOCreatePost dto, string authorId)
    {
        var user = _userRepository.GetById(authorId);

        if (user == null)
        {
            return null;
        }

        var post = new Post
        {
            Title = dto.Title,
            Description = dto.Description,
            AuthorId = authorId,
            Attachments = dto.Attachments,
        };

        _postRepository.Add(post);

        return post;
    }
}
