using backend.DTOs;
using backend.Models;

public interface IPostService
{
    Post? Create(DTOCreatePost dto, string authorId);
    bool Delete(DTODeletePost dto, string authorId);
}
