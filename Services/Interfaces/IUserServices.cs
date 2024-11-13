using Common.Models;
using Data.Entities;


namespace Services.Interfaces
{
    public interface IUserServices
    {
        User? AuthUser(CredentialsDTO credentialsDTO);
        int CreateUser(NewUserDTO userDTO);
    }
}
