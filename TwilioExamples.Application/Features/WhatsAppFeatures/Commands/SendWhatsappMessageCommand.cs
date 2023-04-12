using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwilioExamples.Application.Features.WhatsappFeatures.Requests;
using TwilioExamples.Presistence.Abstruct;

namespace TwilioExamples.Application.Features.WhatsappFeatures.Commands
{
    public class SendWhatsappMessageCommand : IRequest<Unit>
    {
        public SendWhatsappMessageCommandRequest Model { get; set; }
        public SendWhatsappMessageCommand(SendWhatsappMessageCommandRequest model)
        {
            Model = model;
        }
        public class SendWhatsappMessageCommandHandler : IRequestHandler<SendWhatsappMessageCommand, Unit>
        {
            private readonly ITwilioProvider _twilioProvider;

            public SendWhatsappMessageCommandHandler(ITwilioProvider TwilioProvider)
            {
                _twilioProvider = TwilioProvider;
            }
            public async Task<Unit> Handle(SendWhatsappMessageCommand command, CancellationToken cancellationToken)
            {
                await _twilioProvider.SendSms(command.Model.Message, command.Model.To, true, command.Model.From);

                return Unit.Value;
            }
        }
    }
}
