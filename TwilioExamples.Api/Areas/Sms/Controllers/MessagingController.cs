using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwilioExamples.Application.Features.SmsFeatures.Commands;
using TwilioExamples.Application.Features.SmsFeatures.Requests;

namespace TwilioExamples.Api.Areas.Sms.Controllers
{
    [Area("Sms")]
    [Route("[area]/[controller]")]
    public class MessagingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessagingController(IMediator Mediator)
        {
            _mediator = Mediator;
        }

        [HttpPost("AutoReplay/WebHook")]
        public async Task<IActionResult> AutoReplaySms()
        {
            var result = await _mediator.Send(new AutoReplaySmsCommand());
            return Ok(result);
        }

        [HttpPost("SendBulkSms")]
        public async Task<IActionResult> SendBulkSms([FromBody] SendBulkSmsCommandRequest model)
        {
            var result = await _mediator.Send(new SendBulkSmsCommand(model));
            return Ok(result);
        }     

        [HttpPost("SendSms")]
        public async Task<IActionResult> SendSms([FromBody] SendSmsCommandRequest model)
        {
            var result = await _mediator.Send(new SendSmsCommand(model));
            return Ok(result);
        }

        [HttpPost("SendSms/Scheduled")]
        public async Task<IActionResult> SendScheduledSms([FromBody] SendScheduledSmsCommandRequest model)
        {
            var result = await _mediator.Send(new SendScheduledSmsCommand(model));
            return Ok(result);
        }
    }
}
