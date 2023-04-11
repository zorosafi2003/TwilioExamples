using Flurl;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Twilio.TwiML;
using TwilioExamples.Application.Features.VoiceFeatures.SimpleIvr.Models;
using TwilioExamples.Presistence.Abstruct;

namespace TwilioExamples.Application.Features.VoiceFeatures.SimpleIvr.Commands.DailActionsWebHook
{
    public class ConfirmDailActionsWebHook_SimpleIvr_VoiceCommand : IRequest<VoiceResponse>
    {
        public string Digits { get; set; }
        public string Dto { get; set; }
        public ConfirmDailActionsWebHook_SimpleIvr_VoiceCommand(string digits, string dto)
        {
            Digits = digits;
            Dto = dto;
        }
        public class ConfirmDailActionsWebHook_SimpleIvr_VoiceCommandHandler : IRequestHandler<ConfirmDailActionsWebHook_SimpleIvr_VoiceCommand, VoiceResponse>
        {
            private string audioDir;
            private readonly ITwilioHelperProvider _twilioHelperProvider;
            private readonly ITwilioProvider _twilioProvider;

            public ConfirmDailActionsWebHook_SimpleIvr_VoiceCommandHandler(ITwilioHelperProvider TwilioHelperProvider, ITwilioProvider TwilioProvider)
            {
                _twilioHelperProvider = TwilioHelperProvider;
                _twilioProvider = TwilioProvider;

                audioDir = Url.Combine("audio", "voice", "simpleIvr");
            }
            public async Task<VoiceResponse> Handle(ConfirmDailActionsWebHook_SimpleIvr_VoiceCommand command, CancellationToken cancellationToken)
            {
                var response = new VoiceResponse();

                if (command.Digits == "1")
                {
                    var dtoModel = JsonConvert.DeserializeObject<CompanyIvrFlow_VoiceModel>(command.Dto);

                    _twilioHelperProvider.ReturnAudioFile(response, "Thanks will call you soon", "Thanks_Will_Call_Soon.wav", audioDir);

                    await _twilioProvider.SendSms($"we appointment call for customer number {dtoModel.From}", "+1515151513");
                }
                else if (command.Digits == "2")
                {
                    _twilioHelperProvider.ReturnAudioFile(response, "Thanks for calling", "Thanks_For_Calling.wav", audioDir);
                }

                response.Hangup();

                return response;
            }
        }
    }
}
