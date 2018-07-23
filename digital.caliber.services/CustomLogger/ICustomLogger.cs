using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace digital.caliber.services.CustomLogger
{
    public interface ICustomLogger
    {
        Task Log(LogLevel logLevel, string category, string title, string message);

        Task Log(LogLevel logLevel, string category, Exception exception, string title = null);
    }
}
