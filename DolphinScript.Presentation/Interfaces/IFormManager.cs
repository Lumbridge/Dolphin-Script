using DolphinScript.Core.Events.BaseEvents;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace DolphinScript.Interfaces
{
    public interface IFormManager
    {
        void UpdateFormControl<T>(Control control, Expression<Func<T>> property, T value);
        bool UpdateListboxCurrentEventIndex(ScriptEvent ev);
        void SetControlsEnabled(List<Control> controls, bool state);
        void UpdateListBox(DataGridView control);
    }
}