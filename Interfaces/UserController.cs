using backend.DTOs;

namespace backend.Interfaces;

public interface IUserControllers
{
    IResult CreateUser(CreateUser dto);
    IResult GetAllUsers();
    IResult GetUserById(string id);
    IResult GetUserByUsername(string username);
}
