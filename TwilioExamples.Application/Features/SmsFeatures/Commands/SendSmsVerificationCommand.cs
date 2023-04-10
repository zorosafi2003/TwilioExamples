using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwilioExamples.Presistence.Abstruct;
using static TwilioExamples.Application.Features.SmsFeatures.Commands.SendSmsVerificationCommand;

namespace TwilioExamples.Application.Features.SmsFeatures.Commands
{
    public class SendSmsVerificationCommand : IRequest<SendSmsVerificationCommandResult>
    {
        public string ToPhoneNumber { get; set; }
        public SendSmsVerificationCommand(string toPhoneNumber)
        {
            ToPhoneNumber = toPhoneNumber;
        }
        public class SendSmsVerificationCommandHandler : IRequestHandler<SendSmsVerificationCommand, SendSmsVerificationCommandResult>
        {
            private readonly ITwilioProvider _twilioProvider;

            public SendSmsVerificationCommandHandler(ITwilioProvider TwilioProvider)
            {
                _twilioProvider = TwilioProvider;
            }
            public async Task<SendSmsVerificationCommandResult> Handle(SendSmsVerificationCommand command, CancellationToken cancellationToken)
            {
               var sendVerificationResult = await _twilioProvider.SendVerification(command.ToPhoneNumber,"sms");

                return new SendSmsVerificationCommandResult
                {
                    VerificationSid = sendVerificationResult.Sid
                };
            }
        }

        public class SendSmsVerificationCommandResult
        {
            public string VerificationSid { get; set; }
        }
    }
}
