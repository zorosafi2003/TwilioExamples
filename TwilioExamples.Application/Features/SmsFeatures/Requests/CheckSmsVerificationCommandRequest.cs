﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilioExamples.Application.Features.SmsFeatures.Requests
{
    public class CheckSmsVerificationCommandRequest
    {
        public string To { get; set; }
        public string Code { get; set; }
    }
}
