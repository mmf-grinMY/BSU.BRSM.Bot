using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using BRSM_Bot.Models;

namespace BRSM_Bot.Controllers
{
    [Route("/update")] //webhook uri part
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost]
        public async Task<OkResult> Update(Update update)
        {
            var commands = Bot.Commands;
            var message = update.Message;
            var client = await Bot.Get();

            foreach (var command in commands)
            {
                if (command.Contains(message.Text))
                {
                    command.Execute(message, client);
                    break;
                }
            }

            return Ok();
        }
    }
}
