using System;
using System.Threading.Tasks;
using digital.caliber.model.Data;
using digital.caliber.model.Models;
using Microsoft.Extensions.Logging;

namespace digital.caliber.services.CustomLogger
{
    public class CustomLogger : ICustomLogger
    {
        private readonly CaliberDbContext _context;

        public CustomLogger(CaliberDbContext context)
        {
            _context = context;
        }

        public async Task Log(LogLevel logLevel, string category, string title, string message)
        {
            var log = new Log()
            {
                Category = category,
                Message = message,
                Title = title,
                SubmittedOn = DateTime.UtcNow
            };

            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task Log(LogLevel logLevel, string category, Exception exception, string title = null)
        {
            var log = new Log()
            {
                Category = category,
                Message = exception.ToString(),
                Title = title ?? exception.Message,
                StackTrace = exception.StackTrace,
                SubmittedOn = DateTime.UtcNow
            };

            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }
}
