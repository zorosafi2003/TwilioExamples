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

namespace TwilioExamples.Application.Features.VoiceFeatures.ClinicIvr.Commands.MainMenu
{
    public class EnterMainMenu_ClinicIvr_VoiceCommand : IRequest<VoiceResponse>
    {
        public IvrBaseModel Model { get; set; }
        public EnterMainMenu_ClinicIvr_VoiceCommand(IvrBaseModel model)
        {
            Model = model;
        }
        public class EnterMainMenu_ClinicIvr_VoiceCommandHandler : IRequestHandler<EnterMainMenu_ClinicIvr_VoiceCommand, VoiceResponse>
        {
            private string audioDir;
            private readonly ITwilioHelperProvider _twilioHelperProvider;

            public EnterMainMenu_ClinicIvr_VoiceCommandHandler(ITwilioHelperProvider TwilioHelperProvider)
            {
                _twilioHelperProvider = TwilioHelperProvider;

                audioDir = Url.Combine("audio", "voice", "clinicIvr");
            }
            public Task<VoiceResponse> Handle(EnterMainMenu_ClinicIvr_VoiceCommand command, CancellationToken cancellationToken)
            {
                var response = new VoiceResponse();

                var dtoModel = JsonConvert.DeserializeObject<ClinicIvrFlow_VoiceModel>(command.Model.Dto);

                var url = _twilioHelperProvider.ReturnFunctionUrl(new ReturnFunctionUrlModel
                {
                    FunctionName = command.Model.NextActionUrl.ActionName,
                    DtoModel = dtoModel
                });

                var gather = new CustomGather(action: url, numDigits: 1, method: HttpMethod.Get);

                _twilioHelperProvider.ReturnAudioFile(gather, "if you want to Scheduling an appointment press 1 to check your next appointment press 2 to speak with support press 3 ", "Enter_Main_Menu.wav", audioDir);

                response.Append(gather).Redirect(url, method: HttpMethod.Get);

                return Task.FromResult(response);
            }
        }
    }
}
