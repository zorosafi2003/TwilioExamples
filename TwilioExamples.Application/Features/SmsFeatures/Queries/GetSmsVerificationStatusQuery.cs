using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwilioExamples.Presistence.Abstruct;
using static TwilioExamples.Application.Features.SmsFeatures.Queries.GetSmsVerificationStatusQuery;

namespace TwilioExamples.Application.Features.SmsFeatures.Queries
{
   public class GetSmsVerificationStatusQuery :IRequest<GetSmsVerificationStatusCommandResult>
    {
        public string VerificationSid { get; set; }
        public GetSmsVerificationStatusQuery(string verificationSid)
        {
            VerificationSid = verificationSid;
        }
        public class GetSmsVerificationStatusQueryHandler : IRequestHandler<GetSmsVerificationStatusQuery, GetSmsVerificationStatusCommandResult>
        {
            private readonly ITwilioProvider _twilioProvider;

            public GetSmsVerificationStatusQueryHandler(ITwilioProvider TwilioProvider)
            {
                _twilioProvider = TwilioProvider;
            }
            public async Task<GetSmsVerificationStatusCommandResult> Handle(GetSmsVerificationStatusQuery request, CancellationToken cancellationToken)
            {
                var getVerificationStatusResult = await _twilioProvider.GetVerificationStatus(request.VerificationSid);

                return new GetSmsVerificationStatusCommandResult
                {
                    IsVerified = getVerificationStatusResult
                };
            }
        }
        public class GetSmsVerificationStatusCommandResult
        {
            public bool? IsVerified { get; set; }
        }
    }
}
