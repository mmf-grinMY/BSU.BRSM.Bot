using BSU.BRSM.Bot.Core;
using BSU.BRSM.Bot.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BSU.BRSM.Bot.Data.EF;

internal class UserRepository : IUserRepository
{
    private readonly DbContextFactory dbContextFactory;
    public UserRepository(DbContextFactory dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var dbContext = dbContextFactory.Create(typeof(UserRepository));

        var dtos = await dbContext.Users
            .Include(user => user.Questions)
            .ToArrayAsync();

        return dtos.Select(User.Mapper.Map);
    }
    public async Task<User> GetByChatIdAsync(long chatId)
    {
        var dbContext = dbContextFactory.Create(typeof(UserRepository));

        return User.Mapper.Map(
            await dbContext.Users
            .Include(user => user.Questions)
            .SingleAsync(user => user.ChatId == chatId));
    }
    public async Task<IEnumerable<User>> GetByFirstOrLastNameAsync(string name)
    {
        var dbContext = dbContextFactory.Create(typeof(UserRepository));

        var dtos = await dbContext.Users
            .Include(user => user.Questions)
            .Where(user => user.UserName.Contains(name) || user.FirstName.Contains(name))
            .ToArrayAsync();

        return dtos.Select(User.Mapper.Map);
    }
    public async Task<Question> GetQuestionByIdAsync(int id)
    {
        var dbContext = dbContextFactory.Create(typeof(UserRepository));

        return Question.Mapper.Map(await dbContext.Questions.SingleAsync(q => q.Id == id));
    }
    public async Task UpdateAsync()
    {
        var dbContext = dbContextFactory.Create(typeof(UserRepository));

        await dbContext.SaveChangesAsync();
    }
    public async Task RemoveByIdAsync(int id)
    {
        var dbContext = dbContextFactory.Create(typeof(UserRepository));

        dbContext.Questions.Remove(await dbContext.Questions.SingleAsync(q => q.Id == id));
        await dbContext.SaveChangesAsync();
    }
    public async Task<User> CreateAsync(long chatId, string firstName, string userName)
    {
        var dbContext = dbContextFactory.Create(typeof(UserRepository));

        var dto = User.DtoFactory.Create(chatId, firstName, userName);
        dbContext.Users.Add(dto);
        await dbContext.SaveChangesAsync();

        return User.Mapper.Map(dto);
    }
    public async Task<Question> CreateQuestionAsync(long chatId)
    {
        var dbContext = dbContextFactory.Create(typeof(UserRepository));

        var dto = Question.DtoFactory.Create(chatId);
        dbContext.Questions.Add(dto);
        await dbContext.SaveChangesAsync();

        return Question.Mapper.Map(dto);
    }
    //public void RemoveQuestionById(int id)
    //{
    //    var dbContext = dbContextFactory.Create(typeof(UserRepository));

    //    dbContext.Questions.Remove(dbContext.Questions.Single(q => q.Id == id));
    //}
}
