using BSU.BRSM.Bot.Core.Data;

namespace BSU.BRSM.Bot.Core.Models;
public class Question
{
    private readonly QuestionDto dto;
    public int Id => dto.Id;
    public long ChatId => dto.UserChatId;
    public bool IsEnded
    {
        get => dto.IsEnded;
        set => dto.IsEnded = value;
    }
    public string Body
    {
        get => dto.Body;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) 
                throw new ArgumentNullException(nameof(value));

            dto.Body = value;
        }
    }
    public string Answer
    {
        get => dto.Answer;
        set
        {
            if (IsPosted && string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            dto.Answer = value;
        }
    }
    public bool IsPosted
    {
        get => dto.IsPosted;
        set => dto.IsPosted = value;
    }
    public int MessageId => dto.MessageId;
    public DateTime DateTime => dto.DateTime;
    public Question(QuestionDto dto)
    {
        this.dto = dto;
    }
    public static class DtoFactory
    {
        private static int counter = 15;
        public static QuestionDto Create(long chatId)
        {
            return new QuestionDto
            {
                Id = ++counter,
                UserChatId = chatId,
                DateTime = DateTime.Now
            };
        }
    }
    public static class Mapper
    {
        public static Question Map(QuestionDto dto) => new(dto);
        public static QuestionDto Map(Question domain) => domain.dto;
    }
}