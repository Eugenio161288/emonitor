using Microsoft.AspNetCore.Mvc;

namespace Euromonitor.Server.Api.Controllers
{
    /// <summary>
    /// This controller process errors responses.
    /// </summary>
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
