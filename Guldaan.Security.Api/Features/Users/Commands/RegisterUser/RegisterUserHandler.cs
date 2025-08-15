using LanguageExt;
using Guldaan.Common.Db;
using Guldaan.Common.Errors;
using Guldaan.Security.Api.Data.Models;
using Guldaan.Security.Api.Features.Tenants.Services;
using Guldaan.Security.Api.Features.Users.Services;
using Guldaan.Security.Api.Mappers;
using Guldaan.Security.Contracts.Tenants.Commands;
using Guldaan.Security.Contracts.Users.Commands;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Guldaan.Security.Api.Features.Users.Commands.RegisterUser
{
    public class RegisterUserHandler(UserCommandService commandService)
    {
        private static readonly Guid SYSTEM_USER_ID = new("dc820000-5dd4-0015-b13b-08dd55cc80f7");
        private readonly UserCommandService _commandService = commandService;


        public async Task<UserModel> Handle(RegisterUserCommand command)
        {
            var user = command.MapToUser();

            // Need to put the system user as the creator of the user (for registration)
            var now = DateTime.UtcNow;
            user.IsEmailVerified = false;
            user.ActivationCode = GenerateActivationCode();
            user.AuditInfo = new AuditData(now, SYSTEM_USER_ID, now, SYSTEM_USER_ID);

            return await _commandService.AddUserInDbAsync(user);
        }

        private static string GenerateActivationCode()
        {
            using var rng = RandomNumberGenerator.Create();
            var tokenData = new byte[32];
            rng.GetBytes(tokenData);

            return Convert.ToBase64String(tokenData);
        }
    }
}
