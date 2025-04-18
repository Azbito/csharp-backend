using backend.DTOs;

namespace backend.Interfaces;

public interface IPostController
{
    IResult CreatePost(DTOCreatePost dto);
}
