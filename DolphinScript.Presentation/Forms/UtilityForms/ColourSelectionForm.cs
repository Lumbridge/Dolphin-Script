using AutoMapper;
using DolphinScript.Core.Classes;
using DolphinScript.Core.Interfaces;
using DolphinScript.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DolphinScript.Forms.UtilityForms
{
    public partial class ColourSelectionForm : Form
    {
        private readonly IColourService _colourService;
        private readonly IMapper _mapper;
        private readonly IObjectFactory _objectFactory;

        public NextFormModel NextFormModel { get; set; }

        public ColourSelectionForm(IColourService colourService, IMapper mapper, IObjectFactory objectFactory)
        {
            _colourService = colourService;
            _mapper = mapper;
            _objectFactory = objectFactory;
            InitializeComponent();
            SetupFormDefaults();
            PopulateDataGrid();
        }

        private void PopulateDataGrid()
        {
            var colours = _colourService.GetPixelColours(ScriptState.LastSavedArea).Distinct();

            foreach (var colour in colours)
            {
                var hexCode = ColorTranslator.ToHtml(Color.FromArgb(colour.ToArgb()));

                var row = new DataGridViewRow();

                var hexCodeCell = new DataGridViewTextBoxCell
                {
                    Value = hexCode
                };

                var colourPreviewCell = new DataGridViewTextBoxCell
                {
                    Value = string.Empty,
                    Style = new DataGridViewCellStyle
                    {
                        BackColor = colour,
                        ForeColor = colour
                    }
                };

                var checkboxCell = new DataGridViewCheckBoxCell
                {
                    Value = false
                };

                row.Cells.Add(hexCodeCell);
                row.Cells.Add(colourPreviewCell);
                row.Cells.Add(checkboxCell);

                colourSelectionDataGrid.Rows.Add(row);
            }
        }

        private void SetupFormDefaults()
        {
            colourSelectionDataGrid.Columns[0].ReadOnly = true;
            colourSelectionDataGrid.Columns[1].ReadOnly = true;

            var checkboxColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = "Select",
                TrueValue = true,
                FalseValue = false,
                ReadOnly = false
            };
            colourSelectionDataGrid.Columns.Add(checkboxColumn);

            colourSelectionDataGrid.Enabled = true;
            colourSelectionDataGrid.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            colourSelectionDataGrid.ReadOnly = false;
        }

        private List<DataGridViewRow> GetSelectedRows()
        {
            var selectedRows = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in colourSelectionDataGrid.Rows)
            {
                var checkboxCell = row.Cells[2] as DataGridViewCheckBoxCell;
                if (checkboxCell.Value != null && (bool) checkboxCell.Value)
                {
                    selectedRows.Add(row);
                }
            }

            return selectedRows;
        }

        private void SaveColourSelectionsButton_Click(object sender, EventArgs e)
        {
            var scriptEvent = _objectFactory.CreateObject(NextFormModel.EventType);
            scriptEvent.ColourSearchArea = ScriptState.LastSavedArea;
            var rows = GetSelectedRows();
            var mappedRows = _mapper.Map<List<DataGridViewRow>, List<ColourSelectionDataGridRow>>(rows);
            foreach (var row in mappedRows)
            {
                scriptEvent.SearchColours.Add(row.ColourArgb);
            }

            if (NextFormModel.UseWindowSelector)
            {
                scriptEvent.EventProcess = ScriptState.LastSelectedProcess;
            }

            ScriptState.AllEvents.Add(scriptEvent);
            Close();
        }

        private void CancelColourSelectionButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
