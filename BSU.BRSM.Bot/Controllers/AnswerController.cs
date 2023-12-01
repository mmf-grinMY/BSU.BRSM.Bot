using BSU.BRSM.Bot.App;
using BSU.BRSM.Bot.Core;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;

namespace BSU.BRSM.Bot.Controllers;

[Route("Answer/")]
public class AnswerController : Controller
{
    private readonly IUserRepository userRepository;
    private readonly BrsmBot bot;
    public AnswerController(IUserRepository userRepository, BrsmBot bot)
    {
        this.userRepository = userRepository;
        this.bot = bot;
    }
    [HttpGet]
    [Route("/Answer/Index")]
    public async Task<IActionResult> IndexAsync(int id)
    {
        var question = await userRepository.GetQuestionByIdAsync(id);
        await userRepository.UpdateAsync();
        if (question.IsPosted)
            return Redirect($"/Answer/Posted?id={id}");
        return View("Index", question);
    }
    [HttpPost]
    [Route("/Answer/Index")]
    public async Task<IActionResult> IndexAsync(int id, string query)
    {
        var question = await userRepository.GetQuestionByIdAsync(id);
        if (query != string.Empty)
        {
            question.Answer = query;
            question.IsPosted = true;
            await userRepository.UpdateAsync();
            await bot.Client.SendTextMessageAsync(question.ChatId, question.Answer, replyToMessageId: question.MessageId);
        }
        return Redirect("/");
    }
    [HttpPost]
    [Route("/Answer/Remove")]
    public async Task<IActionResult> RemoveAsync(int id)
    {
        await userRepository.RemoveByIdAsync(id);
        return Redirect("/");
    }
    [HttpGet]
    [Route("/Answer/Posted")]
    public async Task<IActionResult> PostedAsync(int id)
    {
        var question = await userRepository.GetQuestionByIdAsync(id);
        var owner = await userRepository.GetByChatIdAsync(question.ChatId);
        ViewData["FirstName"] = owner.FirstName;
        ViewData["UserName"] = owner.UserName;
        return View("Posted", question);
    }
    [Route("/Answer/Edit")]
    public async Task<IActionResult> EditAsync(int id)
    {
        var question = await userRepository.GetQuestionByIdAsync(id);
        question.IsPosted = false;
        await userRepository.UpdateAsync();
        return Redirect($"Index?id={id}");
    }
}