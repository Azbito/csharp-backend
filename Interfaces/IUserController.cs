using backend.DTOs;

namespace backend.Interfaces;

public interface IUserControllers
{
    IResult CreateUser(DTOCreateUser dto);
    IResult GetAllUsers();
    IResult GetUserById(string id);
    IResult GetUserByUsername(string username);
}
