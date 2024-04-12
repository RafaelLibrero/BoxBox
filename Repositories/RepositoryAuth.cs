using BoxBox.Data;
using BoxBox.Helpers;
using BoxBox.Models;
using Microsoft.EntityFrameworkCore;

namespace BoxBox.Repositories
{
    public class RepositoryAuth : IRepositoryAuth
    {
        private BoxBoxContext context;

        public RepositoryAuth(BoxBoxContext context)
        {
            this.context = context;
        }

        public async Task<User> Register(string userName, string email, string password)
        {
            User user = new User();
            user.UserId = await this.context.Users.MaxAsync(x => x.UserId) + 1;
            user.UserName = userName;
            user.Email = email.ToLower();
            user.Salt = HelperTools.GenerateSalt();
            user.Password = HelperCryptography.EncryptPassword(password, user.Salt);
            user.RegistrationDate = DateTime.Today;
            user.LastAccess = DateTime.UtcNow;
            user.RolId = 2;
            user.ProfilePicture = "";
            user.DriverId = null;
            user.TeamId = null;

            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();

            return user;
        }

        public async Task<User> LoginUserAsync(string email, string password)
        {
            User user = await
                this.context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                return null;
            }
            else
            {
                string salt = user.Salt;
                byte[] temp =
                    HelperCryptography.EncryptPassword(password, salt);
                byte[] passUser = user.Password;
                bool response =
                    HelperTools.CompareArrays(temp, passUser);
                if (response == true)
                {
                    user.LastAccess = DateTime.UtcNow;
                    await this.context.SaveChangesAsync();
                    return user;
                }
                else
                {
                    return null;
                }
            }


        }
    }
}
