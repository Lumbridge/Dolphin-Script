namespace RSSP
{
    partial class KeyPressForm
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
            this.SpecialKeysComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Button_AddKeypressEvent = new System.Windows.Forms.Button();
            this.KeyPressEventTextBox = new System.Windows.Forms.RichTextBox();
            this.Button_AddSpecialButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SpecialKeysComboBox
            // 
            this.SpecialKeysComboBox.FormattingEnabled = true;
            this.SpecialKeysComboBox.Location = new System.Drawing.Point(12, 127);
            this.SpecialKeysComboBox.Name = "SpecialKeysComboBox";
            this.SpecialKeysComboBox.Size = new System.Drawing.Size(282, 21);
            this.SpecialKeysComboBox.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Insert what you want the event to type below:";
            // 
            // Button_AddKeypressEvent
            // 
            this.Button_AddKeypressEvent.Location = new System.Drawing.Point(12, 190);
            this.Button_AddKeypressEvent.Name = "Button_AddKeypressEvent";
            this.Button_AddKeypressEvent.Size = new System.Drawing.Size(282, 48);
            this.Button_AddKeypressEvent.TabIndex = 25;
            this.Button_AddKeypressEvent.Text = "Add Keypess Event To Event List";
            this.Button_AddKeypressEvent.UseVisualStyleBackColor = true;
            this.Button_AddKeypressEvent.Click += new System.EventHandler(this.Button_AddKeypressEvent_Click);
            // 
            // KeyPressEventTextBox
            // 
            this.KeyPressEventTextBox.Location = new System.Drawing.Point(12, 25);
            this.KeyPressEventTextBox.Multiline = false;
            this.KeyPressEventTextBox.Name = "KeyPressEventTextBox";
            this.KeyPressEventTextBox.Size = new System.Drawing.Size(282, 96);
            this.KeyPressEventTextBox.TabIndex = 27;
            this.KeyPressEventTextBox.Text = "";
            // 
            // Button_AddSpecialButton
            // 
            this.Button_AddSpecialButton.Location = new System.Drawing.Point(12, 154);
            this.Button_AddSpecialButton.Name = "Button_AddSpecialButton";
            this.Button_AddSpecialButton.Size = new System.Drawing.Size(282, 30);
            this.Button_AddSpecialButton.TabIndex = 28;
            this.Button_AddSpecialButton.Text = "Add Special Button";
            this.Button_AddSpecialButton.UseVisualStyleBackColor = true;
            this.Button_AddSpecialButton.Click += new System.EventHandler(this.Button_AddSpecialButton_Click);
            // 
            // KeyPressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 252);
            this.Controls.Add(this.Button_AddSpecialButton);
            this.Controls.Add(this.KeyPressEventTextBox);
            this.Controls.Add(this.SpecialKeysComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Button_AddKeypressEvent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "KeyPressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Keyboard Event";
            this.Load += new System.EventHandler(this.KeyPressForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox SpecialKeysComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Button_AddKeypressEvent;
        private System.Windows.Forms.RichTextBox KeyPressEventTextBox;
        private System.Windows.Forms.Button Button_AddSpecialButton;
    }
}