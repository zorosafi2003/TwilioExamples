using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilioExamples.Application.Features.SmsFeatures.Requests
{
  public  class SendBulkSmsCommandRequest
    {
        public List<SendToChildOfSendBulkSmsCommandRequest> SendToList { get; set; }
        public class SendToChildOfSendBulkSmsCommandRequest
        {
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
        }
    }
}
