using Telegram.Bot;
using Telegram.Bot.Types;

namespace BRSM_Bot.Models.Commands
{
    public class HelloCommand : Command
    {
        public override string Name => "hello";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            //TODO: Bot logic

            await client.SendTextMessageAsync(message.Chat.Id, "Hello!");
        }
    }
}
