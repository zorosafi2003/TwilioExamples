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
    public class SendBulkSmsCommand : IRequest<Unit>
    {
        public SendBulkSmsCommandRequest Model { get; set; }
        public SendBulkSmsCommand(SendBulkSmsCommandRequest model)
        {
            Model = model;
        }
        public class SendBulkSmsCommandHandler : IRequestHandler<SendBulkSmsCommand, Unit>
        {
            private readonly ITwilioProvider _twilioProvider;

            public SendBulkSmsCommandHandler(ITwilioProvider TwilioProvider)
            {
                _twilioProvider = TwilioProvider;
            }
            public async Task<Unit> Handle(SendBulkSmsCommand command, CancellationToken cancellationToken)
            {
                var result = new SendBulkSmsCommandResult
                {
                    NumberOfFailed = 0,
                    NumberOfSuccess = 0
                };

                foreach (var item in command.Model.SendToList)
                {
                    try
                    {
                        var body = $"Hi {item.Name} , hope your fine pls rate out service so can help to going better , best regards";

                        await _twilioProvider.SendSms(body, item.PhoneNumber);

                        result.NumberOfSuccess += 1;
                    }
                    catch (Exception)
                    {
                        result.NumberOfFailed += 1;
                    }
                   
                }

                return Unit.Value;
            }

            public class SendBulkSmsCommandResult
            {
                public int NumberOfSuccess { get; set; }
                public int NumberOfFailed { get; set; }
            }
        }
    }
}
