using Microsoft.AspNetCore.Mvc;
using ScPlayerAPI.Services;

namespace ScPlayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        ILogger<MailController> _logger;
        MailSender _sender;
        public MailController(ILogger<MailController> logger, MailSender mailSender)
        {
            _logger = logger;
            _sender = mailSender;
        }


        [HttpGet(nameof(MailController))]
        public IActionResult SendMessage(string messageBody)
        {
            string result = "ok";
            try
            {//
                _sender.SendMessage(messageBody);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }


    }
}
