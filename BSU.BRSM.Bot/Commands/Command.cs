using Telegram.Bot.Types;
using Telegram.Bot;

namespace BSU.BRSM.Bot.Commands;
public abstract class Command
{
    public abstract string Name { get; }
    public abstract Task Execute(Update update, TelegramBotClient client, string message = "");
    public bool Contains(string command)
    {
        return command.StartsWith("/" + Name);
    }
}
