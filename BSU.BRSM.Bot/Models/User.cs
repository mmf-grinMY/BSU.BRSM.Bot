using Microsoft.Data.Sqlite;
using static BSU.BRSM.Bot.Bot;

namespace BSU.BRSM.Bot.Models;

public class User
{
    public User(long chatId)
    {
        ChatId = chatId;
    }
    public long ChatId { get; }
    private bool? _isQuestionCommand = null;
    public bool IsQuestionCommand
    {
        get
        {
            if (_isQuestionCommand is not null)
            {
                return _isQuestionCommand.Value;
            }
            else
            {
                using SqliteConnection connection = new(ConnectionString);
                connection.Open();
                SqliteCommand command = new($"SELECT chatid FROM questions WHERE isEnded == 0 AND chatId == {ChatId}", connection);
                using var reader = command.ExecuteReader();
                return (_isQuestionCommand = reader.HasRows).Value;
            }
        }
    }
    public async Task<string> AddQuestionPart(string part)
    {
        string body = null;
        using SqliteConnection connection = new(ConnectionString);
        await connection.OpenAsync();
        if (part != "")
        {
            SqliteCommand command = new($"SELECT body FROM questions WHERE chatId == {ChatId} AND IsEnded == 0", connection);

            using (var reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows && reader.Read())
                {
                    body = reader.GetString(0);
                }
            }

            if (body is not null)
            {
                body += part + '\n';
                command = new($"UPDATE questions SET body = \"{body.Replace("\"", "'")}\" WHERE chatId == {ChatId} AND IsEnded == 0", connection);
                await command.ExecuteNonQueryAsync();
            }
        }
        else
        {
            await AddQuestion(part, 0);
        }

        return "Вы можете вводить свой вопрос несколькими сообщениями. Чтобы закончить ввод вопроса введите команду /endquestion";
    }
    public async Task<string> AddQuestion(string question, int end = 1)
    {
        using var connection = new SqliteConnection(ConnectionString);
        await connection.OpenAsync();
        SqliteCommand sqlCommand = new($"INSERT INTO questions VALUES ({ChatId}, \"{question.Replace("\"", "'")}\", {end}, \"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\")", connection);
        await sqlCommand.ExecuteNonQueryAsync();
        await connection.CloseAsync();

        return "Ваш вопрос отправлен комитету. Мы постараемся ответить Вам в ближайшее время.";
    }
    public static async Task<Question?> GetQuestion(string chatId, string dateTime)
    {
        using var connection = new SqliteConnection(ConnectionString);
        await connection.OpenAsync();
        SqliteCommand sqlCommand = new($"SELECT * FROM questions WHERE ChatId == {chatId} and DateTime == \"{dateTime}\"", connection);
        Question? question = null;
        using (var reader = sqlCommand.ExecuteReader())
        {
            if (reader.HasRows)
            {
                reader.Read();
                question = new Question(reader.GetInt64(0), reader.GetString(1), reader.GetBoolean(2), reader.GetDateTime(3));
            }
        }
        await connection.CloseAsync();

        return question;
    }
    public static async Task DeleteQuestion(long chatId, DateTime dateTime)
    {
        using var connection = new SqliteConnection(ConnectionString);
        await connection.OpenAsync();
        await new SqliteCommand($"DELETE FROM questions WHERE chatId == {chatId} AND DateTime == \"{dateTime:yyyy-MM-dd HH:mm:ss}\"", connection).ExecuteNonQueryAsync();
        await connection.CloseAsync();
    }
}
