using Flurl;
using MediatR;
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
using TwilioExamples.Models.Models;
using TwilioExamples.Presistence.Abstruct;

namespace TwilioExamples.Application.Features.VoiceFeatures.ClinicIvr.Commands.MainMenu
{
    public class ConfirmMainMenu_ClinicIvr_VoiceCommand : IRequest<VoiceResponse>
    {
        public IvrBaseModel Model { get; set; }
        public ConfirmMainMenu_ClinicIvr_VoiceCommand(IvrBaseModel model)
        {
            Model = model;
        }
        public class ConfirmMainMenu_ClinicIvr_VoiceCommandHandler : IRequestHandler<ConfirmMainMenu_ClinicIvr_VoiceCommand, VoiceResponse>
        {
            private string audioDir;
            private readonly ITwilioHelperProvider _twilioHelperProvider;

            public ConfirmMainMenu_ClinicIvr_VoiceCommandHandler(ITwilioHelperProvider TwilioHelperProvider)
            {
                _twilioHelperProvider = TwilioHelperProvider;

                audioDir = Url.Combine("audio", "voice", "clinicIvr");
            }
            public Task<VoiceResponse> Handle(ConfirmMainMenu_ClinicIvr_VoiceCommand command, CancellationToken cancellationToken)
            {
                Uri url = null;

                var response = new VoiceResponse();

                var dtoModel = command.Model.GetDtoModel<ClinicIvrFlow_VoiceModel>();

                Uri backUrl = _twilioHelperProvider.ReturnFunctionUrl(new ReturnFunctionUrlModel
                {
                    FunctionName = command.Model.BackActionUrl.ActionName,
                    ControllerName = command.Model.BackActionUrl.ControllerName,
                    AreaName = command.Model.BackActionUrl.AreaName,
                    DtoModel = dtoModel
                });

                if (string.IsNullOrEmpty(command.Model.Digits))
                {
                    url = backUrl;
                }
                else if (command.Model.Digits == "1")
                {

                }
                else if (command.Model.Digits == "2")
                {

                }
                else if (command.Model.Digits == "3")
                {

                }
                else
                {
                    url = backUrl;

                    _twilioHelperProvider.ReturnAudioFile(response, "sorry your entery is incorrect", "Incorrect_Entry.wav", audioDir);
                }

                response.Redirect(url, HttpMethod.Get);

                return Task.FromResult(response);
            }
        }
    }
}
