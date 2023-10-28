using BSU.BRSM.Bot.Commands;
using Telegram.Bot;
using BSU.BRSM.Bot.Controllers;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace BSU.BRSM.Bot;
public partial class Bot
{
    private static TelegramBotClient? _client = null;
    private static List<Command> _commands = new();
    public static Dictionary<long, User> Users { get; set; } = new Dictionary<long, User>();
    public static IReadOnlyList<Command> Commands { get => _commands.AsReadOnly(); }
    public static TelegramBotClient GetTelegramBot()
    {
        if (_client != null) { return _client; }
        DotEnv.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));
        _client = new TelegramBotClient(Environment.GetEnvironmentVariable("BOT_TOKEN") ?? throw new ArgumentNullException("Bot_token"));

        //Process.Start(@"C:\Users\grinm\Desktop\bot_get_url.bat");

        //while (!File.Exists(@"C:\Users\grinm\Desktop\tunnels.json")) { }

        string webHook = string.Empty;
        using (StreamReader stream = new(@"C:\Users\grinm\Desktop\tunnels.json"))
        {
            webHook = WebHookRegex().Match(stream.ReadToEnd()).Value;
        }

        if (string.IsNullOrEmpty(webHook))
        {
            throw new ArgumentException("Can not connect to Telegram bot while webHook is empty or null");
        }

        //File.Delete(@"C:\Users\grinm\Desktop\tunnels.json");

        _client.SetWebhookAsync(webHook);

        _commands.AddItems<Command>(
            new StartCommand(), 
            new AboutCommand(), 
            new QuestionCommand(),
            new EndQuestionCommand()
        );

        return _client;
    }
    [GeneratedRegex("https://(([0-9a-f]{4})-){8}([0-9a-f]{4})\\.ngrok-free\\.app")]
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
