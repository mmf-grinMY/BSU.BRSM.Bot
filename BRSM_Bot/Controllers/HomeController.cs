using BRSM_Bot.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BRSM_Bot.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "It`s my telegram bot D:";
        }
    }
}