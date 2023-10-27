using Telegram.Bot.Types;
using Telegram.Bot;

namespace BSU.BRSM.Bot.Commands;
public class QuestionCommand : Command
{
    public override string Name => "question";
    public async override Task Execute(Update update, TelegramBotClient client)
    {
        if (update is null) return;
        if (update.Message is null) return;
        string message = "Ваш вопрос отправлен комитету. Мы постараемся ответить Вам в ближайшее время.";
        await client.SendTextMessageAsync(update.Message.Chat.Id, message);
    }
}
