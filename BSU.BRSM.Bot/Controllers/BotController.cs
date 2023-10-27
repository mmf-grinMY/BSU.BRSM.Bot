using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BSU.BRSM.Bot.Controllers;

[ApiController]
[Route("/")]
public class BotController : ControllerBase
{
    private readonly TelegramBotClient bot = Bot.GetTelegramBot();
    [HttpPost]
    public async void Post(Update update)
    {
        if (update.Message is null) throw new ArgumentNullException(nameof(update));
        long chatId = update.Message.Chat.Id;
        var message = update.Message;
        var commands = Bot.Commands;

        string text = message?.Text ?? throw new ArgumentNullException(nameof(update));
        if (text[0] == '/')
        {
            bool isCommand = false;
            foreach (var command in commands)
            {
                if (command.Contains(text))
                {
                    isCommand = true;
                    if (command.Contains("/question"))
                    {
                        // TODO: Push to list questions
                    }
                    await command.Execute(update, bot);
                }
            }

            if (!isCommand)
            {
                await bot.SendTextMessageAsync(chatId, "Неверная команда!");
            }
        }
        else
        {
            await bot.SendTextMessageAsync(message.Chat.Id, "Для того, чтобы задать боту вопрос введите команду /question!", replyToMessageId: message.MessageId - 1);
        }
    }
    [HttpGet]
    public string Get()
    {
        return "Telegram бот запущен!";
    }
}
