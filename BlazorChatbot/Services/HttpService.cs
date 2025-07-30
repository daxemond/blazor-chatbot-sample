using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text;

namespace BlazorChatbot.Services
{
    public class HttpService
    {
        private readonly HttpClient _client;
        public HttpService(HttpClient client) {
            _client = client;
        }

        protected async Task<string> GetHttpResponse(HttpMethod type, string url, string body)
        {
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(type, url);
            request.Content = new StringContent(body, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.SendAsync(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine(response.ReasonPhrase);
                return $".error: {response.ReasonPhrase}";
            }
            string s = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return s;
            }
            return ".error in calling data api.";
        }
    }
}
