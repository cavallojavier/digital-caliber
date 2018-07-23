using System;
using Microsoft.Extensions.Logging;

namespace digital.caliber.model.Models
{
    public class Log
    {
        public int Id { get; set; }

        public string Category { get; set; }

        public LogLevel LogLevel { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public DateTime SubmittedOn { get; set; }
    }
}
