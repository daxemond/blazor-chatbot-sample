namespace BlazorChatbot.Services
{
    public interface ITokenService
    {
        string GetServerBearerToken(string email);
    }
}
