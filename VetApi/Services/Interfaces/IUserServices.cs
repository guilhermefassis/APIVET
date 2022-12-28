using System.Threading.Tasks;
using FluentResults;
using VetApi.Data;
using VetApi.DTO;

namespace VetApi.Services.Interfaces
{
    public interface IUserServices
    {
        Task<Result> RegisterUser(RegisterUserDTO model);
        Task RegisterVeterinarian(RegisterUserDTO model);
        Result Logout();
        Task<Result> LogIn(LoginUserDTO userInfo);
        Result RequestResetPassword(RequestReset request);
        Result MakeResetPassword(MakeResetRequest request);
    }
}