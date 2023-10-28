using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace BSU.BRSM.Bot.Commands;

public class AboutCommand : Command
{
    public override string Name => "about";
    private readonly string _message = "<b>Мы строим свою работу по 8 различным направлениям днятельности:</b>\n\n" + 
        "1. Организация вторичной занятости студентов - Штаб трудовых дел БРСМ БГУ;\n\n" +
        "2. Волонтерская деятельность - волонтёрское движение БРСМ БГУ \"Объятия\";\n\n" +
        "3. Копроративно-кадровое направление;\n\n" + 
        "4. Молодежный отряд охраны правопорядка и территориальная добровольная дружина (МООП и ТДД);\n\n" + 
        "5. Работа по внешним связям и фандрайзингу;\n\n" + 
        "6. PR и работа со СМИ - пресс-служба БРСМ БГУ;\n\n" + 
        "7. Молодежный политический клуб;\n\n" + 
        "8. Навправление документационного обеспечения.";

    public async override Task Execute(Update update, TelegramBotClient client, string message = "")
    {
        if (update is null) return;
        if (update.Message is null) return;
        await client.SendTextMessageAsync(update.Message.Chat.Id, _message, parseMode: ParseMode.Html);
    }
}
