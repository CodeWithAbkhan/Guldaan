namespace Guldaan.Common.Frontend.HttpClients
{ 
    public interface IHttpUserClient
    {
        Task<HttpResponseMessage> GetUserInfoAsync(string token);
    }
}
