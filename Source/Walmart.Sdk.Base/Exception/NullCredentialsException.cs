using System;
using System.Collections.Generic;
using System.Text;
using Walmart.Sdk.Base.Primitive;

namespace Walmart.Sdk.Base.Exception
{
    class NullCredentialsException:BaseException
    {
        public NullCredentialsException(string message):base(message)
        {
            
        }
    }
}
