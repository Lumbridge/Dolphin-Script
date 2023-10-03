using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Extensions;
using DolphinScript.Forms;
using DolphinScript.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;

namespace DolphinScript.Concrete
{
    public class FormManager : IFormManager
    {
        private static MainForm Form => (MainForm)Application.OpenForms["MainForm"];

        private delegate void UpdateFormControlDelegate<T>(
            Control control,
            Expression<Func<T>> property,
            T value);

        /// <summary>
        /// sets a given property of a control in a thread-safe manner
        /// </summary>
        /// <typeparam name="T">type of control to update</typeparam>
        /// <param name="control">the control to update the property for</param>
        /// <param name="property">expression of property to update</param>
        /// <param name="value">the value to assign to the selected property</param>
        /// <exception cref="ArgumentException">invalid property for chosen control</exception>
        public void UpdateFormControl<T>(Control control, Expression<Func<T>> property, T value)
        {
            var propertyInfo = (property.Body as MemberExpression)?.Member as PropertyInfo;

            if (propertyInfo == null || !control.GetType().IsSubclassOf(propertyInfo.ReflectedType) || control.GetType().GetProperty(propertyInfo.Name, propertyInfo.PropertyType) == null)
            {
                throw new ArgumentException("The lambda expression 'property' must reference a valid property on this Control.");
            }

            if (control.InvokeRequired)
            {
                control.Invoke(new UpdateFormControlDelegate<T>(UpdateFormControl), control, property, value);
            }
            else
            {
                control.GetType().InvokeMember(propertyInfo.Name, BindingFlags.SetProperty, null, control, new object[] { value });
            }
        }

        /// <summary>
        /// updates the selected index in the events listbox and provides a way of breaking the main
        /// loop if the script should no longer be running
        /// </summary>
        /// <param name="ev"></param>
        /// <returns></returns>
        public bool UpdateListboxCurrentEventIndex(ScriptEvent ev)
        {
            if (!ScriptState.IsRunning)
            {
                Form.MainDataGrid.Invoke(new Action(() =>
                {
                    foreach (DataGridViewRow row in Form.MainDataGrid.Rows)
                    {
                        row.Selected = false;
                    }
                }));

                return true;
            }

            Form.MainDataGrid.Invoke(new Action(() =>
            {
                foreach (DataGridViewRow row in Form.MainDataGrid.Rows)
                {
                    if (ScriptState.AllEvents.IndexOf(ev) == row.Index)
                    {
                        Form.MainDataGrid.Rows[row.Index].Cells["CurrentAction"].Value = ScriptState.CurrentAction;
                    }
                    else
                    {
                        row.Cells["CurrentAction"].Value = string.Empty;
                    }
                    row.Selected = ScriptState.AllEvents.IndexOf(ev) == row.Index;
                }
            }));

            return false;
        }

        /// <summary>
        /// toggles select controls on the form between enabled and disabled
        /// </summary>
        /// <param name="controls">list of controls to toggle the state of</param>
        /// <param name="state">the enabled state to make the controls</param>
        public void SetControlsEnabled(List<Control> controls, bool state)
        {
            foreach (var control in controls)
            {
                UpdateFormControl(control, () => control.Enabled, state);
            }
        }

        /// <summary>
        /// clears the contents of the main form listbox and updates it with the items in the event list
        /// </summary>
        public void UpdateListBox(DataGridView control)
        {
            var selected = control.GetSelectedIndices();

            // clear the listbox
            control.Invoke(new Action(() =>
            {
                control.Rows.Clear();
            }));

            // add all events in the event list to the listbox
            foreach (var scriptEvent in ScriptState.AllEvents)
            {
                Form.MainDataGrid.Invoke(new Action(() =>
                {
                    int rowId = Form.MainDataGrid.Rows.Add();

                    DataGridViewRow row = Form.MainDataGrid.Rows[rowId];

                    row.Cells["Index"].Value = ScriptState.AllEvents.IndexOf(scriptEvent);
                    row.Cells["ScriptEvent"].Value = scriptEvent.GetType().Name;
                    row.Cells["Description"].Value = scriptEvent.EventDescription();
                    row.Cells["GroupId"].Value = scriptEvent.GroupId;
                    row.Cells["RepeatCount"].Value = scriptEvent.NumberOfCycles;
                    row.Cells["WindowTitle"].Value = scriptEvent.EventProcess.WindowTitle;
                    row.Cells["WindowHandle"].Value = scriptEvent.EventProcess.WindowHandle == IntPtr.Zero ? string.Empty : scriptEvent.EventProcess.WindowHandle.ToString();
                    row.Cells["ProcessName"].Value = scriptEvent.EventProcess.ProcessName;
                }));
            }

            control.SetSelectedIndices(selected);
        }
    }
}