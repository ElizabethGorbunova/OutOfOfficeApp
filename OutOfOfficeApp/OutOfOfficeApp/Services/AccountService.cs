using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OutOfOfficeApp.Entities;
using OutOfOfficeApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OutOfOfficeApp.Exceptions;

namespace OutOfOfficeApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly OOODbContext dbContext;
        private readonly IMapper mapper;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly AuthenticationSettings authenticationSettings;

        public AccountService(OOODbContext _dbContext, IMapper _mapper, IPasswordHasher<User> _passwordHasher, AuthenticationSettings _authenticationSettings)
        {
            dbContext = _dbContext;
            mapper = _mapper;
            passwordHasher = _passwordHasher;
            authenticationSettings = _authenticationSettings;
        }
        public void RegisterUser(RegisterUserDtoIn userDtoIn)
        {
            
            var newUser = new User()
            {
                Email = userDtoIn.Email,
                FirstName = userDtoIn.FirstName,
                LastName = userDtoIn.LastName,
                RoleId = userDtoIn.RoleId,
            };

            var hashedPassword = passwordHasher.HashPassword(newUser, userDtoIn.Password);
            newUser.PasswordHash = hashedPassword;
            dbContext.Users.Add(newUser);
            dbContext.SaveChanges();
        }
        public string GenerateJwt(LoginDtoIn login)
        {
            var user = dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == login.Email);
            if (user == null)
            {
                throw new BadRequestException("Invalid username or password");
            };

            var passwordResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);
            if (passwordResult == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,  user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Role.Name),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(authenticationSettings.JwtIssuer,
                authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);


            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
