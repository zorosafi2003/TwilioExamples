using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilioExamples.Models
{
  public  class ReturnFunctionUrlModel
    {
        public string Digits { get; set; }
        public dynamic DtoModel { get; set; }
        public string FunctionName { get; set; }
        public string ControllerName { get; set; }
        public string AreaName { get; set; }
        public List<KeyValuePair<string, string>> QueryParams { get; set; }
    }
}
