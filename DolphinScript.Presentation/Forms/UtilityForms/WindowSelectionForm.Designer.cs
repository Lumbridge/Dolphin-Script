namespace DolphinScript.Forms.UtilityForms
{
    partial class WindowSelectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowSelectionForm));
            this.windowComboBox = new ImageComboBox.ImageComboBox();
            this.useSelectedWindowButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // windowComboBox
            // 
            this.windowComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.windowComboBox.ImageList = null;
            this.windowComboBox.Indent = 0;
            this.windowComboBox.ItemHeight = 15;
            this.windowComboBox.Location = new System.Drawing.Point(12, 12);
            this.windowComboBox.Name = "windowComboBox";
            this.windowComboBox.Size = new System.Drawing.Size(137, 21);
            this.windowComboBox.TabIndex = 0;
            // 
            // useSelectedWindowButton
            // 
            this.useSelectedWindowButton.Location = new System.Drawing.Point(12, 39);
            this.useSelectedWindowButton.Name = "useSelectedWindowButton";
            this.useSelectedWindowButton.Size = new System.Drawing.Size(137, 23);
            this.useSelectedWindowButton.TabIndex = 1;
            this.useSelectedWindowButton.Text = "Use selected window";
            this.useSelectedWindowButton.UseVisualStyleBackColor = true;
            this.useSelectedWindowButton.Click += new System.EventHandler(this.useSelectedWindowButton_Click);
            // 
            // WindowSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(164, 69);
            this.Controls.Add(this.useSelectedWindowButton);
            this.Controls.Add(this.windowComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WindowSelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Window selection";
            this.Load += new System.EventHandler(this.WindowSelectionForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ImageComboBox.ImageComboBox windowComboBox;
        private System.Windows.Forms.Button useSelectedWindowButton;
    }
}