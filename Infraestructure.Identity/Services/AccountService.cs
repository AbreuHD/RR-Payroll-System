using Azure.Core;
using Core.Application.DTOs.Account;
using Core.Application.DTOs.Register;
using Core.Application.DTOs.Toast;
using Core.Application.Enum;
using Core.Application.Features.Empleado.Queries.GetEmpleadoByIdentityId;
using Core.Application.Interface.Services;
using Core.Application.ViewModels.User;
using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using Infrastructure.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Infraestructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<AuthenticationResponse> Authentication(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No account registered with {request.UserName}";

                return response;
            }
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.UserName}";

                return response;
            }

            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Cuenta desactivada para {request.UserName}";

                return response;
            }
            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            response.isTheFirsTime = user.isTheFirsTime;

            return response;
        }

        public async Task<RegisterResponse> Update(RegisterRequestDTO dTo)
        {
            RegisterResponse response = new();
            response.HasError = false;

            var userToUpdate = await _userManager.FindByIdAsync(dTo.Id);
            var user = userToUpdate;

            user.Email = dTo.Correo;
            user.Name = dTo.Nombre;
            user.LastName = dTo.Apellido;
            user.PhoneNumber = dTo.Telefono;


            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                response.Toasts.Add(new ToastRequestDTO()
                {
                    Title = "ERROR",
                    Message = $"Ocurrio un erorr tratando de actualizar el usuario"
                });
                //response.HasError = true;
                //response.Error = $"An error ocurred trying to update the user";
                return response;
            }
            return response;
        }

        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            var userNameLoop = true;
            var userName = "";
            RegisterResponse response = new();
            response.Toasts = new List<ToastRequestDTO>();
            var i = 0;

            response.HasError = false;
            while (userNameLoop)
            {
                i++;
                userName = request.Name.Substring(0, 3) + request.LastName.Substring(0,3) + i;
                var userWithSameUserName = await _userManager.FindByNameAsync(userName);
                if(userWithSameUserName == null)
                {
                    userNameLoop = false;
                }
            }
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);

            if (userWithSameEmail != null)
            {
                response.Toasts.Add(new ToastRequestDTO()
                {
                    Title = "El Correo ya existe",
                    Message = $"Email {request.Email} ya existe, inicia sesion con esa cuenta o usa otro correo"
                });
                response.HasError = true;
                response.Error = $"Email {request.Email} is already register";
                return response;
            }
            var user = new ApplicationUser()
            {
                Email = request.Email,
                Name = request.Name,
                LastName = request.LastName,
                UserName = userName,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error ocurred trying to register the user";
                return response;
            }

            response.Id = user.Id;
            response.Username = user.UserName;
            return response;
        }

        public async Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request)
        {
            ForgotPasswordResponse response = new();

            response.HasError = false;

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No account registered with {request.Email}";

                return response;
            }

            return response;
        }

        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new();
            response.HasError = false;

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                response.HasError = true;
                return response;
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            
            var result = await _userManager.ResetPasswordAsync(user, token, request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred while resetting password ";
                return response;
            }

            var data = await _userManager.FindByIdAsync(request.UserId);
            data.isTheFirsTime = true;
            await _userManager.UpdateAsync(data);


            return response;
        }

        public async Task<string> ConfirmAccount(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "No accounts registered with this user";
            }
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirmed for {user.Email}. You can now use the app";
            }
            return $"An error occurred while confirming {user.Email}.";
        }

        private async Task<string> SendVerificacionEmailUrl(ApplicationUser user)
        {

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{"a"}/", route));
            var verificationUrl = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUrl += QueryHelpers.AddQueryString(verificationUrl, "token", code);

            return verificationUrl;
        }

        private async Task<string> SendForgotPasswordUrl(ApplicationUser user)
        {

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            string route = "User/ResetPassword";
            var uri = new Uri(string.Concat($"{""}/{route}"));
            var verificationUrl = QueryHelpers.AddQueryString(uri.ToString(), "token", code);

            return verificationUrl;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public List<UserViewModel> GetAllUser()
        {
            var users = _userManager.Users.ToList();
            List<UserViewModel> usersList = users.Select(user => new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                UserName = user.UserName,
            }).ToList();

            return usersList;
        }

        public async Task<List<UserGetAllViewModel>> GetAllVMUser()
        {
            var users = _userManager.Users.ToList();
            var all = users.Select(x => new UserGetAllViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                IsAdmin = false,
                Email = x.Email,
                LastName = x.LastName,
                Name = x.Name,
                IsActive = x.EmailConfirmed
            }).ToList();

            var adminID = GetAdminUsers().Result;
            foreach (var item in all)
            {
                var user = await _userManager.FindByIdAsync(item.Id);
                item.IsActive = user.EmailConfirmed;
                if (adminID.Contains(item.Id))
                {
                    item.IsAdmin = true;
                }
            }

            return all;
        }

        public async Task ChangeUserState(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.EmailConfirmed = user.EmailConfirmed == false ? true : false;
            await _userManager.UpdateAsync(user);
        }

        public async Task<List<string>> GetAdminUsers()
        {
            var roleList = _userManager.GetUsersInRoleAsync("Admin").Result.ToList();
            return roleList.Select(x => x.Id).ToList();
        }
        
        public async Task<UserSaveViewModel> GetAccountByid(string ID)
        {
            var data = await _userManager.FindByIdAsync(ID);
            return new UserSaveViewModel
            {
                Name = data.Name,
                LastName = data.LastName,
                UserName = data.UserName,
                Email = data.Email,
                Id = data.Id,
                isActive = data.EmailConfirmed,
                Phone = data.PhoneNumber,
                IsAdmin = await _userManager.IsInRoleAsync(data, "Admin")
            };
        }

        public async Task ChangeStatus(string Id, int TipoUsuario)
        {
            var userToUpdate = await _userManager.FindByIdAsync(Id);
            userToUpdate.EmailConfirmed = !userToUpdate.EmailConfirmed;

            var roles = await _userManager.GetRolesAsync(userToUpdate);

            if(roles.Count > 0)
            {
                await _userManager.RemoveFromRolesAsync(userToUpdate, roles);

            }
            if(TipoUsuario != 0)
            {
                Roles selectedRole = (Roles)TipoUsuario - 1;
                await _userManager.AddToRoleAsync(userToUpdate, selectedRole.ToString());
                await _userManager.UpdateAsync(userToUpdate);
            }
        }


    }
}
