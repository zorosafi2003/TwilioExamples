using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Http;
using Twilio.TwiML.Voice;

namespace TwilioExamples.Models.Customs
{
   public class CustomGather : Gather
    {
        public CustomGather(Uri action = null, HttpMethod method = null, int? timeout = null
          , string finishOnKey = null, int? numDigits = null)
        {
            this.Action = action;
            this.Method = method == null ? HttpMethod.Get : method;
            this.Timeout = timeout == null ? 4 : timeout;
            this.FinishOnKey = finishOnKey == null ? "#" : finishOnKey;
            this.NumDigits = numDigits;
        }
    }
}
