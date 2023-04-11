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
using TwilioExamples.Application.Features.VoiceFeatures.Enums;
using TwilioExamples.Application.Features.VoiceFeatures.SimpleIvr.Models;
using TwilioExamples.Models;
using TwilioExamples.Models.Models;
using TwilioExamples.Presistence.Abstruct;

namespace TwilioExamples.Application.Features.VoiceFeatures.SimpleIvr.Commands
{
    public class ConfirmMainMenu_SimpleIvr_VoiceCommand : IRequest<VoiceResponse>
    {
        public IvrBaseModel Model { get; set; }
        public ConfirmMainMenu_SimpleIvr_VoiceCommand(IvrBaseModel model)
        {
            Model = model;
        }
        public class ConfirmMainMenu_SimpleIvr_VoiceCommandHandler : IRequestHandler<ConfirmMainMenu_SimpleIvr_VoiceCommand, VoiceResponse>
        {
            private string audioDir;
            private readonly ITwilioHelperProvider _twilioHelperProvider;

            public ConfirmMainMenu_SimpleIvr_VoiceCommandHandler(ITwilioHelperProvider TwilioHelperProvider)
            {
                _twilioHelperProvider = TwilioHelperProvider;

                audioDir = Url.Combine("audio", "voice", "simpleIvr");
            }
            public Task<VoiceResponse> Handle(ConfirmMainMenu_SimpleIvr_VoiceCommand command, CancellationToken cancellationToken)
            {
                Uri url = null;

                var response = new VoiceResponse();

                CompanyIvrFlow_VoiceModel dtoModel = command.Model.GetDtoModel<CompanyIvrFlow_VoiceModel>();

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
                else if (command.Model.Digits == "1" || command.Model.Digits == "2")
                {
                    var dailActionsUrl = _twilioHelperProvider.ReturnFunctionUrl(new ReturnFunctionUrlModel
                    {
                        FunctionName = CompanyIvrActionsEnum.EnterDailActionsWebHook,
                        ControllerName = CompanyIvrActionsEnum.ControllerName,
                        AreaName = CompanyIvrActionsEnum.AreaName,
                        DtoModel = dtoModel
                    }); 

                    if (command.Model.Digits == "1")
                    {
                        _twilioHelperProvider.ReturnAudioFile(response, "Please wait while we contact sales department", "Please_Wait_While_Contact_Sales.wav", audioDir);
                        response.Dial("+1515151511",action: dailActionsUrl , method:HttpMethod.Post);
                    }
                    else
                    {
                        _twilioHelperProvider.ReturnAudioFile(response, "Please wait while we contact support department", "Please_Wait_While_Contact_Support.wav", audioDir);
                        response.Dial("+1515151512", action: dailActionsUrl, method: HttpMethod.Post);
                    }

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
