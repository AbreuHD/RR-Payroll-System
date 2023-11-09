using Core.Application.DTOs.Account;
using Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interface.Services
{
    public interface IUserService
    {
        Task<AuthenticationResponse> Login(LoginViewModel loginViewModel);
        Task<RegisterResponse> RegisterClient(UserSaveViewModel userSaveViewModel);
        Task<RegisterResponse> RegisterAdmin(UserSaveViewModel userSaveViewModel);
        Task<string> ConfirmEmail(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel);
        Task<ResetPasswordResponse> ResetPassword(ResetPasswordViewModel resetPasswordViewModel);
        Task SingOut();
        Task<List<UserGetAllViewModel>> GetAllClients();
        Task<UserSaveViewModel> GetAccountByid(string ID);
        Task<bool> IsAdmin(string ID);
        Task<string> GetSavingByID(string id);
        Task<RegisterResponse> UpdateClient(UserSaveViewModel userSaveViewModel);
        Task<RegisterResponse> UpdateAdmin(UserSaveViewModel userSaveViewModel);
        Task ChangeStatus(string Id);

    }
}
