using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwilioExamples.Application.Features.SmsFeatures.Commands;
using TwilioExamples.Application.Features.SmsFeatures.Queries;
using TwilioExamples.Application.Features.SmsFeatures.Requests;

namespace TwilioExamples.Api.Areas.Sms.Controllers
{
    [Area("Sms")]
    [Route("[area]/[controller]")]
    public class VerificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VerificationController(IMediator Mediator)
        {
            _mediator = Mediator;
        }

        [HttpGet("Status/{verificationSid}")]
        public async Task<IActionResult> GetSmsVerificationStatus([FromRoute] string verificationSid)
        {
            var result = await _mediator.Send(new GetSmsVerificationStatusQuery(verificationSid));
            return Ok(result);
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendSmsVerification([FromQuery] string to)
        {
            var result = await _mediator.Send(new SendSmsVerificationCommand(to));
            return Ok(result);
        }

        [HttpPost("Verify")]
        public async Task<IActionResult> CheckSmsVerification([FromBody] CheckSmsVerificationCommandRequest model)
        {
            var result = await _mediator.Send(new CheckSmsVerificationCommand(model));
            return Ok(result);
        }

    }
}
