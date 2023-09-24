using DolphinScript.Forms;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Forms;
using DolphinScript.Interfaces;
using System.Reflection;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Classes;

namespace DolphinScript.Concrete
{
    public class FormManager : IFormManager
    {
        private MainForm _form => (MainForm)Application.OpenForms["MainForm"];

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
                _form.ListBox_Events.Invoke(new Action(() =>
                {
                    _form.ListBox_Events.ClearSelected();
                }));
                return true;
            }

            _form.ListBox_Events.Invoke(new Action(() =>
            {
                _form.ListBox_Events.ClearSelected();
            }));

            UpdateFormControl(_form.ListBox_Events, () => _form.ListBox_Events.SelectedIndex, ScriptState.AllEvents.IndexOf(ev));

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
        public void UpdateListBox(ListBox listBox)
        {
            // clear the listbox
            listBox.Invoke(new Action(() =>
            {
                listBox.Items.Clear();
            }));

            // add all events in the event list to the listbox
            foreach (var scriptEvent in ScriptState.AllEvents)
            {
                listBox.Invoke(new Action(() =>
                {
                    listBox.Items.Add(scriptEvent.GetEventListBoxString());
                }));
            }
        }
    }
}