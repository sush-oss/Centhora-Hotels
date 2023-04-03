using Centhora_Hotels.DB_Context;
using Centhora_Hotels.Models.DTO;

namespace Centhora_Hotels.InternalServices.CenthoraAuth
{
    public class CenthoraAuth : ICenthoraAuth
    {
        private readonly CenthoraDbContext _context;

        public CenthoraAuth(CenthoraDbContext context)
        {
            _context = context;
        }
        public CenthoraAuthDto AuthenticateUser(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == username && u.UserPassword == password);

            if (user == null)
            {
                return null;
            }

            return new CenthoraAuthDto { UserName = username, UserPassword = password };
        }
    }
}
