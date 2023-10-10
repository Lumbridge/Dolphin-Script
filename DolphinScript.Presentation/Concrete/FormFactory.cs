using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Forms.PauseForms;
using DolphinScript.Interfaces;
using System;
using System.Windows.Forms;
using DolphinScript.Core.Constants;
using DolphinScript.Core.Events.Mouse;
using DolphinScript.Core.Interfaces;
using DolphinScript.Forms.UtilityForms;

namespace DolphinScript.Concrete
{
    public class FormFactory : IFormFactory
    {
        private readonly IObjectFactory _objectFactory;

        public FormFactory(IObjectFactory objectFactory)
        {
            _objectFactory = objectFactory;
        }

        public Form GetForm<T>(T scriptEvent) where T : ScriptEvent
        {
            var eventType = scriptEvent.EventType;

            IEventForm form = null;

            switch (eventType)
            {
                case ScriptEventConstants.EventType.FixedPause:
                    form = _objectFactory.CreateObject<FixedPauseForm>();
                    break;
                case ScriptEventConstants.EventType.PauseWhileColourDoesntExistInArea:
                    break;
                case ScriptEventConstants.EventType.PauseWhileColourDoesntExistInAreaOnWindow:
                    form = _objectFactory.CreateObject<OverlayForm>();
                    break;
                case ScriptEventConstants.EventType.PauseWhileColourExistsInArea:
                    break;
                case ScriptEventConstants.EventType.PauseWhileColourExistsInAreaOnWindow:
                    form = _objectFactory.CreateObject<OverlayForm>();
                    break;
                case ScriptEventConstants.EventType.PauseWhileWindowNotFound:
                    break;
                case ScriptEventConstants.EventType.RandomPauseInRange:
                    form = _objectFactory.CreateObject<RandomPauseInRangeForm>();
                    break;
                case ScriptEventConstants.EventType.MoveWindowToFront:
                    break;
                case ScriptEventConstants.EventType.MouseClick:
                    break;
                case ScriptEventConstants.EventType.MouseMove:
                    form = _objectFactory.CreateObject<OverlayForm>();
                    break;
                case ScriptEventConstants.EventType.MouseMoveToArea:
                    form = _objectFactory.CreateObject<OverlayForm>();
                    break;
                case ScriptEventConstants.EventType.MouseMoveToAreaOnWindow:
                    form = _objectFactory.CreateObject<OverlayForm>();
                    break;
                case ScriptEventConstants.EventType.MouseMoveToColour:
                    break;
                case ScriptEventConstants.EventType.MouseMoveToColourOnWindow:
                    form = _objectFactory.CreateObject<OverlayForm>();
                    break;
                case ScriptEventConstants.EventType.MouseMoveToPointOnWindow:
                    form = _objectFactory.CreateObject<OverlayForm>();
                    break;
                case ScriptEventConstants.EventType.KeyboardHoldKey:
                    break;
                case ScriptEventConstants.EventType.KeyboardKeyPress:
                    break;
                case ScriptEventConstants.EventType.KeyboardReleaseKey:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            form?.Bind(scriptEvent);

            return (Form)form;
        }

        public Form GetForm(ScriptEventConstants.EventType eventType)
        {
            var obj = _objectFactory.CreateObject(eventType);
            return GetForm(obj);
        }
    }
}