using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Twilio.AspNet.Common;
using Twilio.TwiML;

namespace TwilioExamples.Application.Features.SmsFeatures.Commands
{
    public class AutoReplaySmsCommand : IRequest<MessagingResponse>
    {
        public AutoReplaySmsCommand()
        {
        }
        public class AutoReplaySmsCommandHandler : IRequestHandler<AutoReplaySmsCommand, MessagingResponse>
        {
            public AutoReplaySmsCommandHandler()
            {

            }
            public Task<MessagingResponse> Handle(AutoReplaySmsCommand command, CancellationToken cancellationToken)
            {
                var messagingResponse = new MessagingResponse();

                messagingResponse.Message("We got your message, thank you!");

                return Task.FromResult(messagingResponse);
            }
        }
    }
}
