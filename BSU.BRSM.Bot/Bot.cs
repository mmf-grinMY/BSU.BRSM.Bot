using BSU.BRSM.Bot.Commands;
using Telegram.Bot;

namespace BSU.BRSM.Bot;
public class Bot
{
    private static TelegramBotClient? _client = null;
    private static List<Command> _commands;
    public static IReadOnlyList<Command> Commands { get => _commands.AsReadOnly(); }

    public static TelegramBotClient GetTelegramBot()
    {
        if (_client != null) { return _client; }
        DotEnv.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));
        _client = new TelegramBotClient(Environment.GetEnvironmentVariable("BOT_TOKEN") ?? throw new ArgumentNullException("Bot_token"));
        _client.SetWebhookAsync("https://8990-2a02-d247-1100-dccd-e4bc-c141-a329-5c4c.ngrok-free.app");

        _commands = new List<Command>
        {
            new StartCommand(),
            new AboutCommand(),
            new QuestionCommand()
        };

        return _client;
    }
}
