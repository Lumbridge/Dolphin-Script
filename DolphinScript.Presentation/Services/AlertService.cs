using DolphinScript.Interfaces;
using DolphinScript.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace DolphinScript.Services
{
    public class AlertService : IAlertService
    {
        private static readonly TimeSpan DefaultDuration = TimeSpan.FromSeconds(5);

        private readonly Dispatcher _dispatcher;

        public AlertService()
        {
            _dispatcher = Application.Current.Dispatcher;
            Alerts = new ObservableCollection<AlertNotification>();
        }

        public ObservableCollection<AlertNotification> Alerts { get; }

        public void Show(AlertSeverity severity, string title, string message, TimeSpan? duration = null)
        {
            var alert = new AlertNotification(severity, title, message);
            _dispatcher.Invoke(() => Alerts.Insert(0, alert));
            _ = DismissAfterDelayAsync(alert, duration ?? DefaultDuration);
        }

        public void Dismiss(AlertNotification alert)
        {
            if (alert == null)
            {
                return;
            }

            _dispatcher.Invoke(() =>
            {
                var existingAlert = Alerts.FirstOrDefault(x => x.Id == alert.Id);
                if (existingAlert != null)
                {
                    Alerts.Remove(existingAlert);
                }
            });
        }

        private async Task DismissAfterDelayAsync(AlertNotification alert, TimeSpan duration)
        {
            await Task.Delay(duration);
            Dismiss(alert);
        }
    }
}