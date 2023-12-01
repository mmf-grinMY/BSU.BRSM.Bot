using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace BSU.BRSM.Bot.App.Commands;
public class AboutCommand : BaseCommand
{
    public AboutCommand(ITelegramBotClient client, string name) : base(client, name) { }
    private readonly string message = "<b>Мы строим свою работу по 8 различным направлениям днятельности:</b>\n\n" + 
        "1. Организация вторичной занятости студентов - Штаб трудовых дел БРСМ БГУ;\n\n" +
        "2. Волонтерская деятельность - волонтёрское движение БРСМ БГУ \"Объятия\";\n\n" +
        "3. Копроративно-кадровое направление;\n\n" + 
        "4. Молодежный отряд охраны правопорядка и территориальная добровольная дружина (МООП и ТДД);\n\n" + 
        "5. Работа по внешним связям и фандрайзингу;\n\n" + 
        "6. PR и работа со СМИ - пресс-служба БРСМ БГУ;\n\n" + 
        "7. Молодежный политический клуб;\n\n" + 
        "8. Направление документационного обеспечения.";
    public async override Task Execute(Update update, string message = "")
    {
        switch (update.Type)
        {
            case UpdateType.Message:
                {
                    await client.SendTextMessageAsync(update.Message.Chat.Id, this.message, parseMode: ParseMode.Html);
                }
                break;
            case UpdateType.CallbackQuery:
                {
                    await client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, this.message, parseMode: ParseMode.Html);
                }
                break;
            default: break;
        }
    }
}
