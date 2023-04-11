using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.AspNet.Core;
using TwilioExamples.Application.Features.VoiceFeatures.Enums;
using TwilioExamples.Application.Features.VoiceFeatures.SimpleIvr.Commands;

namespace TwilioExamples.Api.Areas.Voice.Controllers
{
    [Area("Voice")]
    [Route("[area]/[controller]")]
    public class ClinicIvrController : TwilioController
    {
        private readonly IMediator _mediator;

        public ClinicIvrController(IMediator Mediator)
        {
            _mediator = Mediator;
        }

        [HttpGet(ClinicIvrActionsEnum.Call)]
        public async Task<TwiMLResult> Call(string from)
        {
            var response = await _mediator.Send(new Call_ClinicIvr_VoiceCommand(from, new Models.Models.IvrBaseModel.UrlInfoChildOfIvrBaseModel
            {
                ActionName = ClinicIvrActionsEnum.EnterMainMenu
            }));

            return TwiML(response);
        }

        #region MainMenu



        #endregion
    }
}
