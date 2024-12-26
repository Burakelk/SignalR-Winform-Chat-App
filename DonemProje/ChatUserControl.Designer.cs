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
            UserNameLabelChatUserControl = new Label();
            ChatInfoPanelChatUserControl = new Panel();
            ChatScreenPanelChatUserControl = new Panel();
            ChatInfoPanelChatUserControl.SuspendLayout();
            SuspendLayout();
            // 
            // UserNameLabelChatUserControl
            // 
            UserNameLabelChatUserControl.AutoSize = true;
            UserNameLabelChatUserControl.Location = new Point(53, 33);
            UserNameLabelChatUserControl.Name = "UserNameLabelChatUserControl";
            UserNameLabelChatUserControl.Size = new Size(90, 20);
            UserNameLabelChatUserControl.TabIndex = 0;
            UserNameLabelChatUserControl.Text = "Kullanıcı adı";
            // 
            // ChatInfoPanelChatUserControl
            // 
            ChatInfoPanelChatUserControl.BackColor = Color.Turquoise;
            ChatInfoPanelChatUserControl.Controls.Add(UserNameLabelChatUserControl);
            ChatInfoPanelChatUserControl.Location = new Point(3, 3);
            ChatInfoPanelChatUserControl.Name = "ChatInfoPanelChatUserControl";
            ChatInfoPanelChatUserControl.Size = new Size(737, 87);
            ChatInfoPanelChatUserControl.TabIndex = 1;
            // 
            // ChatScreenPanelChatUserControl
            // 
            ChatScreenPanelChatUserControl.AutoScroll = true;
            ChatScreenPanelChatUserControl.BackColor = Color.IndianRed;
            ChatScreenPanelChatUserControl.Dock = DockStyle.Bottom;
            ChatScreenPanelChatUserControl.Location = new Point(0, 99);
            ChatScreenPanelChatUserControl.Name = "ChatScreenPanelChatUserControl";
            ChatScreenPanelChatUserControl.Size = new Size(743, 604);
            ChatScreenPanelChatUserControl.TabIndex = 2;
            // 
            // ChatUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ChatScreenPanelChatUserControl);
            Controls.Add(ChatInfoPanelChatUserControl);
            Margin = new Padding(3, 4, 3, 4);
            Name = "ChatUserControl";
            Size = new Size(743, 703);
            Load += ChatUserControl_Load;
            ChatInfoPanelChatUserControl.ResumeLayout(false);
            ChatInfoPanelChatUserControl.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        public Label UserNameLabelChatUserControl;
        private Panel ChatInfoPanelChatUserControl;
        public Panel ChatScreenPanelChatUserControl;
    }
}
