using Centhora_Hotels.Models.DTO;

namespace Centhora_Hotels.InternalServices.CenthoraAuth
{
    public interface ICenthoraAuth
    {
        CenthoraAuthDto AuthenticateUser(string username, string password);
    }
}
