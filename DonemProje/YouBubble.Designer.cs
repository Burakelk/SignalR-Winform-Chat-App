namespace DonemProje
{
    partial class YouBubble
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
            MessageLabel.BackColor = Color.Transparent;
            MessageLabel.ForeColor = Color.White;
            MessageLabel.Location = new Point(14, 18);
            MessageLabel.Name = "MessageLabel";
            MessageLabel.Size = new Size(67, 20);
            MessageLabel.TabIndex = 1;
            MessageLabel.Text = "Message";
            MessageLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // YouBubble
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            Controls.Add(MessageLabel);
            Name = "YouBubble";
            Size = new Size(326, 58);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public Label MessageLabel;
    }
}
