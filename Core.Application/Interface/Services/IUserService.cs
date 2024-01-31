using Core.Application.DTOs.Account;
using Core.Application.DTOs.Register;
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
        Task<RegisterResponse> Register(RegisterRequestDTO dTO);
        Task<RegisterResponse> Update(RegisterRequestDTO dTo);
        Task SingOut();
        Task ChangeStatus(string Id, int TipoUsuario);


        Task<string> ConfirmEmail(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel);
        Task<ResetPasswordResponse> ResetPassword(ResetPasswordViewModel resetPasswordViewModel);
        Task<List<UserGetAllViewModel>> GetAllClients();
        Task<UserSaveViewModel> GetAccountByid(string ID);
        Task<bool> IsAdmin(string ID);
        Task<string> GetSavingByID(string id);
        //Task<RegisterResponse> UpdateClient(UserSaveViewModel userSaveViewModel);
        //Task<RegisterResponse> UpdateAdmin(UserSaveViewModel userSaveViewModel);
    }
}
