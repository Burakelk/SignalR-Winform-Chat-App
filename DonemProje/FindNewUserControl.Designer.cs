namespace DonemProje
{
    partial class FindNewUserControl
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            findNewUserLabel = new Label();
            findFriendTextBox = new Guna.UI2.WinForms.Guna2TextBox();
            findFriendsButton = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // findNewUserLabel
            // 
            findNewUserLabel.AutoSize = true;
            findNewUserLabel.Font = new Font("News706 BT", 72F, FontStyle.Bold, GraphicsUnit.Point, 162);
            findNewUserLabel.Location = new Point(-30, 12);
            findNewUserLabel.Name = "findNewUserLabel";
            findNewUserLabel.Size = new Size(822, 143);
            findNewUserLabel.TabIndex = 2;
            findNewUserLabel.Text = "Find Friends";
            // 
            // findFriendTextBox
            // 
            findFriendTextBox.CustomizableEdges = customizableEdges1;
            findFriendTextBox.DefaultText = "";
            findFriendTextBox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            findFriendTextBox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            findFriendTextBox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            findFriendTextBox.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            findFriendTextBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            findFriendTextBox.Font = new Font("Segoe UI", 9F);
            findFriendTextBox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            findFriendTextBox.Location = new Point(157, 209);
            findFriendTextBox.Margin = new Padding(3, 4, 3, 4);
            findFriendTextBox.Name = "findFriendTextBox";
            findFriendTextBox.PasswordChar = '\0';
            findFriendTextBox.PlaceholderText = "";
            findFriendTextBox.SelectedText = "";
            findFriendTextBox.ShadowDecoration.CustomizableEdges = customizableEdges2;
            findFriendTextBox.Size = new Size(387, 78);
            findFriendTextBox.TabIndex = 3;
            // 
            // findFriendsButton
            // 
            findFriendsButton.BackColor = Color.Black;
            findFriendsButton.CustomizableEdges = customizableEdges3;
            findFriendsButton.DisabledState.BorderColor = Color.DarkGray;
            findFriendsButton.DisabledState.CustomBorderColor = Color.DarkGray;
            findFriendsButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            findFriendsButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            findFriendsButton.FillColor = Color.Black;
            findFriendsButton.Font = new Font("News706 BT", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            findFriendsButton.ForeColor = Color.White;
            findFriendsButton.Location = new Point(247, 385);
            findFriendsButton.Name = "findFriendsButton";
            findFriendsButton.ShadowDecoration.CustomizableEdges = customizableEdges4;
            findFriendsButton.Size = new Size(225, 56);
            findFriendsButton.TabIndex = 4;
            findFriendsButton.Text = "Send Request";
            findFriendsButton.Click += findFriendsButton_Click;
            // 
            // FindNewUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(findFriendsButton);
            Controls.Add(findFriendTextBox);
            Controls.Add(findNewUserLabel);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FindNewUserControl";
            Size = new Size(743, 730);
            Load += FindNewUserControl_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label findNewUserLabel;
        private Guna.UI2.WinForms.Guna2TextBox findFriendTextBox;
        private Guna.UI2.WinForms.Guna2Button findFriendsButton;
    }
}
