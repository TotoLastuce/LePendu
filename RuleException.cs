using System;
using System.Collections.Generic;
using System.Text;

namespace LePendu
{
    public class RuleException : Exception
    {
        public RuleException()
        {
        }
        
    public RuleException(string message)
        : base(message)
        {
        }
        
    public RuleException(string message, Exception inner)
        : base(message, inner)
        {

        }
    }
}
