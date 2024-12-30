namespace DonemProje
{
    partial class FriendsListMemberUserControl
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
            usernameFriendLabel = new Label();
            SuspendLayout();
            // 
            // usernameFriendLabel
            // 
            usernameFriendLabel.AutoSize = true;
            usernameFriendLabel.Font = new Font("Swis721 Blk BT", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            usernameFriendLabel.Location = new Point(18, 36);
            usernameFriendLabel.Name = "usernameFriendLabel";
            usernameFriendLabel.Size = new Size(123, 24);
            usernameFriendLabel.TabIndex = 0;
            usernameFriendLabel.Text = "UserName";
            // 
            // FriendsListMemberUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Snow;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(usernameFriendLabel);
            Name = "FriendsListMemberUserControl";
            Padding = new Padding(0, 7, 0, 7);
            Size = new Size(333, 98);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public Label usernameFriendLabel;
    }
}
