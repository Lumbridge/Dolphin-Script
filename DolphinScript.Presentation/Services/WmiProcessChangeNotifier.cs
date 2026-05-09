using DolphinScript.Interfaces;
using DolphinScript.Models;
using System;
using System.Diagnostics;
using System.Management;

namespace DolphinScript.Services
{
    public class WmiProcessChangeNotifier : IProcessChangeNotifier
    {
        private readonly ManagementEventWatcher _processStartWatcher = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStartTrace");
        private readonly ManagementEventWatcher _processStopWatcher = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStopTrace");

        private bool _processStartWatcherStarted;
        private bool _processStopWatcherStarted;
        private bool _handlersAttached;

        public event EventHandler<ProcessChangedEventArgs> ProcessChanged;

        public void Start()
        {
            AttachHandlers();

            try
            {
                _processStartWatcher.Start();
                _processStartWatcherStarted = true;

                _processStopWatcher.Start();
                _processStopWatcherStarted = true;
            }
            catch (ManagementException ex)
            {
                Disable(ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                Disable(ex);
            }
        }

        public void Stop()
        {
            StopWatcher(_processStartWatcher, ref _processStartWatcherStarted);
            StopWatcher(_processStopWatcher, ref _processStopWatcherStarted);
        }

        public void Dispose()
        {
            Stop();
            DetachHandlers();
            _processStartWatcher.Dispose();
            _processStopWatcher.Dispose();
        }

        private void AttachHandlers()
        {
            if (_handlersAttached)
            {
                return;
            }

            _processStartWatcher.EventArrived += ProcessEventArrived;
            _processStopWatcher.EventArrived += ProcessEventArrived;
            _handlersAttached = true;
        }

        private void DetachHandlers()
        {
            if (!_handlersAttached)
            {
                return;
            }

            _processStartWatcher.EventArrived -= ProcessEventArrived;
            _processStopWatcher.EventArrived -= ProcessEventArrived;
            _handlersAttached = false;
        }

        private void Disable(Exception ex)
        {
            Debug.WriteLine($"Process watcher startup failed: {ex.Message}");
            Stop();
            DetachHandlers();
        }

        private static void StopWatcher(ManagementEventWatcher watcher, ref bool started)
        {
            if (!started)
            {
                return;
            }

            try
            {
                watcher.Stop();
            }
            catch (ManagementException ex)
            {
                Debug.WriteLine($"Process watcher stop failed: {ex.Message}");
            }
            finally
            {
                started = false;
            }
        }

        private void ProcessEventArrived(object sender, EventArrivedEventArgs e)
        {
            var processName = e.NewEvent.Properties["ProcessName"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(processName))
            {
                return;
            }

            ProcessChanged?.Invoke(this, new ProcessChangedEventArgs(processName));
        }
    }
}