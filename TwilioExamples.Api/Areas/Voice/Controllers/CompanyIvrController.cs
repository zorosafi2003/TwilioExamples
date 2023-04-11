using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using TwilioExamples.Application.Features.VoiceFeatures.Enums;
using TwilioExamples.Application.Features.VoiceFeatures.SimpleIvr.Commands;
using TwilioExamples.Application.Features.VoiceFeatures.SimpleIvr.Commands.DailActionsWebHook;
using TwilioExamples.Models.Models;

namespace TwilioExamples.Api.Areas.Voice.Controllers
{
    [Area("Voice")]
    [Route("[area]/[controller]")]
    public class CompanyIvrController : TwilioController
    {
        private readonly IMediator _mediator;

        public CompanyIvrController(IMediator Mediator)
        {
            _mediator = Mediator;
        }

        [HttpGet(CompanyIvrActionsEnum.Call)]
        public async Task<TwiMLResult> Call(string from)
        {
            var response = await _mediator.Send(new Call_SimpleIvr_VoiceCommand(from, new  Models.Models.IvrBaseModel.UrlInfoChildOfIvrBaseModel
            {
                ActionName = CompanyIvrActionsEnum.EnterMainMenu
            }));

            return TwiML(response);
        }

        #region MainMenu

        [HttpGet(CompanyIvrActionsEnum.EnterMainMenu)]
        public async Task<TwiMLResult> EnterMainMenu(string dto)
        {
            var response = await _mediator.Send(new EnterMainMenu_SimpleIvr_VoiceCommand(new IvrBaseModel
            {
                Dto = dto,
                NextActionUrl = new IvrBaseModel.UrlInfoChildOfIvrBaseModel
                {
                    ActionName = CompanyIvrActionsEnum.ConfirmMainMenu
                }
            }));

            return TwiML(response);
        }

        [HttpGet(CompanyIvrActionsEnum.ConfirmMainMenu)]
        public async Task<TwiMLResult> ConfirmMainMenu(string digits, string dto)
        {
            var response = await _mediator.Send(new ConfirmMainMenu_SimpleIvr_VoiceCommand(new IvrBaseModel
            {
                Digits = digits,
                Dto = dto,
                BackActionUrl = new IvrBaseModel.UrlInfoChildOfIvrBaseModel
                {
                    ActionName = CompanyIvrActionsEnum.EnterMainMenu
                }
            }));

            return TwiML(response);
        }

        #endregion

        #region DailActionsWebHook

        [HttpPost(CompanyIvrActionsEnum.EnterDailActionsWebHook)]
        public async Task<TwiMLResult> EnterDailActionsWebHook([FromForm]StatusCallbackRequest statusCallback,[FromQuery] string dto)
        {
            var response = await _mediator.Send(new EnterDailActionsWebHook_SimpleIvr_VoiceCommand(statusCallback , dto));

            return TwiML(response);
        }

        [HttpPost(CompanyIvrActionsEnum.ConfirmDailActionsWebHook)]
        public async Task<TwiMLResult> ConfirmDailActionsWebHook([FromQuery] string digits, [FromQuery] string dto)
        {
            var response = await _mediator.Send(new ConfirmDailActionsWebHook_SimpleIvr_VoiceCommand(digits, dto));

            return TwiML(response);
        }

        #endregion

    }
}
