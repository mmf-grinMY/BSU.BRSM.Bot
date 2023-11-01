using BSU.BRSM.Bot.Commands;
using Telegram.Bot;
using System.Text.RegularExpressions;

namespace BSU.BRSM.Bot;
public partial class Bot
{
    public static string ConnectionString { get; } = "Data Source=bsu_brsm_bot.db";
    private static TelegramBotClient? _client = null;
    private static List<Command> _commands = new();
    public static IReadOnlyList<Command> Commands { get => _commands.AsReadOnly(); }
    public static TelegramBotClient GetTelegramBot()
    {
        if (_client != null) { return _client; }
        DotEnv.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));
        _client = new TelegramBotClient(Environment.GetEnvironmentVariable("BOT_TOKEN") ?? throw new ArgumentNullException("Bot_token"));

        string webHook = string.Empty;
        using (StreamReader stream = new(@".\tunnels.json"))
        {
            webHook = WebHookRegex().Match(stream.ReadToEnd()).Value;
        }

        if (string.IsNullOrEmpty(webHook))
        {
            throw new ArgumentException("Can not connect to Telegram bot while webHook is empty or null");
        }

        _client.SetWebhookAsync(webHook);

        _commands.AddItems<Command>(
            new StartCommand(), 
            new AboutCommand(), 
            new QuestionCommand(),
            new EndQuestionCommand()
        );

        return _client;
    }
    [GeneratedRegex("https://.*\\.ngrok-free\\.app")]
    private static partial Regex WebHookRegex();
}
public static class ListExtensions
{
    public static void AddItems<T>(this List<T> list, params T[] items)
    {
        foreach (T item in items)
        {
            list.Add(item);
        }
    }
}
