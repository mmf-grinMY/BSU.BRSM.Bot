using Telegram.Bot.Types;
using Telegram.Bot;
using Microsoft.Data.Sqlite;

namespace BSU.BRSM.Bot.Commands;
public class QuestionCommand : Command
{
    public override string Name => "question";
    public async override Task Execute(Update update, TelegramBotClient client, string message = "")
    {
        if (update is null) return;
        if (update.Message is null) return;
        //string message = "Ваш вопрос отправлен комитету. Мы постараемся ответить Вам в ближайшее время.";
        await client.SendTextMessageAsync(update.Message.Chat.Id, message);
    }
}
public class EndQuestionCommand : Command
{
    public override string Name => "endquestion";
    public override async Task Execute(Update update, TelegramBotClient client, string message = "")
    {
        long chatId = update.Message.Chat.Id;
        using SqliteConnection connection = new(Bot.ConnectionString);
        await connection.OpenAsync();
        SqliteCommand command = new($"SELECT IsEnded FROM questions WHERE chatId == {chatId} AND IsEnded == 0", connection);
        using (var reader = command.ExecuteReader())
        {
            if (reader.HasRows)
            {
                command = new($"UPDATE questions SET IsEnded = 1 WHERE chatId == {chatId} AND IsEnded == 0", connection);
                await command.ExecuteNonQueryAsync();
                await client.SendTextMessageAsync(chatId, "Ваш вопрос отправлен комитету. Мы постараемся ответить Вам в ближайшее время.");
            }
            else
            {
                await client.SendTextMessageAsync(chatId, "У Вас нет открытых вопросов для отправки комитету. Чтобы задать вопрос воспользуйтесь командой /question");
            }
        }
        await connection.CloseAsync();
    }
}
