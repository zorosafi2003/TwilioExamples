using Flurl;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Twilio.Http;
using Twilio.TwiML;
using TwilioExamples.Application.Features.VoiceFeatures.SimpleIvr.Models;
using TwilioExamples.Models;
using TwilioExamples.Models.Customs;
using TwilioExamples.Models.Models;
using TwilioExamples.Presistence.Abstruct;

namespace TwilioExamples.Application.Features.VoiceFeatures.SimpleIvr.Commands
{
    public class EnterMainMenu_SimpleIvr_VoiceCommand : IRequest<VoiceResponse>
    {
        public IvrBaseModel Model { get; set; }
        public EnterMainMenu_SimpleIvr_VoiceCommand(IvrBaseModel model)
        {
            Model = model;
        }
        public class EnterMainMenu_SimpleIvr_VoiceCommandHandler : IRequestHandler<EnterMainMenu_SimpleIvr_VoiceCommand, VoiceResponse>
        {
            private string audioDir;
            private readonly ITwilioHelperProvider _twilioHelperProvider;

            public EnterMainMenu_SimpleIvr_VoiceCommandHandler(ITwilioHelperProvider TwilioHelperProvider)
            {
                _twilioHelperProvider = TwilioHelperProvider;

                audioDir = Url.Combine("audio", "voice", "simpleIvr");
            }
            public Task<VoiceResponse> Handle(EnterMainMenu_SimpleIvr_VoiceCommand command, CancellationToken cancellationToken)
            {
                var response = new VoiceResponse();

                CompanyIvrFlow_VoiceModel dtoModel = JsonConvert.DeserializeObject<CompanyIvrFlow_VoiceModel>(command.Model.Dto);

                var url = _twilioHelperProvider.ReturnFunctionUrl(new ReturnFunctionUrlModel
                {
                    FunctionName = command.Model.NextActionUrl.ActionName,
                    DtoModel = dtoModel
                });

                var gather = new CustomGather(action: url, numDigits: 1, method: HttpMethod.Get);

                _twilioHelperProvider.ReturnAudioFile(gather, "for sales press 1 for support press 2", "Enter_Main_Menu.wav", audioDir);

                response.Append(gather).Redirect(url, method: HttpMethod.Get);

                return Task.FromResult(response);
            }
        }
    }
}
