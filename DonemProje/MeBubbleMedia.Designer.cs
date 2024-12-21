namespace DonemProje
{
    partial class MeBubbleMedia
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
            pictureBoxMeBubble = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMeBubble).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxMeBubble
            // 
            pictureBoxMeBubble.Dock = DockStyle.Fill;
            pictureBoxMeBubble.Location = new Point(0, 0);
            pictureBoxMeBubble.Name = "pictureBoxMeBubble";
            pictureBoxMeBubble.Size = new Size(150, 150);
            pictureBoxMeBubble.TabIndex = 0;
            pictureBoxMeBubble.TabStop = false;
            // 
            // MeBubbleMedia
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pictureBoxMeBubble);
            Name = "MeBubbleMedia";
            ((System.ComponentModel.ISupportInitialize)pictureBoxMeBubble).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public PictureBox pictureBoxMeBubble;
    }
}
