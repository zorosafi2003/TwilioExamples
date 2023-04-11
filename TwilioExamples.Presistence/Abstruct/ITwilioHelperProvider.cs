using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML;
using TwilioExamples.Models;
using TwilioExamples.Models.Customs;

namespace TwilioExamples.Presistence.Abstruct
{
    public interface ITwilioHelperProvider
    {
        Uri ReturnFunctionUrl(ReturnFunctionUrlModel model);
      void  ReturnAudioFile(CustomGather gather, string message, string audioFileName, string dir);
        void ReturnAudioFile(VoiceResponse response, string message, string audioFileName, string dir);
    }
}
