using Telegram.Bot;
using BRSM_Bot.Models.Commands;

namespace BRSM_Bot.Models;
public static class Bot
{
    private static TelegramBotClient _client;
    private static List<Command> _commands;
    public static IReadOnlyList<Command> Commands { get => _commands.AsReadOnly(); }
    public static async Task<TelegramBotClient> Get()
    {
        if (_client is not null) return _client;

        _commands = new List<Command>();
        _commands.Add(new HelloCommand());
        //TODO: Add new commands

        _client = new TelegramBotClient(AppSettings.Key);
        var hook = string.Format(AppSettings.Url, "api/message/update");
        await _client.SetWebhookAsync(hook);

        return _client;
    }
}
