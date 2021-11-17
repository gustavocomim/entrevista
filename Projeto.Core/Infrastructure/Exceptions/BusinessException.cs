using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Core.Infrastructure.Exceptions
{
    /// <summary>
    /// TODO: comente o que essa classe representa.
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
        }
    }
}
