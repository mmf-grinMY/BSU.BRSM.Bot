using BSU.BRSM.Bot.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace BSU.BRSM.Bot.Data.EF;
public class BotDbContext : DbContext
{
    public DbSet<UserDto> Users { get; set; }
    public DbSet<QuestionDto> Questions { get; set; }
    public BotDbContext(DbContextOptions<BotDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        BuildQuestions(modelBuilder);
        BuildUsers(modelBuilder);
    }

    private static void BuildQuestions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuestionDto>(action =>
        {
            action.HasOne(dto => dto.User)
                  .WithMany(dto => dto.Questions)
                  .IsRequired();

            action.HasData(
                new QuestionDto
                {
                    Id = 1,
                    IsEnded = true,
                    Body = "Как дела?",
                    DateTime = DateTime.Now,
                    MessageId = 1,
                    UserChatId = 87235698347
                },
                new QuestionDto
                {
                    Id = 2,
                    IsEnded = false,
                    Body = "Как твои",
                    DateTime = DateTime.Now,
                    MessageId = 2,
                    UserChatId = 87235698347
                },
                new QuestionDto
                {
                    Id = 3,
                    IsEnded = true,
                    Body = "Что делаешь?",
                    DateTime = DateTime.Now,
                    MessageId = 1,
                    UserChatId = 87232694547
                },
                new QuestionDto
                {
                    Id = 4,
                    IsEnded = false,
                    Body = "Какая сегодня",
                    DateTime = DateTime.Now,
                    MessageId = 2,
                    UserChatId = 87232694547
                },
                new QuestionDto
                {
                    Id = 5,
                    IsEnded = true,
                    Body = "Ты покушал?",
                    DateTime = DateTime.Now,
                    MessageId = 1,
                    UserChatId = 87235656834
                },
                new QuestionDto
                {
                    Id = 6,
                    IsEnded = false,
                    Body = "Ты пок",
                    DateTime = DateTime.Now,
                    MessageId = 2,
                    UserChatId = 87235656834
                }
            );
        });
    }
    private static void BuildUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserDto>(action =>
        {
            action.HasData(
                new UserDto
                {
                    ChatId = 87235698347,
                    FirstName = "Гринь",
                    UserName = "Максим",
                },
                new UserDto
                {
                    ChatId = 87232694547,
                    FirstName = "Гринь",
                    UserName = "Макс",
                },
                new UserDto
                {
                    ChatId = 87235656834,
                    FirstName = "Green",
                    UserName = "MaX",
                }
            );
        });
    }
}
