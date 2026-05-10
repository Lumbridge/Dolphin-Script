using DolphinScript.Core.Classes;
using DolphinScript.Core.Constants;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;
using DolphinScript.Forms.UtilityForms;
using DolphinScript.Interfaces;
using DolphinScript.Models;
using DolphinScript.Views;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace DolphinScript.Services
{
    public class SelectionOverlayService : ISelectionOverlayService
    {
        private readonly IObjectFactory _objectFactory;

        public SelectionOverlayService(IObjectFactory objectFactory)
        {
            _objectFactory = objectFactory;
        }

        public void Show(NextFormModel nextFormModel)
        {
            var window = _objectFactory.CreateObject<LiveSelectionWindow>();
            window.Configure(nextFormModel);
            window.SelectionCommitted += (_, eventArgs) => SaveSelection(nextFormModel, eventArgs);
            window.Show();
            window.Activate();
        }

        private async void SaveSelection(NextFormModel nextFormModel, SelectionCompletedEventArgs eventArgs)
        {
            ScriptState.LastSavedArea = eventArgs.RelativeBounds;

            if (IsColourSelectionEvent(nextFormModel.EventType))
            {
                await OpenColourSelectionFormAsync(nextFormModel);
                return;
            }

            var scriptEvent = CreateScriptEvent(nextFormModel);
            ScriptState.AllEvents.Add(scriptEvent);
        }

        private ScriptEvent CreateScriptEvent(NextFormModel nextFormModel)
        {
            var scriptEvent = _objectFactory.CreateObject(nextFormModel.EventType);

            if (nextFormModel.UseAreaSelection)
            {
                scriptEvent.ClickArea = ScriptState.LastSavedArea;
            }
            else
            {
                scriptEvent.CoordsToMoveTo = new Point(ScriptState.LastSavedArea.Left, ScriptState.LastSavedArea.Top);
            }

            if (nextFormModel.UseWindowSelector)
            {
                scriptEvent.EventProcess = ScriptState.LastSelectedProcess;
            }

            return scriptEvent;
        }

        private async Task OpenColourSelectionFormAsync(NextFormModel nextFormModel)
        {
            await Task.Delay(120);

            var colourSelectionForm = _objectFactory.CreateObject<ColourSelectionForm>();
            colourSelectionForm.NextFormModel = new NextFormModel(nextFormModel.EventType)
            {
                UseAreaSelection = nextFormModel.UseAreaSelection,
                UseWindowSelector = nextFormModel.UseWindowSelector
            };

            colourSelectionForm.Show();
        }

        private static bool IsColourSelectionEvent(ScriptEventConstants.EventType eventType)
        {
            return eventType.ToString("G").IndexOf("Colour", StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}