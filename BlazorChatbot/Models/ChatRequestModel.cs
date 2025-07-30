using System.ComponentModel.DataAnnotations;

namespace BlazorChatbot.Models
{
    public class ChatRequestModel
    {
        [Required]
        public string Query { get; set; } = string.Empty;
    }
}
