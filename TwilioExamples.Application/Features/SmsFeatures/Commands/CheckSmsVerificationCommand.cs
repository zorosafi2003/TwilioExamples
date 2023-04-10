using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwilioExamples.Application.Features.SmsFeatures.Requests;
using TwilioExamples.Presistence.Abstruct;
using static TwilioExamples.Application.Features.SmsFeatures.Commands.CheckSmsVerificationCommand;

namespace TwilioExamples.Application.Features.SmsFeatures.Commands
{
    public class CheckSmsVerificationCommand : IRequest<CheckSmsVerificationCommandResult>
    {
        public CheckSmsVerificationCommandRequest Model { get; set; }
        public CheckSmsVerificationCommand(CheckSmsVerificationCommandRequest model)
        {
            Model = model;
        }
        public class CheckSmsVerificationCommandHandler : IRequestHandler<CheckSmsVerificationCommand, CheckSmsVerificationCommandResult>
        {
            private readonly ITwilioProvider _twilioProvider;

            public CheckSmsVerificationCommandHandler(ITwilioProvider TwilioProvider)
            {
                _twilioProvider = TwilioProvider;
            }

            public async Task<CheckSmsVerificationCommandResult> Handle(CheckSmsVerificationCommand command, CancellationToken cancellationToken)
            {
                var checkVerificationResult = await _twilioProvider.CheckVerification(command.Model.To, command.Model.Code);

                return new CheckSmsVerificationCommandResult
                {
                    IsVerified = checkVerificationResult
                };
            }
        }
        public class CheckSmsVerificationCommandResult
        {
            public bool? IsVerified { get; set; }
        }
    }
}
