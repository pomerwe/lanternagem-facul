using lanternagem_api.DataTransferObjects;
using lanternagem_api.Domain;
using lanternagem_api.Interfaces;
using lanternagem_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Services
{
    public class SystemManagementService : ISystemManagementService
    {
        private readonly ILogger<SystemManagementService> logger;
        private readonly ISystemUserProvider userProvider;
        private readonly ITokenService tokenService;

        public SystemManagementService(
            ILogger<SystemManagementService> logger,
            ISystemUserProvider userProvider,
            ITokenService tokenService)
        {
            this.logger = logger;
            this.userProvider = userProvider;
            this.tokenService = tokenService;
        }

        public async Task<(bool IsSuccess, SystemUser NewUser, string ErrorMessage)> InsertNewUserForCostumer(Customer customer)
        {
            try
            {
                var result = await InsertNewUser(customer, Role.Customer);

                if (result.IsSuccess)
                {
                    return (true, result.NewUser, null);
                }
                else
                {
                    return (false, null, result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, SystemUser NewUser, string ErrorMessage)> InsertNewUserForManager(Manager manager)
        {
            try
            {
                var result = await InsertNewUser(manager, Role.Manager);

                if (result.IsSuccess)
                {
                    return (true, result.NewUser, null);
                }
                else
                {
                    return (false, null, result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        private async Task<(bool IsSuccess, SystemUser NewUser, string ErrorMessage)> InsertNewUser(IUser user, Role role)
        {
            var newUser = CreateNewUser(user, role);

            var result = await userProvider.AddUser(newUser);

            if (result.IsSuccess)
            {
                return (true, result.User, null);
            }
            else
            {
                return (false, null, result.ErrorMessage);
            }
        }

        private SystemUser CreateNewUser(IUser user, Role role)
        {
            SystemUser newUser = new SystemUser();
            newUser.Username = user.GetName().Substring(user.GetName().Length / 2) + user.GetCPF().Substring(user.GetName().Length / 4, user.GetName().Length / 3) + user.GetCPF().Substring(0);
            var guid = Guid.NewGuid();
            newUser.Password = guid.ToString();
            newUser.Role = role;
            return newUser;
        }

        public async Task<(bool IsSuccess, AuthenticatedDto AuthenticatedDto, string ErrorMessage)> Login(LoginDto loginDto)
        {
            // Recupera o usuário
            var userResult = await userProvider.GetUserByUsername(loginDto.Username);

            // Verifica se o usuário existe
            if (userResult.IsSuccess)
            {
                var user = userResult.User;
                bool isLoginValid = user.CheckPassword(loginDto.Password);

                if (isLoginValid)
                {
                    var token = tokenService.GenerateToken(user);
                    user.Password = "";
                    var authenticatedDto = new AuthenticatedDto()
                    {
                        Token = token,
                        User = user
                    };

                    return (true, authenticatedDto, null);
                }
                else
                {
                    return (false, null, "Invalid password!");
                }

            }
            else
            {
                return (false, null, userResult.ErrorMessage);
            }
        }
    }
}
