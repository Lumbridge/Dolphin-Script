using System;

namespace DolphinScript.Models
{
    public class AlertNotification
    {
        public AlertNotification(AlertSeverity severity, string title, string message)
        {
            Id = Guid.NewGuid();
            Severity = severity;
            Title = title;
            Message = message;
        }

        public Guid Id { get; }

        public AlertSeverity Severity { get; }

        public string Title { get; }

        public string Message { get; }
    }
}