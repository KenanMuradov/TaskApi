using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using TaskApi.Models;
using TaskApi.Models.DTO;
using TaskApi.Services.Interfaces;

namespace TaskApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IStorageManager _storageManager;

        public UserService(AppDbContext context, IStorageManager storageManager)
        {
            _context = context;
            _storageManager = storageManager;
        }

        public async Task<User?> FindUserByEmailAsync(string email) => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public bool Login(LoginDTO model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterAsync(RegisterDTO model)
        {
            try
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
                var photoId = await _storageManager.UploadFileAsync(model.ProfilePicture.OpenReadStream(), model.ProfilePicture.FileName, model.ProfilePicture.ContentType);

                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    Age = model.Age,
                    PasswordHash = passwordHash,
                    ProfilePhoto = photoId,
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
