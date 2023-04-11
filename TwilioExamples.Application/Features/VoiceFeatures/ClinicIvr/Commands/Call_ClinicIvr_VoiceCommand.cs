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
using static TwilioExamples.Models.Models.IvrBaseModel;

namespace TwilioExamples.Application.Features.VoiceFeatures.SimpleIvr.Commands
{
    public class Call_ClinicIvr_VoiceCommand : IRequest<VoiceResponse>
    {
        public string From { get; set; }
        public UrlInfoChildOfIvrBaseModel NextActionUrl { get; set; }
        public Call_ClinicIvr_VoiceCommand(string from , UrlInfoChildOfIvrBaseModel nextActionUrl)
        {
            From = from;
            NextActionUrl = nextActionUrl;
        }
        public class Call_MediumIvr_VoiceCommandHandler : IRequestHandler<Call_ClinicIvr_VoiceCommand, VoiceResponse>
        {
            private string audioDir;
            private readonly ITwilioHelperProvider _twilioHelperProvider;

            public Call_MediumIvr_VoiceCommandHandler(ITwilioHelperProvider TwilioHelperProvider)
            {
                _twilioHelperProvider = TwilioHelperProvider;

                audioDir = Url.Combine("audio", "voice", "mediumIvr");
            }
            public Task<VoiceResponse> Handle(Call_ClinicIvr_VoiceCommand command, CancellationToken cancellationToken)
            {
                var response = new VoiceResponse();

                var dtoModel = new CompanyIvrFlow_VoiceModel
                {
                    From = command.From
                };

                var url = _twilioHelperProvider.ReturnFunctionUrl(new ReturnFunctionUrlModel
                {
                    FunctionName = command.NextActionUrl.ActionName,
                    DtoModel = dtoModel
                });

                _twilioHelperProvider.ReturnAudioFile(response, "Welcome", "Welcome.wav", audioDir);

                response.Redirect(url, method: HttpMethod.Get);

                return Task.FromResult(response);
            }
        }
    }
}
