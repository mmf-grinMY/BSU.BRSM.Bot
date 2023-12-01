using BSU.BRSM.Bot.App.Commands;
using Telegram.Bot;
using System.Text.RegularExpressions;
using BSU.BRSM.Bot.Core;

// TODO: Поудалять все закомментированные ненужные куски кода

namespace BSU.BRSM.Bot.App;
public partial class BrsmBot
{
    public TelegramBotClient Client { get; }
    private List<BaseCommand> commands = new();
    public BrsmBot(IUserRepository userRepository)
    {
        DotEnv.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));
        Client = new TelegramBotClient(Environment.GetEnvironmentVariable("BOT_TOKEN"));

        string webHook = string.Empty;
        using (StreamReader stream = new(@".\tunnels.json"))
        {
            webHook = WebHookRegex().Match(stream.ReadToEnd()).Value;
        }

        Client.SetWebhookAsync(webHook);

        commands.AddRange(new BaseCommand[]
        {
            new StartCommand(Client, "start"),
            new AboutCommand(Client, "about"),
            new QuestionCommand(Client, "question", userRepository),
        });
    }
    public IReadOnlyList<BaseCommand> Commands => commands.AsReadOnly();
    [GeneratedRegex("https://.*\\.ngrok-free\\.app")]
    private static partial Regex WebHookRegex();
}
