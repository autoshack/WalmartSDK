using System;
using System.Collections.Generic;
using System.Text;
using Walmart.Sdk.Base.Primitive;

namespace Walmart.Sdk.Base.Http.Exception
{
    class InvalidApiRequestException:BaseException
    {
        public InvalidApiRequestException(string message):base(message)
        {
            
        }

        public InvalidApiRequestException(string message, string response):base(message,response)
        {
            
        }
    }
}
