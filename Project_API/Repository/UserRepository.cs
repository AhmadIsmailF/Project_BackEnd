using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Project_API.Data;
using Project_API.Models;
using Project_API.Models.Dto;
using Project_API.Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Project_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly AppSettings _appSettings;

        public bool IsValidPassword(string password)
        {
            // Password validation logic here
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";
            return Regex.IsMatch(password, pattern);
        }

        public UserRepository(ApplicationDbContext db, IOptions<AppSettings> appsetting)
        {
            _db = db;
            _appSettings = appsetting.Value;
        }
        public async Task<User> Authenticate(string email, string password)
        {
            var user =await _db.Users.SingleOrDefaultAsync(x => x.Email == email && x.Password == password);
            if (user == null)
            {
                return null;
            }

            //if user was found generate Jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    //new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = "";
            return user;
        }
        public async Task<bool> IsUniqueUser(string email)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return true;

            return false;
        }

        public async Task<User> Register(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            user.Password = "";
            return user;
        }
    }
}

