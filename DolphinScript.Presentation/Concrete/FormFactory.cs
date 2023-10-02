using DolphinScript.Core.Classes;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Forms.PauseForms;
using DolphinScript.Interfaces;
using System;
using System.Windows.Forms;
using DolphinScript.Core.Interfaces;

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

            EventForm form = null;

            switch (eventType)
            {
                case Constants.EventType.FixedPause:
                    form = _objectFactory.CreateObject<FixedPauseForm>();
                    break;
                case Constants.EventType.PauseWhileColourDoesntExistInArea:
                    break;
                case Constants.EventType.PauseWhileColourDoesntExistInAreaOnWindow:
                    break;
                case Constants.EventType.PauseWhileColourExistsInArea:
                    break;
                case Constants.EventType.PauseWhileColourExistsInAreaOnWindow:
                    break;
                case Constants.EventType.PauseWhileWindowNotFound:
                    break;
                case Constants.EventType.RandomPauseInRange:
                    form = _objectFactory.CreateObject<RandomPauseInRangeForm>();
                    break;
                case Constants.EventType.MoveWindowToFront:
                    break;
                case Constants.EventType.MouseClick:
                    break;
                case Constants.EventType.MouseMove:
                    break;
                case Constants.EventType.MouseMoveToArea:
                    break;
                case Constants.EventType.MouseMoveToAreaOnWindow:
                    break;
                case Constants.EventType.MouseMoveToColour:
                    break;
                case Constants.EventType.MouseMoveToColourOnWindow:
                    break;
                case Constants.EventType.MouseMoveToMultiColourOnWindow:
                    break;
                case Constants.EventType.MouseMoveToPointOnWindow:
                    break;
                case Constants.EventType.KeyboardHoldKey:
                    break;
                case Constants.EventType.KeyboardKeyPress:
                    break;
                case Constants.EventType.KeyboardReleaseKey:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            form?.Bind(scriptEvent);

            return form;
        }
    }
}