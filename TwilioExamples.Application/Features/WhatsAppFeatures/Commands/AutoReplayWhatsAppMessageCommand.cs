using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Twilio.TwiML;

namespace TwilioExamples.Application.Features.WhatsappFeatures.Commands
{
    public class AutoReplayWhatsAppMessageCommand : IRequest<MessagingResponse>
    {
        public FormCollection FormCollection { get; set; }
        public AutoReplayWhatsAppMessageCommand(FormCollection formCollection)
        {
            FormCollection = formCollection;
        }
        public class AutoReplayWhatsAppMessageCommandHandler : IRequestHandler<AutoReplayWhatsAppMessageCommand, MessagingResponse>
        {
            public AutoReplayWhatsAppMessageCommandHandler()
            {

            }
            public Task<MessagingResponse> Handle(AutoReplayWhatsAppMessageCommand command, CancellationToken cancellationToken)
            {
                var numMedia = int.Parse(command.FormCollection["NumMedia"]);

                var response = new MessagingResponse();

                if (numMedia == 0)
                {
                    response.Message("Thanks you , we will contact you soon .");

                }

                return Task.FromResult(response);
            }
        }
    }
}
