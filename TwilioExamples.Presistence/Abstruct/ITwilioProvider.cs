using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Verify.V2.Service;

namespace TwilioExamples.Presistence.Abstruct
{
    public interface ITwilioProvider
    {
        Task<MessageResource> SendSms(string body, string to, bool isWhatsApp = false, string from = null);
        Task<MessageResource> SendSms(string body, string to, DateTime sendAt, bool isWhatsApp = false, string from = null);
        Task<bool?> GetVerificationStatus(string sid);
        Task<VerificationResource> SendVerification(string to, string channel);
        Task<bool?> CheckVerification(string to, string code);
    }
}
