using DolphinScript.Models;
using System;
using System.Collections.ObjectModel;

namespace DolphinScript.Interfaces
{
    public interface IAlertService
    {
        ObservableCollection<AlertNotification> Alerts { get; }

        void Show(AlertSeverity severity, string title, string message, TimeSpan? duration = null);

        void Dismiss(AlertNotification alert);
    }
}