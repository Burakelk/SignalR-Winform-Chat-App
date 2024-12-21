namespace DonemProje
{
    partial class YouBubbleMedia
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
            pictureBoxYouBubble = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxYouBubble).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxYouBubble
            // 
            pictureBoxYouBubble.Dock = DockStyle.Fill;
            pictureBoxYouBubble.Location = new Point(0, 0);
            pictureBoxYouBubble.Name = "pictureBoxYouBubble";
            pictureBoxYouBubble.Size = new Size(150, 150);
            pictureBoxYouBubble.TabIndex = 0;
            pictureBoxYouBubble.TabStop = false;
            // 
            // YouBubbleMedia
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pictureBoxYouBubble);
            Name = "YouBubbleMedia";
            ((System.ComponentModel.ISupportInitialize)pictureBoxYouBubble).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public PictureBox pictureBoxYouBubble;
    }
}
