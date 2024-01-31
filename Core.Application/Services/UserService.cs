﻿using AutoMapper;
using Core.Application.DTOs.Account;
using Core.Application.DTOs.Register;
using Core.Application.Interface.Services;
using Core.Application.ViewModels.User;
using DocumentFormat.OpenXml.ExtendedProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> Login(LoginViewModel loginViewModel)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(loginViewModel);
            AuthenticationResponse response = await _accountService.Authentication(loginRequest);

            return response;
        }
        //public async Task<RegisterResponse> RegisterClient(UserSaveViewModel userSaveViewModel)
        //{
        //    RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(userSaveViewModel);
        //    return await _accountService.RegisterClients(registerRequest);
        //}
        public async Task<RegisterResponse> Register(RegisterRequestDTO dTO)
        {
            RegisterRequest registerRequest = new RegisterRequest() { 
                Name = dTO.Nombre,
                LastName = dTO.Apellido,
                Identification = dTO.Cedula,
                Email = dTO.Correo,
                isActive = false,
                Password = "123Pa$$word!",
                ConfirmPassword = "123Pa$$word!"
            };
            return await _accountService.Register(registerRequest);
        }
        public async Task<string> ConfirmEmail(string userId, string token)
        {
            return await _accountService.ConfirmAccount(userId, token);
        }
        public async Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            ForgotPasswordRequest PasswordRequest = _mapper.Map<ForgotPasswordRequest>(forgotPasswordViewModel);
            return await _accountService.ForgotPassword(PasswordRequest);
        }
        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            ResetPasswordRequest PasswordRequest = _mapper.Map<ResetPasswordRequest>(resetPasswordViewModel);
            return await _accountService.ResetPassword(PasswordRequest);
        }
        public async Task SingOut()
        {
            await _accountService.SignOut();
        }
        public async Task<List<UserGetAllViewModel>> GetAllClients()
        {
            return await _accountService.GetAllVMUser();
        }
        public List<UserViewModel> GetAlladmin()
        {
            List<UserViewModel> usersVM = _accountService.GetAllUser();
            List<UserViewModel> usersClients = new();
            return usersClients;
        }
        public async Task<UserSaveViewModel> GetAccountByid(string ID)
        {
            return await _accountService.GetAccountByid(ID);
        }

        public async Task<bool> IsAdmin(string ID)
        {
            var adminList = await _accountService.GetAdminUsers();
            foreach (var admin in adminList)
            {
                if (admin == ID)
                {
                    return true;
                }
            }
            return false;
        }

        //public async Task<RegisterResponse> UpdateClient(UserSaveViewModel userSaveViewModel)
        //{
        //    RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(userSaveViewModel);
        //    return await _accountService.UpdateClient(registerRequest);
        //}

        //public async Task<RegisterResponse> UpdateAdmin(UserSaveViewModel userSaveViewModel)
        //{
        //    RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(userSaveViewModel);
        //    return await _accountService.UpdateAdmin(registerRequest);

        //}

        public async Task<string> GetSavingByID(string id)
        {
            return "a";
        }
        public async Task ChangeStatus(string Id, int TipoUsuario)
        {
            await _accountService.ChangeStatus(Id, TipoUsuario);
        }

        public async Task<RegisterResponse> Update(RegisterRequestDTO dTo)
        {
            return await _accountService.Update(dTo);
        }
    }
}
