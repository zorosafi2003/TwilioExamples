using Flurl;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Twilio.AspNet.Common;
using Twilio.Http;
using Twilio.TwiML;
using TwilioExamples.Application.Features.VoiceFeatures.Enums;
using TwilioExamples.Application.Features.VoiceFeatures.SimpleIvr.Models;
using TwilioExamples.Models;
using TwilioExamples.Models.Customs;
using TwilioExamples.Presistence.Abstruct;

namespace TwilioExamples.Application.Features.VoiceFeatures.SimpleIvr.Commands
{
    public class EnterDailActionsWebHook_SimpleIvr_VoiceCommand : IRequest<VoiceResponse>
    {
        public StatusCallbackRequest StatusCallback { get; set; }
        public string Dto { get; set; }
        public EnterDailActionsWebHook_SimpleIvr_VoiceCommand(StatusCallbackRequest statusCallback , string dto)
        {
            StatusCallback = statusCallback;
            Dto = dto;
        }
        public class EnterDailActionsWebHook_SimpleIvr_VoiceCommandHandler : IRequestHandler<EnterDailActionsWebHook_SimpleIvr_VoiceCommand, VoiceResponse>
        {
            private string audioDir;
            private readonly ITwilioHelperProvider _twilioHelperProvider;

            public EnterDailActionsWebHook_SimpleIvr_VoiceCommandHandler(ITwilioHelperProvider TwilioHelperProvider)
            {
                _twilioHelperProvider = TwilioHelperProvider;

                audioDir = Url.Combine("audio", "voice", "simpleIvr");
            }
            public Task<VoiceResponse> Handle(EnterDailActionsWebHook_SimpleIvr_VoiceCommand command, CancellationToken cancellationToken)
            {
                var response = new VoiceResponse();

                var badStatusCodes = new HashSet<string> { "busy", "no-answer", "canceled", "failed" };

                if (!badStatusCodes.Contains(command.StatusCallback.DialCallStatus))
                {
                    return Task.FromResult(response);
                }

               var url = _twilioHelperProvider.ReturnFunctionUrl(new ReturnFunctionUrlModel
                {
                    FunctionName = CompanyIvrActionsEnum.ConfirmDailActionsWebHook,
                    DtoModel = JsonConvert.DeserializeObject<CompanyIvrFlow_VoiceModel>(command.Dto)
                });

                var gather = new CustomGather(action: url, numDigits: 1, method: HttpMethod.Get, timeout: 5);

                _twilioHelperProvider.ReturnAudioFile(gather, "Sorry we are busy now. If you would like to receive a callback, press 1. If not, press 2 or hang up.", "Enter_Dail_Actions_WebHook.wav", audioDir);

                response.Append(gather).Redirect(url);

                return Task.FromResult(response);
            }
        }
    }
}
