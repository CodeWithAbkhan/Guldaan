using Microsoft.AspNetCore.Routing;

namespace Guldaan.Common.Http
{
    public interface IEndpoint
    {
        void MapEndpoint(IEndpointRouteBuilder app);
    }
}
