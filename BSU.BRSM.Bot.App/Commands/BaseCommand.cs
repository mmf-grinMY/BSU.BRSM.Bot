using Telegram.Bot.Types;
using Telegram.Bot;

namespace BSU.BRSM.Bot.App.Commands;
public abstract class BaseCommand
{
    protected readonly ITelegramBotClient client;
    public BaseCommand(ITelegramBotClient client, string name)
    {
        this.client = client;

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        Name = name;
    }
    public string Name { get; }
    public abstract Task Execute(Update update, string message = "");
    public bool Contains(string command) => command.StartsWith("/" + Name);
}
