namespace DolphinScript.Forms.UtilityForms
{
    partial class AreaSelectionForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AreaSelectionForm));
            this.PictureBox_AreaSelection = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_CloseOverlay = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_AreaSelection)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PictureBox_AreaSelection
            // 
            this.PictureBox_AreaSelection.InitialImage = null;
            this.PictureBox_AreaSelection.Location = new System.Drawing.Point(12, 12);
            this.PictureBox_AreaSelection.Name = "PictureBox_AreaSelection";
            this.PictureBox_AreaSelection.Size = new System.Drawing.Size(760, 537);
            this.PictureBox_AreaSelection.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox_AreaSelection.TabIndex = 0;
            this.PictureBox_AreaSelection.TabStop = false;
            this.PictureBox_AreaSelection.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox_AreaSelection_Paint);
            this.PictureBox_AreaSelection.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_AreaSelection_MouseDown);
            this.PictureBox_AreaSelection.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_AreaSelection_MouseMove);
            this.PictureBox_AreaSelection.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox_AreaSelection_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_CloseOverlay});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(145, 26);
            // 
            // toolStripMenuItem_CloseOverlay
            // 
            this.toolStripMenuItem_CloseOverlay.Name = "toolStripMenuItem_CloseOverlay";
            this.toolStripMenuItem_CloseOverlay.Size = new System.Drawing.Size(144, 22);
            this.toolStripMenuItem_CloseOverlay.Text = "Close overlay";
            this.toolStripMenuItem_CloseOverlay.Click += new System.EventHandler(this.toolStripMenuItem_CloseOverlay_Click);
            // 
            // AreaSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.PictureBox_AreaSelection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AreaSelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AreaSelectionForm";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_AreaSelection)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBox_AreaSelection;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_CloseOverlay;
    }
}