using BlazorChatbot.Models;

namespace BlazorChatbot.Services
{
    public interface IChatService
    {
        Task<string> GetResponse(string query);
    }
}
