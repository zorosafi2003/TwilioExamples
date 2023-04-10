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
    public class SendSmsCommand : IRequest<Unit>
    {
        public SendSmsCommandRequest Model { get; set; }
        public SendSmsCommand(SendSmsCommandRequest model)
        {
            Model = model;
        }
        public class SendSmsCommandHandler : IRequestHandler<SendSmsCommand, Unit>
        {
            private readonly ITwilioProvider _twilioProvider;

            public SendSmsCommandHandler(ITwilioProvider TwilioProvider)
            {
                _twilioProvider = TwilioProvider;
            }
            public async Task<Unit> Handle(SendSmsCommand command, CancellationToken cancellationToken)
            {
                var msg = await _twilioProvider.SendSms(command.Model.Body, command.Model.To);

                return Unit.Value;
            }
        }
    }
}
