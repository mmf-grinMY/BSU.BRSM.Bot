using Telegram.Bot.Types;
using Telegram.Bot;

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
        if (Bot.Users.ContainsKey(update.Message.Chat.Id))
        {
            var user = Bot.Users[update.Message.Chat.Id];
            if (user.Questions.Count > 0)
            {
                if (user.Questions[^1].IsQuestionEnded)
                {
                    await client.SendTextMessageAsync(update.Message.Chat.Id, "У Вас нет открытых вопросов для отправки комитету. Чтобы задать вопрос воспользуйтесь командой /question");
                }
                else
                {
                    user.Questions[^1].IsQuestionEnded = true;
                    await client.SendTextMessageAsync(update.Message.Chat.Id, "Ваш вопрос отправлен комитету. Мы постараемся ответить Вам в ближайшее время.");
                }
            }
        }
        else
        {
            await client.SendTextMessageAsync(update.Message.Chat.Id, "У Вас нет открытых вопросов для отправки комитету. Чтобы задать вопрос воспользуйтесь командой /question");
        }
    }
}
