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
            LastMessageFriendListMemberLabel = new Label();
            dateTimeLastMessageFriendListMemberLabel = new Label();
            SuspendLayout();
            // 
            // usernameFriendLabel
            // 
            usernameFriendLabel.AutoSize = true;
            usernameFriendLabel.Location = new Point(41, 23);
            usernameFriendLabel.Name = "usernameFriendLabel";
            usernameFriendLabel.Size = new Size(78, 20);
            usernameFriendLabel.TabIndex = 0;
            usernameFriendLabel.Text = "UserName";
            // 
            // LastMessageFriendListMemberLabel
            // 
            LastMessageFriendListMemberLabel.AutoSize = true;
            LastMessageFriendListMemberLabel.Location = new Point(41, 53);
            LastMessageFriendListMemberLabel.Name = "LastMessageFriendListMemberLabel";
            LastMessageFriendListMemberLabel.Size = new Size(97, 20);
            LastMessageFriendListMemberLabel.TabIndex = 1;
            LastMessageFriendListMemberLabel.Text = "Last Message";
            // 
            // dateTimeLastMessageFriendListMemberLabel
            // 
            dateTimeLastMessageFriendListMemberLabel.AutoSize = true;
            dateTimeLastMessageFriendListMemberLabel.Location = new Point(248, 35);
            dateTimeLastMessageFriendListMemberLabel.Name = "dateTimeLastMessageFriendListMemberLabel";
            dateTimeLastMessageFriendListMemberLabel.Size = new Size(79, 20);
            dateTimeLastMessageFriendListMemberLabel.TabIndex = 2;
            dateTimeLastMessageFriendListMemberLabel.Text = "19.12.2024";
            // 
            // FriendsListMemberUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Snow;
            BorderStyle = BorderStyle.Fixed3D;
            Controls.Add(dateTimeLastMessageFriendListMemberLabel);
            Controls.Add(LastMessageFriendListMemberLabel);
            Controls.Add(usernameFriendLabel);
            Name = "FriendsListMemberUserControl";
            Size = new Size(331, 96);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public Label usernameFriendLabel;
        public Label LastMessageFriendListMemberLabel;
        public Label dateTimeLastMessageFriendListMemberLabel;
    }
}
