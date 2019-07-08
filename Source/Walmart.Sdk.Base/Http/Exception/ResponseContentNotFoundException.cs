using System;
using System.Collections.Generic;
using System.Text;
using Walmart.Sdk.Base.Primitive;

namespace Walmart.Sdk.Base.Http.Exception
{
    public class ResponseContentNotFoundException: BaseException
    {
        public ResponseContentNotFoundException(string message):base(message)
        {
            
        }

        public ResponseContentNotFoundException(string message,string response) : base(message,response)
        {

        }
    }
}
