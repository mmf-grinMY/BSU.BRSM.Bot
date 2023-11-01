using Microsoft.Data.Sqlite;

namespace BSU.BRSM.Bot.Models;
public class Question
{
    public Question(long chatId, string body, bool isEnded, DateTime dateTime)
    {
        ChatId = chatId;
        Body = body;
        IsEnded = isEnded;
        DateTime = dateTime;
    }
    public long ChatId { get; }
    public string Body { get; }
    public bool IsEnded { get;  }
    public DateTime DateTime { get; }
    public async Task Close()
    {
        using SqliteConnection connection = new(Bot.ConnectionString);
        connection.Open();
        SqliteCommand command = new($"DELETE FROM questions WHERE chatId == {ChatId} AND DateTime == \"{DateTime:yyyy-MM-dd HH:mm:ss}\"", connection);
        await command.ExecuteNonQueryAsync();
    }
}