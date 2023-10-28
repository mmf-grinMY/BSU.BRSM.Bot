using BSU.BRSM.Bot.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using static BSU.BRSM.Bot.Bot;

namespace BSU.BRSM.Bot.Controllers;

public class Question 
{
    public DateTime DateTime { get; private set; }
    public string QuestionBody { get; private set; } = string.Empty;
    public bool IsQuestionEnded { get; set; } = false;
    public Question AddPart(string part)
    {
        DateTime = DateTime.Now;
        QuestionBody += part + '\n';
        return this;
    }
    public override string ToString()
    {
        return $"Question : {{ DateTime : \"{DateTime}\", QuestionBody : {QuestionBody}, IsQuestionEnded : \"{IsQuestionEnded}\" }}";
    }
}

public class User
{
    public User(long chatId)
    {
        ChatId = chatId;
        Questions = new List<Question>();
    }
    public bool IsQuestionCommand { get; set; } = false;
    public long ChatId { get; set; }
    public List<Question> Questions { get; set; }
    public User AddQuestion(string question)
    {
        if (Questions.Count == 0 || Questions[^1].IsQuestionEnded)
        {
            Questions.Add(new Question().AddPart(question));
        }
        else
        {
            Questions[^1].AddPart(question);
        }
        return this;
    }
    public override string ToString()
    {
        return $"User : {{ ChatId : \"{ChatId}\", Questions : {Questions.ToJsonString()} }}";
    }
}

[ApiController]
[Route("/")]
public class BotController : ControllerBase
{
    private readonly TelegramBotClient bot = GetTelegramBot();
    [HttpPost]
    public async Task<string> Post(Update update)
    {
        if (update.Message is null) throw new ArgumentNullException(nameof(update));
        long chatId = update.Message.Chat.Id;
        var message = update.Message;
        var commands = Bot.Commands;

        string text = message?.Text ?? throw new ArgumentNullException(nameof(update));
        User user;
        if (!Users.ContainsKey(chatId))
        {
            Users.Add(chatId, user = new User(chatId));
        }
        else
        {
            user = Users[chatId];
        }
        if (text[0] == '/')
        {
            if (user.IsQuestionCommand && !text.StartsWith("/endquestion"))
            {
                bool isCommand = false;
                foreach (var command in Bot.Commands)
                {
                    if (text.StartsWith("/" + command.Name))
                    {
                        isCommand = true;
                    }
                }
                if (isCommand)
                {
                    await bot.SendTextMessageAsync(chatId, "Вы не можете пользоваться другими командами во время ввода вопроса! Сначало завершите ввод вопроса с помощью команды /endquestion.");
                }
                else
                {
                    await AddQuestion(user, text, message);
                }
            }
            else
            {
                bool isCommand = false;
                string chatMessage = string.Empty;
                user.IsQuestionCommand = false;
                foreach (var command in commands)
                {
                    if (command.Contains(text))
                    {
                        isCommand = true;
                        if (command is QuestionCommand)
                        {
                            if ((text = text.Replace("/question", "").Trim()).Length != 0)
                            {
                                user.AddQuestion(text).Questions[^1].IsQuestionEnded = true;
                                chatMessage = "Ваш вопрос отправлен комитету. Мы постараемся ответить Вам в ближайшее время.";
                            }
                            else
                            {
                                user.IsQuestionCommand = true;
                                chatMessage = "Вы можете вводить свой вопрос несколькими сообщениями. Чтобы закончить ввод вопроса введите команду /endquestion";
                            }
                        }
                        await command.Execute(update, bot, chatMessage);
                        return Users.ToArrayString();
                    }
                }

                if (!isCommand)
                {
                    await bot.SendTextMessageAsync(chatId, "Неверная команда!");
                }
            }
        }
        else
        {
            await AddQuestion(user, text, message);
        }

        return Users.ToArrayString();
    }
    [HttpGet]
    public string Get() => Users.Count == 0 ? "Telegram бот запущен!" : Users.ToArrayString();
    private async Task AddQuestion(User user, string text, Message message)
    {
        if (user.IsQuestionCommand)
        {
            if (user.Questions.Count != 0)
            {
                if (user.Questions[^1].IsQuestionEnded)
                {
                    user.AddQuestion(text);
                }
                else
                {
                    user.Questions[^1].AddPart(text);
                }
            }
            else
            {
                user.AddQuestion(text);
            }
        }
        else
        {
            await bot.SendTextMessageAsync(message.Chat.Id, "Для того, чтобы задать боту вопрос введите команду /question!", replyToMessageId: message.MessageId - 1);
        }
    } 
}

public static class DictionaryExtensions
{
    public static string ToArrayString<T, P>(this Dictionary<T, P> dict)
    {
        if (Users is null || Users.Count == 0) return string.Empty;
        StringBuilder builder = new();
        builder.Append('[');
        foreach (var item in dict)
        {
            builder.Append(item.Value?.ToString()).Append(",\n");
        }
        builder.Append(']');
        return builder.ToString().Replace(",\n]", " ]");
    }
    public static string ToJsonString<T>(this List<T> list)
    {
        if (list.Count == 0) return "[]";
        StringBuilder builder = new();
        builder.Append('[');
        foreach (T item in list)
        {
            builder.Append(item?.ToString()).Append(",\n");
        }
        builder.Append(']');
        return builder.ToString().Replace(",\n]", " ]");
    }
}
