using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Verify.V2.Service;
using TwilioExamples.Presistence.Abstruct;

namespace TwilioExamples.Presistence.Concrete
{
    public class TwilioProvider : ITwilioProvider
    {
        private string accountSid;
        private string authToken;

        private readonly IConfiguration _configuration;

        public TwilioProvider(IConfiguration Configuration)
        {
            _configuration = Configuration;

            accountSid = _configuration["TwilioConfig:AccountSid"].ToString();
            authToken = _configuration["TwilioConfig:AuthToken"].ToString();
        }
        public async Task<MessageResource> SendSms(string body, string to, bool isWhatsApp = false, string from = null)
        {
            if (string.IsNullOrEmpty(from))
            {
                if (isWhatsApp == false)
                {
                    from = _configuration["TwilioConfig:Sms:From"].ToString();
                }
                else
                {
                    from = _configuration["TwilioConfig:WhatsApp:From"].ToString();
                }
            }

            if (isWhatsApp == true)
            {
                from = from.Contains("whatsapp:") == true ? from : $"whatsapp:{from}";
                to = to.Contains("whatsapp:") == true ? to : $"whatsapp:{to}";
            }

            TwilioClient.Init(accountSid, authToken);

            var createMessageModel = new CreateMessageOptions(new Twilio.Types.PhoneNumber(to))
            {
                Body = body,
                From = new Twilio.Types.PhoneNumber(from),
            };

            var message = await MessageResource.CreateAsync(createMessageModel);

            return message;
        }

        public async Task<MessageResource> SendSms(string body, string to, DateTime sendAt, bool isWhatsApp = false, string from = null)
        {
            var messagingServiceSid = _configuration["TwilioConfig:MessagingServiceSid"].ToString();

            if (string.IsNullOrEmpty(from))
            {
                if (isWhatsApp == false)
                {
                    from = _configuration["TwilioConfig:Sms:From"].ToString();
                }
                else
                {
                    from = _configuration["TwilioConfig:WhatsApp:From"].ToString();
                }
            }

            if (isWhatsApp == true)
            {
                from = from.Contains("whatsapp:") == true ? from : $"whatsapp:{from}";
                to = to.Contains("whatsapp:") == true ? to : $"whatsapp:{to}";
            }

            TwilioClient.Init(accountSid, authToken);

            var createMessageModel = new CreateMessageOptions(new Twilio.Types.PhoneNumber(to))
            {
                MessagingServiceSid = messagingServiceSid,
                Body = body,
                From = new Twilio.Types.PhoneNumber(from),
                SendAt = sendAt,
                ScheduleType = MessageResource.ScheduleTypeEnum.Fixed
            };

            var message = await MessageResource.CreateAsync(createMessageModel);

            return message;
        }

        public async Task<bool?> GetVerificationStatus(string sid)
        {
            var verificationServiceSid = _configuration["TwilioConfig:VerificationServiceSid"].ToString();

            TwilioClient.Init(accountSid, authToken);

            var verificationResource = await VerificationResource.FetchAsync(pathServiceSid: verificationServiceSid, pathSid: sid);

            if (verificationResource.Status == "approved")
            {
                return true;
            }
            else if (verificationResource.Status == "canceled")
            {
                return false;
            }
            else
            {
                return null;
            }
        }

        public async Task<VerificationResource> SendVerification(string to, string channel)
        {
            var verificationServiceSid = _configuration["TwilioConfig:VerificationServiceSid"].ToString();

            TwilioClient.Init(accountSid, authToken);

            var verificationResource = await VerificationResource.CreateAsync(pathServiceSid: verificationServiceSid, to: to, channel: channel);

            return verificationResource;
        }

        public async Task<bool?> CheckVerification(string to, string code)
        {
            var verificationServiceSid = _configuration["TwilioConfig:VerificationServiceSid"].ToString();

            TwilioClient.Init(accountSid, authToken);

            var verificationCheck = await VerificationCheckResource.CreateAsync(to: to, code: code, pathServiceSid: verificationServiceSid);

            if (verificationCheck.Status == "approved")
            {
                return true;
            }
            else if (verificationCheck.Status == "canceled")
            {
                return false;
            }
            else
            {
                return null;
            }
        }

    }
}
