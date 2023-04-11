using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilioExamples.Application.Features.VoiceFeatures.Enums
{
    public class CompanyIvrActionsEnum
    {
        public const string Call = "Call";

        public const string EnterMainMenu = "EnterMainMenu";
        public const string ConfirmMainMenu = "ConfirmMainMenu";

        public const string EnterDailActionsWebHook = "EnterDailActionsWebHook";
        public const string ConfirmDailActionsWebHook = "ConfirmDailActionsWebHook";

        public const string ControllerName = "CompanyIvr";
        public const string AreaName = "Voice";
    }
}
