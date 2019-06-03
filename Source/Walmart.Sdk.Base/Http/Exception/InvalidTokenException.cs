using System;
using System.Collections.Generic;
using System.Text;

namespace Walmart.Sdk.Base.Http.Exception
{
    public class InvalidAccessTokenException : System.Exception
    {
        public InvalidAccessTokenException(string message):base(message)
        {
        }
    }

}
