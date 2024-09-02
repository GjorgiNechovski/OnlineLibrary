using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Domain.Exceptions
{
    public class NowEnoughBooksException : Exception
    {
        public NowEnoughBooksException(string? message) : base(message)
        {
        }
    }
}
