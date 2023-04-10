using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilioExamples.Application.Features.SmsFeatures.Requests
{
  public  class SendScheduledSmsCommandRequest
    {
        public DateTime SentAt { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
    }
}
