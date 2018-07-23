using System;

namespace digital.caliber.services.CustomExceptions
{
    public class ElementNotFoundException: ApplicationException
    {
        public ElementNotFoundException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
