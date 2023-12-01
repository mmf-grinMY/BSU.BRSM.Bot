using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BSU.BRSM.Bot.App.Commands
{
    public class StartCommand : BaseCommand
    {
        public StartCommand(ITelegramBotClient client, string name) : base(client, name) { }
        public async override Task Execute(Update update, string message = "")
        {
            var chatMessage = update.Message ?? throw new ArgumentNullException(nameof(update));
            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "О нас", callbackData: "about"),
                    InlineKeyboardButton.WithCallbackData(text: "Задать вопрос", callbackData: "question"),
                },
            });
            
            message = $"Привет, {chatMessage.Chat.FirstName}!\n\nТы можешь спросить у меня о направлениях БРСМ БГУ с помощью команды /about.\n\nА также, можешь задать вопрос нашему комитету БРСМ БГУ с помощью команды /question.";
            await client.SendTextMessageAsync(chatMessage.Chat.Id, message, replyMarkup: inlineKeyboard);
        }
    }
}
