using System;

namespace digital.caliber.services.CustomExceptions
{
    public class CalculationCustomException : ApplicationException
    {
        public CalculationCustomException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
