namespace VisioHtmlSidebar
{
    partial class SettingsForm
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelPropertyName = new System.Windows.Forms.Label();
            this.labelPropertyNamePlainText = new System.Windows.Forms.Label();
            this.textBoxPropertyNamePlainText = new System.Windows.Forms.TextBox();
            this.checkBoxReplaceHtmlReports = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableWebView2 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.textBoxPropertyName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(427, 485);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(112, 35);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(559, 485);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 35);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelPropertyName
            // 
            this.labelPropertyName.AutoSize = true;
            this.labelPropertyName.Location = new System.Drawing.Point(24, 18);
            this.labelPropertyName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPropertyName.Name = "labelPropertyName";
            this.labelPropertyName.Size = new System.Drawing.Size(275, 20);
            this.labelPropertyName.TabIndex = 3;
            this.labelPropertyName.Text = "Shape data property to edit (rich text):";
            // 
            // labelPropertyNamePlainText
            // 
            this.labelPropertyNamePlainText.AutoSize = true;
            this.labelPropertyNamePlainText.Location = new System.Drawing.Point(24, 100);
            this.labelPropertyNamePlainText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPropertyNamePlainText.Name = "labelPropertyNamePlainText";
            this.labelPropertyNamePlainText.Size = new System.Drawing.Size(283, 20);
            this.labelPropertyNamePlainText.TabIndex = 5;
            this.labelPropertyNamePlainText.Text = "Shape data property to edit (plain text):";
            // 
            // textBoxPropertyNamePlainText
            // 
            this.textBoxPropertyNamePlainText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPropertyNamePlainText.Location = new System.Drawing.Point(28, 131);
            this.textBoxPropertyNamePlainText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxPropertyNamePlainText.Name = "textBoxPropertyNamePlainText";
            this.textBoxPropertyNamePlainText.Size = new System.Drawing.Size(641, 26);
            this.textBoxPropertyNamePlainText.TabIndex = 4;
            // 
            // checkBoxReplaceHtmlReports
            // 
            this.checkBoxReplaceHtmlReports.AutoSize = true;
            this.checkBoxReplaceHtmlReports.Location = new System.Drawing.Point(28, 354);
            this.checkBoxReplaceHtmlReports.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxReplaceHtmlReports.Name = "checkBoxReplaceHtmlReports";
            this.checkBoxReplaceHtmlReports.Size = new System.Drawing.Size(344, 24);
            this.checkBoxReplaceHtmlReports.TabIndex = 6;
            this.checkBoxReplaceHtmlReports.Text = "Replace default HTML report (experimental)";
            this.checkBoxReplaceHtmlReports.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnableWebView2
            // 
            this.checkBoxEnableWebView2.AutoSize = true;
            this.checkBoxEnableWebView2.Location = new System.Drawing.Point(28, 197);
            this.checkBoxEnableWebView2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxEnableWebView2.Name = "checkBoxEnableWebView2";
            this.checkBoxEnableWebView2.Size = new System.Drawing.Size(302, 24);
            this.checkBoxEnableWebView2.TabIndex = 7;
            this.checkBoxEnableWebView2.Text = "Enable Edge rendering (experimental)";
            this.checkBoxEnableWebView2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(52, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(604, 74);
            this.label1.TabIndex = 8;
            this.label1.Text = "Use new Edge (WebView2) for rendering HTML content for improved performance and s" +
    "tability. Depending on your operating system, you may need to install WebView2 r" +
    "untime to enable this option.";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(52, 300);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(394, 20);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Download the WebView2 runtime to enable WebView2";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // textBoxPropertyName
            // 
            this.textBoxPropertyName.FormattingEnabled = true;
            this.textBoxPropertyName.Items.AddRange(new object[] {
            "Comment",
            "User.VisPublish_SidebarMarkdown",
            "User.VisPublish_TooltipMarkdown",
            "User.VisPublish_PopoverMarkdown",
            "User.VisPublish_ContentMarkdown"});
            this.textBoxPropertyName.Location = new System.Drawing.Point(28, 54);
            this.textBoxPropertyName.Name = "textBoxPropertyName";
            this.textBoxPropertyName.Size = new System.Drawing.Size(511, 28);
            this.textBoxPropertyName.TabIndex = 10;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(687, 537);
            this.Controls.Add(this.textBoxPropertyName);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxEnableWebView2);
            this.Controls.Add(this.checkBoxReplaceHtmlReports);
            this.Controls.Add(this.labelPropertyNamePlainText);
            this.Controls.Add(this.textBoxPropertyNamePlainText);
            this.Controls.Add(this.labelPropertyName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HtmlForm Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelPropertyName;
        private System.Windows.Forms.Label labelPropertyNamePlainText;
        private System.Windows.Forms.TextBox textBoxPropertyNamePlainText;
        private System.Windows.Forms.CheckBox checkBoxReplaceHtmlReports;
        private System.Windows.Forms.CheckBox checkBoxEnableWebView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ComboBox textBoxPropertyName;
    }
}