namespace WindowsFormsApp1
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messages = new System.Windows.Forms.ListBox();
            this.seenButton = new System.Windows.Forms.Button();
            this.unseenButton = new System.Windows.Forms.Button();
            this.draftButton = new System.Windows.Forms.Button();
            this.viewName = new System.Windows.Forms.Label();
            this.sendingButton = new System.Windows.Forms.Button();
            this.messageDate = new System.Windows.Forms.Label();
            this.topLabel = new System.Windows.Forms.Label();
            this.loadLabel = new System.Windows.Forms.Label();
            this.messageLoading = new System.Windows.Forms.Label();
            this.attachmentsBox = new System.Windows.Forms.ListBox();
            this.attachmentsLabel = new System.Windows.Forms.Label();
            this.filterBox = new System.Windows.Forms.MaskedTextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.downloadButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.browserView = new System.Windows.Forms.WebBrowser();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendToolStripMenuItem,
            this.loginToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1029, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sendToolStripMenuItem
            // 
            this.sendToolStripMenuItem.Name = "sendToolStripMenuItem";
            this.sendToolStripMenuItem.Size = new System.Drawing.Size(54, 24);
            this.sendToolStripMenuItem.Text = "Send";
            this.sendToolStripMenuItem.Click += new System.EventHandler(this.sendToolStripMenuItem_Click);
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.loginToolStripMenuItem.Text = "Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click_1);
            // 
            // messages
            // 
            this.messages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.messages.FormattingEnabled = true;
            this.messages.HorizontalScrollbar = true;
            this.messages.ItemHeight = 16;
            this.messages.Location = new System.Drawing.Point(8, 127);
            this.messages.Name = "messages";
            this.messages.Size = new System.Drawing.Size(244, 324);
            this.messages.TabIndex = 1;
            // 
            // seenButton
            // 
            this.seenButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.seenButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.seenButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.seenButton.FlatAppearance.BorderSize = 2;
            this.seenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.seenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.seenButton.Location = new System.Drawing.Point(12, 58);
            this.seenButton.Name = "seenButton";
            this.seenButton.Size = new System.Drawing.Size(76, 38);
            this.seenButton.TabIndex = 5;
            this.seenButton.Text = "Seen";
            this.seenButton.UseVisualStyleBackColor = false;
            this.seenButton.Click += new System.EventHandler(this.seenButton_Click);
            // 
            // unseenButton
            // 
            this.unseenButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.unseenButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.unseenButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.unseenButton.FlatAppearance.BorderSize = 2;
            this.unseenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.unseenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.unseenButton.Location = new System.Drawing.Point(94, 58);
            this.unseenButton.Name = "unseenButton";
            this.unseenButton.Size = new System.Drawing.Size(76, 38);
            this.unseenButton.TabIndex = 6;
            this.unseenButton.Text = "Unseen";
            this.unseenButton.UseVisualStyleBackColor = false;
            this.unseenButton.Click += new System.EventHandler(this.unseenButton_Click);
            // 
            // draftButton
            // 
            this.draftButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.draftButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.draftButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.draftButton.FlatAppearance.BorderSize = 2;
            this.draftButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.draftButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.draftButton.Location = new System.Drawing.Point(176, 58);
            this.draftButton.Name = "draftButton";
            this.draftButton.Size = new System.Drawing.Size(76, 38);
            this.draftButton.TabIndex = 8;
            this.draftButton.Text = "Draft";
            this.draftButton.UseVisualStyleBackColor = false;
            this.draftButton.Click += new System.EventHandler(this.draftButton_Click);
            // 
            // viewName
            // 
            this.viewName.AutoSize = true;
            this.viewName.Font = new System.Drawing.Font("Microsoft Uighur", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.viewName.Location = new System.Drawing.Point(606, 37);
            this.viewName.Name = "viewName";
            this.viewName.Size = new System.Drawing.Size(0, 19);
            this.viewName.TabIndex = 9;
            // 
            // sendingButton
            // 
            this.sendingButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.sendingButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sendingButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sendingButton.FlatAppearance.BorderSize = 2;
            this.sendingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sendingButton.Location = new System.Drawing.Point(931, 37);
            this.sendingButton.Name = "sendingButton";
            this.sendingButton.Size = new System.Drawing.Size(86, 38);
            this.sendingButton.TabIndex = 10;
            this.sendingButton.Text = "Sending";
            this.sendingButton.UseVisualStyleBackColor = false;
            this.sendingButton.Click += new System.EventHandler(this.sendingButton_Click);
            // 
            // messageDate
            // 
            this.messageDate.AutoSize = true;
            this.messageDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.messageDate.Location = new System.Drawing.Point(836, 642);
            this.messageDate.Name = "messageDate";
            this.messageDate.Size = new System.Drawing.Size(0, 18);
            this.messageDate.TabIndex = 11;
            // 
            // topLabel
            // 
            this.topLabel.AutoSize = true;
            this.topLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.topLabel.Location = new System.Drawing.Point(282, 97);
            this.topLabel.Name = "topLabel";
            this.topLabel.Size = new System.Drawing.Size(0, 18);
            this.topLabel.TabIndex = 12;
            // 
            // loadLabel
            // 
            this.loadLabel.AutoSize = true;
            this.loadLabel.Location = new System.Drawing.Point(75, 107);
            this.loadLabel.Name = "loadLabel";
            this.loadLabel.Size = new System.Drawing.Size(0, 17);
            this.loadLabel.TabIndex = 13;
            // 
            // messageLoading
            // 
            this.messageLoading.AutoSize = true;
            this.messageLoading.Enabled = false;
            this.messageLoading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.messageLoading.Location = new System.Drawing.Point(282, 642);
            this.messageLoading.Name = "messageLoading";
            this.messageLoading.Size = new System.Drawing.Size(137, 18);
            this.messageLoading.TabIndex = 14;
            this.messageLoading.Text = "Loading message...";
            this.messageLoading.Visible = false;
            // 
            // attachmentsBox
            // 
            this.attachmentsBox.FormattingEnabled = true;
            this.attachmentsBox.ItemHeight = 16;
            this.attachmentsBox.Location = new System.Drawing.Point(8, 524);
            this.attachmentsBox.Name = "attachmentsBox";
            this.attachmentsBox.Size = new System.Drawing.Size(242, 116);
            this.attachmentsBox.TabIndex = 17;
            // 
            // attachmentsLabel
            // 
            this.attachmentsLabel.AutoSize = true;
            this.attachmentsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.attachmentsLabel.Location = new System.Drawing.Point(7, 504);
            this.attachmentsLabel.Name = "attachmentsLabel";
            this.attachmentsLabel.Size = new System.Drawing.Size(97, 17);
            this.attachmentsLabel.TabIndex = 18;
            this.attachmentsLabel.Text = "Attachments";
            // 
            // filterBox
            // 
            this.filterBox.Location = new System.Drawing.Point(820, 96);
            this.filterBox.Name = "filterBox";
            this.filterBox.Size = new System.Drawing.Size(159, 22);
            this.filterBox.TabIndex = 20;
            // 
            // searchButton
            // 
            this.searchButton.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.search1;
            this.searchButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.searchButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.searchButton.Location = new System.Drawing.Point(985, 86);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(32, 35);
            this.searchButton.TabIndex = 21;
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // downloadButton
            // 
            this.downloadButton.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.download;
            this.downloadButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.downloadButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.downloadButton.Location = new System.Drawing.Point(8, 646);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(30, 31);
            this.downloadButton.TabIndex = 19;
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.delete1;
            this.deleteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deleteButton.Enabled = false;
            this.deleteButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.deleteButton.Location = new System.Drawing.Point(46, 457);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(32, 35);
            this.deleteButton.TabIndex = 16;
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.UI_Glyph_03_18_512;
            this.refreshButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.refreshButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.refreshButton.Location = new System.Drawing.Point(8, 457);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(32, 35);
            this.refreshButton.TabIndex = 15;
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // browserView
            // 
            this.browserView.Location = new System.Drawing.Point(283, 127);
            this.browserView.MinimumSize = new System.Drawing.Size(20, 20);
            this.browserView.Name = "browserView";
            this.browserView.Size = new System.Drawing.Size(734, 500);
            this.browserView.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1029, 689);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.filterBox);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.attachmentsLabel);
            this.Controls.Add(this.attachmentsBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.messageLoading);
            this.Controls.Add(this.loadLabel);
            this.Controls.Add(this.topLabel);
            this.Controls.Add(this.messageDate);
            this.Controls.Add(this.sendingButton);
            this.Controls.Add(this.viewName);
            this.Controls.Add(this.draftButton);
            this.Controls.Add(this.browserView);
            this.Controls.Add(this.unseenButton);
            this.Controls.Add(this.seenButton);
            this.Controls.Add(this.messages);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmailClient";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ListBox messages;
        private System.Windows.Forms.Button seenButton;
        private System.Windows.Forms.Button unseenButton;
        private System.Windows.Forms.WebBrowser browserView;
        private System.Windows.Forms.Button draftButton;
        private System.Windows.Forms.Label viewName;
        private System.Windows.Forms.Button sendingButton;
        private System.Windows.Forms.Label messageDate;
        private System.Windows.Forms.Label topLabel;
        private System.Windows.Forms.Label loadLabel;
        private System.Windows.Forms.Label messageLoading;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ListBox attachmentsBox;
        private System.Windows.Forms.Label attachmentsLabel;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.MaskedTextBox filterBox;
        private System.Windows.Forms.Button searchButton;
    }
}

