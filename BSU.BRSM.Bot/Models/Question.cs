using Microsoft.Data.Sqlite;

namespace BSU.BRSM.Bot.Models;
public class Question
{
    public Question(string body, bool isEnded, DateTime dateTime)
    {
        Body = body;
        IsEnded = isEnded;
        DateTime = dateTime;
    }
    public string Body { get; }
    public bool IsEnded { get;  }
    public DateTime DateTime { get; }
    public async Task Close(string chatId)
    {
        using SqliteConnection connection = new(Bot.ConnectionString);
        connection.Open();
        SqliteCommand command = new($"DELETE FROM questions WHERE chatId == {chatId} AND DateTime == \"{DateTime:yyyy-MM-dd HH:mm:ss}\"", connection);
        await command.ExecuteNonQueryAsync();
    }
}