using System.ComponentModel.DataAnnotations;

namespace BSU.BRSM.Bot.Core.Data;
public class UserDto
{
    [Key]
    public long ChatId { get; set; }
    public string FirstName { get; set; }
    public string UserName { get; set; }
    public IEnumerable<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
}
