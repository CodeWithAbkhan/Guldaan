using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guldaan.Security.Contracts.Authorizations.Results
{
    public record AuthorizationLightResult
    {
        public Guid Id { get; init; }
        public required string Code { get; init; }
    }
}
