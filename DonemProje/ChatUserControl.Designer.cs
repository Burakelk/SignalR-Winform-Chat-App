namespace DonemProje
{
    partial class ChatUserControl
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            FriendsListPanel = new Panel();
            ChatTextbox = new Guna.UI2.WinForms.Guna2TextBox();
            guna2ImageButton1 = new Guna.UI2.WinForms.Guna2ImageButton();
            ChatPanelChatUserControl = new Panel();
            SuspendLayout();
            // 
            // FriendsListPanel
            // 
            FriendsListPanel.BackColor = SystemColors.ActiveCaption;
            FriendsListPanel.Dock = DockStyle.Left;
            FriendsListPanel.Location = new Point(0, 0);
            FriendsListPanel.Margin = new Padding(3, 4, 3, 4);
            FriendsListPanel.Name = "FriendsListPanel";
            FriendsListPanel.Size = new Size(330, 867);
            FriendsListPanel.TabIndex = 2;
            // 
            // ChatTextbox
            // 
            ChatTextbox.CustomizableEdges = customizableEdges4;
            ChatTextbox.DefaultText = "";
            ChatTextbox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            ChatTextbox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            ChatTextbox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            ChatTextbox.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            ChatTextbox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            ChatTextbox.Font = new Font("Segoe UI", 9F);
            ChatTextbox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            ChatTextbox.Location = new Point(346, 781);
            ChatTextbox.Margin = new Padding(3, 4, 3, 4);
            ChatTextbox.Multiline = true;
            ChatTextbox.Name = "ChatTextbox";
            ChatTextbox.PasswordChar = '\0';
            ChatTextbox.PlaceholderText = "";
            ChatTextbox.SelectedText = "";
            ChatTextbox.ShadowDecoration.CustomizableEdges = customizableEdges5;
            ChatTextbox.Size = new Size(420, 82);
            ChatTextbox.TabIndex = 3;
            // 
            // guna2ImageButton1
            // 
            guna2ImageButton1.CheckedState.ImageSize = new Size(64, 64);
            guna2ImageButton1.HoverState.ImageSize = new Size(64, 64);
            guna2ImageButton1.Image = Properties.Resources.send_1024x931;
            guna2ImageButton1.ImageOffset = new Point(0, 0);
            guna2ImageButton1.ImageRotate = 0F;
            guna2ImageButton1.Location = new Point(798, 791);
            guna2ImageButton1.Name = "guna2ImageButton1";
            guna2ImageButton1.PressedState.ImageSize = new Size(64, 64);
            guna2ImageButton1.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2ImageButton1.Size = new Size(62, 60);
            guna2ImageButton1.TabIndex = 4;
            guna2ImageButton1.Click += guna2ImageButton1_Click;
            // 
            // ChatPanelChatUserControl
            // 
            ChatPanelChatUserControl.AutoScroll = true;
            ChatPanelChatUserControl.Location = new Point(336, 3);
            ChatPanelChatUserControl.Name = "ChatPanelChatUserControl";
            ChatPanelChatUserControl.Size = new Size(669, 771);
            ChatPanelChatUserControl.TabIndex = 5;
            // 
            // ChatUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(FriendsListPanel);
            Controls.Add(ChatPanelChatUserControl);
            Controls.Add(guna2ImageButton1);
            Controls.Add(ChatTextbox);
            Margin = new Padding(3, 4, 3, 4);
            Name = "ChatUserControl";
            Size = new Size(1005, 867);
            Load += ChatUserControl_Load;
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2TextBox ChatTextbox;
        private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton1;
        private Panel ChatPanelChatUserControl;
        public Panel FriendsListPanel;
    }
}
