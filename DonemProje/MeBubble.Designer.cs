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
            label1 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(246, 16);
            label1.Name = "label1";
            label1.Size = new Size(67, 20);
            label1.TabIndex = 0;
            label1.Text = "Message";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // MeBubble
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Aquamarine;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(label1);
            ForeColor = SystemColors.ActiveCaptionText;
            Name = "MeBubble";
            Size = new Size(326, 58);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public Label label1;
    }
}
