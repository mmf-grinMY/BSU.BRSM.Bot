using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using BSU.BRSM.Bot.App;
using Telegram.Bot.Types.Enums;
using BSU.BRSM.Bot.Core;

namespace BSU.BRSM.Bot.Controllers;

[ApiController]
[Route("/")]
public class HomeController : Controller
{
    private readonly BrsmBot bot;
    private readonly IUserRepository userRepository;
    public HomeController(IUserRepository userRepository, BrsmBot bot)
    {
        this.userRepository = userRepository;
        this.bot = bot;
    }
    [HttpPost]
    public async void Post(Update update)
    {
        if (update is not null)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    {
                        if (update.Message is Message chatMessage)
                        {
                            if (chatMessage.Text is string text)
                            {
                                var chatId = update.Message.Chat.Id;
                                var commands = bot.Commands;

                                //Core.Models.User user = Core.Models.User.Mapper.Map(Core.Models.User.DtoFactory.Create(chatId, message.Chat.FirstName, message.Chat.Username, new List<Core.Models.Question>()));
                                if (text[0] == '/')
                                {
                                    try
                                    {
                                        var command = bot.Commands.Single(x => "/" + x.Name == text);
                                        await command.Execute(update);
                                    }
                                    catch (InvalidOperationException) { }
                                    //if (user.IsQuestionCommand && !text.StartsWith("/endquestion"))
                                    //{
                                    //    if (Bot.Commands.Any(x => text.StartsWith("/" + x.Name)))
                                    //    {
                                    //        await bot.SendTextMessageAsync(chatId, "Вы не можете пользоваться другими командами во время ввода вопроса! Сначало завершите ввод вопроса с помощью команды /endquestion.");
                                    //    }
                                    //    else
                                    //    {
                                    //        await user.AddQuestion(text);
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    string chatMessage = string.Empty;
                                    //    Command? command = Bot.Commands.FirstOrDefault(x => text.StartsWith("/" + x.Name));

                                    //    if (command is not null)
                                    //    {
                                    //        if (command is QuestionCommand)
                                    //        {
                                    //            chatMessage = (text = text.Replace("/question", "").Trim()).Length != 0 ?
                                    //                await user.AddQuestion(text) : await user.AddQuestionPart("");
                                    //        }

                                    //        await command.Execute(update, bot, chatMessage);
                                    //    }
                                    //    else
                                    //    {
                                    //        await bot.SendTextMessageAsync(chatId, "Неверная команда!");
                                    //    }
                                    //}
                                }
                                else
                                {
                                    var user = await userRepository.GetByChatIdAsync(chatId);
                                    
                                    if (!user.IsQuestionEnded)
                                    {
                                        if (user.Questions.Any())
                                        {
                                            var question = user.Questions.Last();
                                            question.Body = text;
                                            question.IsEnded = true;
                                            question.MessageId = chatMessage.MessageId;
                                            await userRepository.UpdateAsync();
                                            string message = "Ваш вопрос отправлен комитету.";
                                            await bot.Client.SendTextMessageAsync(chatId, message);
                                        }

                                    }
                                    //if (user.IsQuestionCommand)
                                    //{
                                    //    await user.AddQuestionPart(text);
                                    //}
                                    //else
                                    //{
                                    //    try
                                    //    {
                                    //        int messageId = message.MessageId;
                                    //        await bot.SendTextMessageAsync(message.Chat.Id, "Для того, чтобы задать боту вопрос введите команду /question!", replyToMessageId: messageId - 1);
                                    //    }
                                    //    catch (Exception ex)
                                    //    {

                                    //    }
                                    //}
                                }
                            }
                        }
                    }
                    break;
                case UpdateType.CallbackQuery:
                    {
                        var command = bot.Commands.Single(x => x.Name == update.CallbackQuery.Data);
                        await command.Execute(update);
                    }
                    break;
                default: break;
            }
        }
    }
    //[HttpGet]
    //public IActionResult Index()
    //{
    //    //ViewData["Model"] = userService.GetAll();
    //    //return View(model: ViewData["Model"]);
    //    return View(userRepository.GetAll());
    //}
    [HttpGet]
    public async Task<IActionResult> IndexAsync()
    {
        return View(await userRepository.GetAllAsync());
    }
    public IActionResult Questions()
    {
        return PartialView();
    }
    [Route("Home/UserInfo")]
    public async Task<IActionResult> UserInfoAsync(long chatId)
    {
        return View(await userRepository.GetByChatIdAsync(chatId));
    }
    [Route("Home/Search")]
    public async Task<IActionResult> SearchAsync(string query)
    {
        return View(await userRepository.GetByFirstOrLastNameAsync(query));
    }
}