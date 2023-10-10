namespace DolphinScript.Forms.UtilityForms
{
    partial class ColourSelectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.colourSelectionDataGrid = new System.Windows.Forms.DataGridView();
            this.ColourHex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Preview = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaveColourSelectionsButton = new System.Windows.Forms.Button();
            this.CancelColourSelectionButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.colourSelectionDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // colourSelectionDataGrid
            // 
            this.colourSelectionDataGrid.AllowUserToAddRows = false;
            this.colourSelectionDataGrid.AllowUserToDeleteRows = false;
            this.colourSelectionDataGrid.AllowUserToResizeColumns = false;
            this.colourSelectionDataGrid.AllowUserToResizeRows = false;
            this.colourSelectionDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.colourSelectionDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColourHex,
            this.Preview});
            this.colourSelectionDataGrid.Location = new System.Drawing.Point(12, 12);
            this.colourSelectionDataGrid.Name = "colourSelectionDataGrid";
            this.colourSelectionDataGrid.RowHeadersVisible = false;
            this.colourSelectionDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.colourSelectionDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.colourSelectionDataGrid.Size = new System.Drawing.Size(322, 253);
            this.colourSelectionDataGrid.TabIndex = 0;
            // 
            // ColourHex
            // 
            this.ColourHex.HeaderText = "Hex code";
            this.ColourHex.Name = "ColourHex";
            // 
            // Preview
            // 
            this.Preview.HeaderText = "Preview";
            this.Preview.Name = "Preview";
            // 
            // SaveColourSelectionsButton
            // 
            this.SaveColourSelectionsButton.Location = new System.Drawing.Point(189, 271);
            this.SaveColourSelectionsButton.Name = "SaveColourSelectionsButton";
            this.SaveColourSelectionsButton.Size = new System.Drawing.Size(109, 23);
            this.SaveColourSelectionsButton.TabIndex = 1;
            this.SaveColourSelectionsButton.Text = "Save";
            this.SaveColourSelectionsButton.UseVisualStyleBackColor = true;
            this.SaveColourSelectionsButton.Click += new System.EventHandler(this.SaveColourSelectionsButton_Click);
            // 
            // CancelColourSelectionButton
            // 
            this.CancelColourSelectionButton.Location = new System.Drawing.Point(43, 271);
            this.CancelColourSelectionButton.Name = "CancelColourSelectionButton";
            this.CancelColourSelectionButton.Size = new System.Drawing.Size(109, 23);
            this.CancelColourSelectionButton.TabIndex = 2;
            this.CancelColourSelectionButton.Text = "Cancel";
            this.CancelColourSelectionButton.UseVisualStyleBackColor = true;
            this.CancelColourSelectionButton.Click += new System.EventHandler(this.CancelColourSelectionButton_Click);
            // 
            // ColourSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 304);
            this.Controls.Add(this.CancelColourSelectionButton);
            this.Controls.Add(this.SaveColourSelectionsButton);
            this.Controls.Add(this.colourSelectionDataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ColourSelectionForm";
            this.Text = "Colour selection";
            ((System.ComponentModel.ISupportInitialize)(this.colourSelectionDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button SaveColourSelectionsButton;
        private System.Windows.Forms.Button CancelColourSelectionButton;
        public System.Windows.Forms.DataGridView colourSelectionDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColourHex;
        private System.Windows.Forms.DataGridViewTextBoxColumn Preview;
    }
}