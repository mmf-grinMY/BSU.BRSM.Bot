using System.ComponentModel.DataAnnotations;

namespace BSU.BRSM.Bot.Core.Data;
public class QuestionDto
{
    [Key]
    public int Id { get; set;}
    public bool IsEnded { get; set; } = false;
    public string Body { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public bool IsPosted { get; set; } = false;
    public int MessageId { get; set; }
    public DateTime DateTime { get; set; }
    public UserDto User { get; set; }
    public long UserChatId { get; set; }
}
