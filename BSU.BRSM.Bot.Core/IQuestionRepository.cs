using BSU.BRSM.Bot.Core.Models;

namespace BSU.BRSM.Bot.Core;

public interface IQuestionRepository
{
    Question GetByGuid(Guid id);
    IEnumerable<Question> GetByChatId(long chatId);
    void RemoveByGuid(Guid id);
    Task<IEnumerable<Question>> GetByChatIdAsync(long chatId);
}
