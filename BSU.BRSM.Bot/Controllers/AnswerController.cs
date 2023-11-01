using BSU.BRSM.Bot.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace BSU.BRSM.Bot.Controllers
{
    public class AnswerController : Controller
    {
        private static readonly int messageLength = 1000;
        [HttpGet]
        public async Task<IActionResult> Index(string chatId, string dateTime)
        {
            var question = await Models.User.GetQuestion(chatId, dateTime);
            ViewData.Model = question;
            return View();
        }
        [HttpPost]
        public async Task Index(string chatId, string dateTime, string answer)
        {
            var question = await Models.User.GetQuestion(chatId, dateTime) ?? throw new ArgumentNullException(nameof(chatId));
            string message = $"*Вопрос*: {question.Body}\n *Ответ*: {answer}";
            //if (message.Length <= messageLength)
            //{
            //}
            //else
            //{
            //    await Bot.GetTelegramBot().SendTextMessageAsync(chatId, "Слишком длинный вопрос! Попробуй разделить его на несколько частей.");
            //    await Models.User.DeleteQuestion(question.ChatId, question.DateTime);
            //}
            await Bot.GetTelegramBot().SendTextMessageAsync(chatId, message, parseMode: ParseMode.MarkdownV2);

            await question.Close();
            Response.Redirect("/");
        }
    }
}