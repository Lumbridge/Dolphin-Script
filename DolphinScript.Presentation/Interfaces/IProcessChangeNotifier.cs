using DolphinScript.Models;
using System;

namespace DolphinScript.Interfaces
{
    public interface IProcessChangeNotifier : IDisposable
    {
        event EventHandler<ProcessChangedEventArgs> ProcessChanged;

        void Start();
        void Stop();
    }
}