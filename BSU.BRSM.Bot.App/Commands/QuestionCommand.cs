using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using BSU.BRSM.Bot.Core;

namespace BSU.BRSM.Bot.App.Commands;
public class QuestionCommand : BaseCommand
{
    private readonly IUserRepository userRepository;
    public QuestionCommand(ITelegramBotClient client, string name, IUserRepository userRepository) : base(client, name) 
    {
        this.userRepository = userRepository;
    }
    public async override Task Execute(Update update, string message = "")
    {
        Message chatMessage = null;
        switch (update.Type)
        {
            case UpdateType.Message: chatMessage = update.Message; break;
            case UpdateType.CallbackQuery:  chatMessage = update.CallbackQuery.Message; break;
            default: return;
        }
        long chatId = chatMessage.Chat.Id;
        Core.Models.User user;
        try
        {
            user = await userRepository.GetByChatIdAsync(chatId);
        }
        catch (InvalidOperationException)
        {
            string firstName = chatMessage.Chat.LastName;
            string userName = chatMessage.Chat.FirstName;
            user = await userRepository.CreateAsync(chatId, firstName, userName);
        }
        _ = await userRepository.CreateQuestionAsync(chatId);
        message = "Введите Ваш вопрос одним сообщением и через некоторое время Вы получите ответ от комитета.";
        _ = await client.SendTextMessageAsync(chatId, message);
    }
}