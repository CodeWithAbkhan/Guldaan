using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guldaan.Security.Contracts.Users.Results;

namespace Guldaan.Security.Contracts.Subscriptions.Commands
{
    public record UpdateSubscriptionLinkedUserCommand
    {
        public required string Firstname { get; init; }
        public required string Lastname { get; init; }
        public bool IsActivated { get; init; } = true;
        public bool IsSubscriptionOwner { get; init; } = true;
        public Guid Version { get; init; }
    }
}
