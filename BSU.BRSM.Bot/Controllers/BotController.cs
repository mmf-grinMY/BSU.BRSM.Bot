using BSU.BRSM.Bot.Commands;
using BSU.BRSM.Bot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using static BSU.BRSM.Bot.Bot;

namespace BSU.BRSM.Bot.Controllers;

[ApiController]
[Route("/")]
public class BotController : Controller
{
    private readonly TelegramBotClient bot = GetTelegramBot();
    [HttpPost]
    public async Task Post(Update update)
    {
        if (update is null) return;
        if (update.Message is null) throw new ArgumentNullException(nameof(update));
        var chatId = update.Message.Chat.Id;
        var message = update.Message;
        var commands = Bot.Commands;

        string text = message?.Text ?? throw new ArgumentNullException(nameof(update));
        Models.User user = new(chatId);
        if (text[0] == '/')
        {
            if (user.IsQuestionCommand && !text.StartsWith("/endquestion"))
            {
                if (Bot.Commands.Any(x => text.StartsWith("/" + x.Name)))
                {
                    await bot.SendTextMessageAsync(chatId, "Вы не можете пользоваться другими командами во время ввода вопроса! Сначало завершите ввод вопроса с помощью команды /endquestion.");
                }
                else
                {
                    await user.AddQuestion(text);
                }
            }
            else
            {
                string chatMessage = string.Empty;
                Command? command = Bot.Commands.FirstOrDefault(x => text.StartsWith("/" + x.Name));

                if (command is not null)
                {
                    if (command is QuestionCommand)
                    {
                        chatMessage = (text = text.Replace("/question", "").Trim()).Length != 0 ? await user.AddQuestion(text) : await user.AddQuestionPart("");
                    }

                    await command.Execute(update, bot, chatMessage);
                }
                else
                {
                    await bot.SendTextMessageAsync(chatId, "Неверная команда!");
                }
            }
        }
        else
        {
            if (user.IsQuestionCommand)
            {
                await user.AddQuestionPart(text);
            }
            else
            {
                // Exception
                try
                {
                    await bot.SendTextMessageAsync(message.Chat.Id, "Для того, чтобы задать боту вопрос введите команду /question!", replyToMessageId: message.MessageId - 1);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
    [HttpGet]
    public ViewResult Index()
    {
        List<Question> questons = new();
        using var connection = new SqliteConnection(ConnectionString);
        connection.OpenAsync();
        StringBuilder builder = new();
        SqliteCommand command = new("SELECT * FROM questions", connection);
        using var reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                questons.Add(new Question(reader.GetInt64(0), reader.GetString(1), reader.GetBoolean(2), reader.GetDateTime(3)));
            }
        }

        return View(model: questons);
    }
}