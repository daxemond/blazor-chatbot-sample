namespace BlazorChatbot.Models
{
    public class SecurityOption
    {
        public string TenantId { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;

    }
}
