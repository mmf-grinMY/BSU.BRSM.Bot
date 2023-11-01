using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BSU.BRSM.Bot.Commands
{
    public class StartCommand : Command
    {
        public override string Name => "start";
        public async override Task Execute(Update update, TelegramBotClient client, string message = "")
        {
            var chatMessage = update.Message ?? throw new ArgumentNullException(nameof(update));

            message = $"Привет, {chatMessage.Chat.FirstName}!\n\nТы можешь спросить у меня о направлениях БРСМа БГУ с помощью команды /about.\n\nА также, можешь задать вопрос нашему комитету БРСМа БГУ с помощью команды /question.";
            await client.SendTextMessageAsync(chatMessage.Chat.Id, message);
        }
    }
}
