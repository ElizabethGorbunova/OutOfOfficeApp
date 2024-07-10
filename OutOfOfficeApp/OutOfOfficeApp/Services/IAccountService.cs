using OutOfOfficeApp.Models;

namespace OutOfOfficeApp.Services
{
    public interface IAccountService
    {
        abstract void RegisterUser(RegisterUserDtoIn userDtoIn);
        public string GenerateJwt(LoginDtoIn login);
    }
}