using BSU.BRSM.Bot.Core.Models;

namespace BSU.BRSM.Bot.Core;
public interface IUserRepository
{
    Task<User> GetByChatIdAsync(long chatId);
    Task<IEnumerable<User>> GetAllAsync();
    Task<IEnumerable<User>> GetByFirstOrLastNameAsync(string name);
    Task<Question> GetQuestionByIdAsync(int id);
    Task UpdateAsync();
    Task RemoveByIdAsync(int id);
    Task<User> CreateAsync(long chatId, string firstName, string userName);
    Task<Question> CreateQuestionAsync(long chatId);
}
