using System;
using System.Collections.Generic;
using System.Text;
using Walmart.Sdk.Base.Primitive;

namespace Walmart.Sdk.Base.Http.Exception
{
    public class InvalidAccessTokenException : BaseException
    {
        public InvalidAccessTokenException(string message):base(message)
        {
        }

        public InvalidAccessTokenException(string message,string response):base(message,response)
        {
            
        }
    }

}
