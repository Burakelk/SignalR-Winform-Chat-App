﻿namespace DonemProje
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            UserNameLabelChatUserControl = new Label();
            ChatInfoPanelChatUserControl = new Panel();
            label1 = new Label();
            guna2ImageButton1 = new Guna.UI2.WinForms.Guna2ImageButton();
            ChatScreenPanelChatUserControl = new Panel();
            ChatInfoPanelChatUserControl.SuspendLayout();
            SuspendLayout();
            // 
            // UserNameLabelChatUserControl
            // 
            UserNameLabelChatUserControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            UserNameLabelChatUserControl.AutoSize = true;
            UserNameLabelChatUserControl.Location = new Point(53, 33);
            UserNameLabelChatUserControl.Name = "UserNameLabelChatUserControl";
            UserNameLabelChatUserControl.Size = new Size(90, 20);
            UserNameLabelChatUserControl.TabIndex = 0;
            UserNameLabelChatUserControl.Text = "Kullanıcı adı";
            // 
            // ChatInfoPanelChatUserControl
            // 
            ChatInfoPanelChatUserControl.BackColor = Color.Gainsboro;
            ChatInfoPanelChatUserControl.Controls.Add(label1);
            ChatInfoPanelChatUserControl.Controls.Add(guna2ImageButton1);
            ChatInfoPanelChatUserControl.Controls.Add(UserNameLabelChatUserControl);
            ChatInfoPanelChatUserControl.Dock = DockStyle.Top;
            ChatInfoPanelChatUserControl.Location = new Point(0, 0);
            ChatInfoPanelChatUserControl.Name = "ChatInfoPanelChatUserControl";
            ChatInfoPanelChatUserControl.Size = new Size(743, 93);
            ChatInfoPanelChatUserControl.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("News706 BT", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.Location = new Point(581, 33);
            label1.Name = "label1";
            label1.Size = new Size(76, 27);
            label1.TabIndex = 2;
            label1.Text = "Block";
            // 
            // guna2ImageButton1
            // 
            guna2ImageButton1.BackColor = Color.RosyBrown;
            guna2ImageButton1.CheckedState.ImageSize = new Size(64, 64);
            guna2ImageButton1.Dock = DockStyle.Right;
            guna2ImageButton1.HoverState.ImageSize = new Size(64, 64);
            guna2ImageButton1.Image = Properties.Resources.block;
            guna2ImageButton1.ImageOffset = new Point(0, 0);
            guna2ImageButton1.ImageRotate = 0F;
            guna2ImageButton1.Location = new Point(663, 0);
            guna2ImageButton1.Name = "guna2ImageButton1";
            guna2ImageButton1.PressedState.ImageSize = new Size(64, 64);
            guna2ImageButton1.ShadowDecoration.CustomizableEdges = customizableEdges1;
            guna2ImageButton1.Size = new Size(80, 93);
            guna2ImageButton1.TabIndex = 1;
            guna2ImageButton1.Click += guna2ImageButton1_Click_1;
            // 
            // ChatScreenPanelChatUserControl
            // 
            ChatScreenPanelChatUserControl.AutoScroll = true;
            ChatScreenPanelChatUserControl.BackColor = Color.WhiteSmoke;
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
            ChatInfoPanelChatUserControl.ResumeLayout(false);
            ChatInfoPanelChatUserControl.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        public Label UserNameLabelChatUserControl;
        private Panel ChatInfoPanelChatUserControl;
        public Panel ChatScreenPanelChatUserControl;
        private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton1;
        private Label label1;
    }
}
