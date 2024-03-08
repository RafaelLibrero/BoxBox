using BoxBox.Data;
using BoxBox.Models;
using Microsoft.EntityFrameworkCore;

namespace BoxBox.Repositories
{
    public class RepositoryAuth
    {
        private BoxBoxContext context;

        public RepositoryAuth(BoxBoxContext context)
        {
            this.context = context;
        }

        public async Task Register(User usuario)
        {
            User user = new User();
            user.UserId = await this.context.Users.MaxAsync(x => x.UserId) + 1;
            user.UserName = usuario.UserName;
            user.Email = usuario.Email;
            user.Password = usuario.Password;
            user.RegistrationDate = usuario.RegistrationDate;
            user.LastAccess = usuario.LastAccess;
            user.RolId = usuario.RolId;
            user.Salt = usuario.Salt;
            user.Estado = usuario.Estado;

            this.context.Users.Add(user);
            this.context.SaveChanges();
        }
    }
}
