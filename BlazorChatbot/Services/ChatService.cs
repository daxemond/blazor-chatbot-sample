using Newtonsoft.Json;

using BlazorChatbot.Models;
using BlazorBootstrap;

namespace BlazorChatbot.Services
{
   
    public class ChatService : HttpService, IChatService
    {
        private readonly ToastService _msgService;
        public ChatService(IHttpClientFactory factory,ToastService msgService) : base(factory.CreateClient("DataApi")) {
            _msgService = msgService;
        }

        public async Task<string> GetResponse(string query)
        {
            string url = "api/chat";
            string body = JsonConvert.SerializeObject(new ChatRequestModel() { Query = query });
            string response = await GetHttpResponse(HttpMethod.Post, url, body);
            if (response.IndexOf(".error") >= 0)
            {
                _msgService.Notify(new ToastMessage()
                {
                    Type = ToastType.Danger,
                    Message = "Error caused by the Data Api"
                });
                return "Internal Server Error.";
            }
            ChatResponseModel? model = JsonConvert.DeserializeObject<ChatResponseModel>(response);
            return model?.Message ?? "Error in Response";
        }



    }
}
