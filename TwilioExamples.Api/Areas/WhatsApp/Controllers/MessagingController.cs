using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.AspNet.Core;
using TwilioExamples.Application.Features.WhatsappFeatures.Commands;
using TwilioExamples.Application.Features.WhatsappFeatures.Requests;

namespace TwilioExamples.Api.Areas.WhatsApp.Controllers
{
    [Area("WhatsApp")]
    [Route("[area]/[controller]")]
    public class MessagingController : TwilioController
    {
        private readonly IMediator _mediator;

        public MessagingController(IMediator Mediator)
        {
            _mediator = Mediator;
        }

        [HttpPost("AutoReplay/WebHook")]
        public async Task<IActionResult> AutoReplayWhatsAppMessage(FormCollection formCollection)
        {
            var result = await _mediator.Send(new AutoReplayWhatsAppMessageCommand(formCollection));
            return TwiML(result);
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendWhatsappMessage([FromBody] SendWhatsappMessageCommandRequest model)
        {
            var result = await _mediator.Send(new SendWhatsappMessageCommand(model));
            return Ok(result);
        }
    }
}
