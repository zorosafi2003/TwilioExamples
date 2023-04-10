using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwilioExamples.Application.Features.SmsFeatures.Requests;
using TwilioExamples.Presistence.Abstruct;

namespace TwilioExamples.Application.Features.SmsFeatures.Commands
{
    public class SendScheduledSmsCommand : IRequest<Unit>
    {
        public SendScheduledSmsCommandRequest Model { get; set; }
        public SendScheduledSmsCommand(SendScheduledSmsCommandRequest model)
        {
            Model = model;
        }
        public class SendScheduledSmsCommandHandler : IRequestHandler<SendScheduledSmsCommand, Unit>
        {
            private readonly ITwilioProvider _twilioProvider;

            public SendScheduledSmsCommandHandler(ITwilioProvider TwilioProvider)
            {
                _twilioProvider = TwilioProvider;
            }
            public async Task<Unit> Handle(SendScheduledSmsCommand command, CancellationToken cancellationToken)
            {
                var msg = await _twilioProvider.SendSms(command.Model.Body, command.Model.To ,command.Model.SentAt);

                return Unit.Value;
            }
        }
    }
}
