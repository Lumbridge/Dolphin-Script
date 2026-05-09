using DolphinScript.Core.Classes;
using DolphinScript.Core.Constants;
using DolphinScript.Models;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DolphinScript.Forms.UtilityForms
{
    public partial class OverlayForm
    {
        private void RightClickMenu_Save_Click(object sender, EventArgs e)
        {
            if (SaveClickAreaToScript())
            {
                Close();
            }
        }

        private bool SaveClickAreaToScript()
        {
            var scriptEvent = _objectFactory.CreateObject(NextFormModel.EventType);

            if (NextFormModel.UseAreaSelection)
            {
                scriptEvent.ClickArea = ScriptState.LastSavedArea;
            }
            else
            {
                scriptEvent.CoordsToMoveTo = new Point(ScriptState.LastSavedArea.Left, ScriptState.LastSavedArea.Top);
            }

            if (NextFormModel.UseWindowSelector)
            {
                scriptEvent.EventProcess = ScriptState.LastSelectedProcess;
            }

            var eventType = NextFormModel.EventType.ToString("G").ToLowerInvariant();
            if (eventType.Contains("colour"))
            {
                OpenColourSelectionForm();
                return false;
            }

            ScriptState.AllEvents.Add(scriptEvent);
            return true;
        }

        private void OpenColourSelectionForm()
        {
            _drawingRect = new Rectangle();
            _overlayPictureBox.Invalidate();

            Task.Delay(100).Wait();

            var colourSelectionForm = _objectFactory.CreateObject<ColourSelectionForm>();
            colourSelectionForm.NextFormModel = new NextFormModel(NextFormModel.EventType)
            {
                UseAreaSelection = NextFormModel.UseAreaSelection,
                UseWindowSelector = NextFormModel.UseWindowSelector
            };

            colourSelectionForm.Show();
            Close();
        }

        private void RightClickMenu_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OnCheckedChanged(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            ScriptState.QuickRegistrationEnabled = menuItem.Checked;
            _rightClickMenu.Items[ContextMenuConstants.SaveMenuItemName].Enabled = !menuItem.Checked;
        }
    }
}