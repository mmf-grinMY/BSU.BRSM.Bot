using BSU.BRSM.Bot.Core.Data;

namespace BSU.BRSM.Bot.Core.Models;
public class User
{
    private readonly UserDto dto;
    public long ChatId => dto.ChatId;
    public string FirstName
    {
        get => dto.FirstName;
        set 
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));
            dto.FirstName = value; 
        }
    }
    public string UserName
    {
        get => dto.UserName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));
            dto.UserName = value;
        }
    }
    public IEnumerable<QuestionDto> Questions
    {
        get => dto.Questions;
        set => dto.Questions = value;
    }
    internal User(UserDto dto)
    {
        this.dto = dto;
    }
    public bool IsQuestionEnded
    {
        get => dto.Questions.Any() && dto.Questions.Last().IsEnded;
        set
        {
            if (dto.Questions.Any())
                dto.Questions.Last().IsEnded = value;
        }
    }
    public static class DtoFactory
    {
        public static UserDto Create(long chatId, string firstName, string userName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException(nameof(firstName));

            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException(nameof(userName));

            return new UserDto
            {
                ChatId = chatId,
                FirstName = firstName,
                UserName = userName
            };
        }
    }
    public static class Mapper
    {
        public static User Map(UserDto dto) => new(dto);
        public static UserDto Map(User domain) => domain.dto;
    }
}