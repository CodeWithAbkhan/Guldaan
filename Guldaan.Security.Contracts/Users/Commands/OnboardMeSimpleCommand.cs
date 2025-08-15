using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guldaan.Security.Contracts.Users.Commands
{
    public record OnboardMeSimpleCommand
    {
        public string ActivationCode { get; init; } = default!;
    }
}
