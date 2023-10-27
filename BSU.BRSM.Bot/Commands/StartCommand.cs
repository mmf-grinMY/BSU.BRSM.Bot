using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BSU.BRSM.Bot.Commands
{
    public class StartCommand : Command
    {
        public override string Name => "start";

        public async override Task Execute(Update update, TelegramBotClient client)
        {
            var ikm = new InlineKeyboardMarkup(new InlineKeyboardButton[][]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "задать вопрос", callbackData: "question"),
                    InlineKeyboardButton.WithCallbackData(text: "о нас", callbackData: "about")
                }
            });

            var message = update.Message ?? throw new ArgumentNullException(nameof(update));

            await client.SendTextMessageAsync(message.Chat.Id, $"Привет, {message.Chat.FirstName}!", replyMarkup: ikm);
        }
    }
}
