using Core.Application.DTOs.Account;
using Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interface.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> Authentication(AuthenticationRequest request);
        Task<string> ConfirmAccount(string userId, string token);
        Task<RegisterResponse> RegisterClients(RegisterRequest request);
        Task<RegisterResponse> UpdateClient(RegisterRequest request);
        Task<RegisterResponse> RegisterAdmin(RegisterRequest request);
        Task<RegisterResponse> UpdateAdmin(RegisterRequest request);
        List<UserViewModel> GetAllUser();
        Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request);
        Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request);
        Task SignOut();
        Task<List<UserGetAllViewModel>> GetAllVMUser();
        Task<UserSaveViewModel> GetAccountByid(string ID);
        Task<List<string>> GetAdminUsers();
        Task ChangeStatus(string id);
    }
}
