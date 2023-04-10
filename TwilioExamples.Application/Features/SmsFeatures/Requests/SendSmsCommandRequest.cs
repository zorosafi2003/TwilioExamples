using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilioExamples.Application.Features.SmsFeatures.Requests
{
  public  class SendSmsCommandRequest
    {
        public string To { get; set; }
        public string Body  { get; set; }
    }
}
