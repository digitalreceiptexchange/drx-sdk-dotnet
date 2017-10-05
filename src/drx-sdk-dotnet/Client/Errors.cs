using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Dreceiptx.Client
{
    public class Errors
    {
        public static Error DRY_RUN_TO_PROD_ENVIRONMENT_ERROR = new Error(412, "You are trying to send a DryRun receipt to a Production receipt API, please ensure you have configured the receipt correctly.");
        public static Error NON_DRY_RUN_TO_DRY_RUN_ENVIRONMENT_ERROR = new Error(412, "You are trying to send a non-DryRun receipt to a DryRun receipt API, please ensure you have configured the receipt correctly.");
            
    }

    public class Error
    {
        public int ErrorCode { get; }
        public string Message { get; }

        public Error(int errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }
    }
}
