﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilioExamples.Application.Features.WhatsappFeatures.Requests
{
   public class SendWhatsappMessageCommandRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
    }
}
