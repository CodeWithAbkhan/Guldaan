using Guldaan.Common.Db;
using Guldaan.Security.Api.Data.Models;

namespace Guldaan.Security.Api.Features.Subscriptions.Services.Poco
{
    public class UserWithLinkedTenants
    {
        public Guid Id { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Email { get; set; }
        public bool IsActivated { get; set; } = true;
        public bool IsOwner { get; set; } = false;
        public Guid Version { get; set; }
        public List<UserLinkedTenant> LinkedTenants { get; set; } = [];
    }
}
