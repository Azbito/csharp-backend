using backend.DTOs;

namespace backend.Interfaces;

public interface IUserControllers
{
    IResult CreateUser(CreateUser dto);
    IResult GetAllUsers();
    IResult GetUserById(Guid id);
    IResult GetUserByUsername(string username);
}
