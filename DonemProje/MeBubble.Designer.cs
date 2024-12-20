namespace DonemProje
{
    partial class MeBubble
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MessageLabel = new Label();
            SuspendLayout();
            // 
            // MessageLabel
            // 
            MessageLabel.AutoSize = true;
            MessageLabel.Location = new Point(3, 19);
            MessageLabel.Name = "MessageLabel";
            MessageLabel.Size = new Size(67, 20);
            MessageLabel.TabIndex = 0;
            MessageLabel.Text = "Message";
            MessageLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // MeBubble
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Aquamarine;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(MessageLabel);
            ForeColor = SystemColors.ActiveCaptionText;
            Name = "MeBubble";
            Size = new Size(326, 58);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public Label MessageLabel;
    }
}
