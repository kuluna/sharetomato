using Microsoft.AspNetCore.Mvc;

namespace Sharetomato.Controllers
{
    /// <summary>
    /// Default MVC Controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Open the wwwroot/index.html
        /// </summary>
        public IActionResult Index() => File("/index.html", "text/html");
    }
}
