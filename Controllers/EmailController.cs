using BitmailServer.Interfaces;
using BitmailServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace BitmailServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailDto emailDto)
        {
            try
            {
                await _emailService.SendEmailAsync(emailDto);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                return BadRequest(new { message = "Failed to send email", error = e.Message });
            }

            return Ok();
        }

        [HttpGet("inbox")]
        public async Task<IActionResult> GetInbox([FromQuery] int page = 0, [FromQuery] int count = 10)
        {
            return Ok(await _emailService.GetInboxAsync(page, count));
        }
    }
}