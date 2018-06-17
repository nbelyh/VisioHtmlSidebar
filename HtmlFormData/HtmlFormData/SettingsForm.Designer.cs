namespace HtmlFormData
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
            this.textBoxPropertyName = new System.Windows.Forms.TextBox();
            this.labelPropertyName = new System.Windows.Forms.Label();
            this.labelPropertyNamePlainText = new System.Windows.Forms.Label();
            this.textBoxPropertyNamePlainText = new System.Windows.Forms.TextBox();
            this.checkBoxReplaceHtmlReports = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(143, 185);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(231, 185);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // textBoxPropertyName
            // 
            this.textBoxPropertyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPropertyName.Location = new System.Drawing.Point(19, 32);
            this.textBoxPropertyName.Name = "textBoxPropertyName";
            this.textBoxPropertyName.Size = new System.Drawing.Size(287, 20);
            this.textBoxPropertyName.TabIndex = 2;
            // 
            // labelPropertyName
            // 
            this.labelPropertyName.AutoSize = true;
            this.labelPropertyName.Location = new System.Drawing.Point(16, 12);
            this.labelPropertyName.Name = "labelPropertyName";
            this.labelPropertyName.Size = new System.Drawing.Size(184, 13);
            this.labelPropertyName.TabIndex = 3;
            this.labelPropertyName.Text = "Shape data property to edit (rich text):";
            // 
            // labelPropertyNamePlainText
            // 
            this.labelPropertyNamePlainText.AutoSize = true;
            this.labelPropertyNamePlainText.Location = new System.Drawing.Point(16, 65);
            this.labelPropertyNamePlainText.Name = "labelPropertyNamePlainText";
            this.labelPropertyNamePlainText.Size = new System.Drawing.Size(189, 13);
            this.labelPropertyNamePlainText.TabIndex = 5;
            this.labelPropertyNamePlainText.Text = "Shape data property to edit (plain text):";
            // 
            // textBoxPropertyNamePlainText
            // 
            this.textBoxPropertyNamePlainText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPropertyNamePlainText.Location = new System.Drawing.Point(19, 85);
            this.textBoxPropertyNamePlainText.Name = "textBoxPropertyNamePlainText";
            this.textBoxPropertyNamePlainText.Size = new System.Drawing.Size(287, 20);
            this.textBoxPropertyNamePlainText.TabIndex = 4;
            // 
            // checkBoxReplaceHtmlReports
            // 
            this.checkBoxReplaceHtmlReports.AutoSize = true;
            this.checkBoxReplaceHtmlReports.Location = new System.Drawing.Point(19, 136);
            this.checkBoxReplaceHtmlReports.Name = "checkBoxReplaceHtmlReports";
            this.checkBoxReplaceHtmlReports.Size = new System.Drawing.Size(232, 17);
            this.checkBoxReplaceHtmlReports.TabIndex = 6;
            this.checkBoxReplaceHtmlReports.Text = "Replace default HTML report (experimental)";
            this.checkBoxReplaceHtmlReports.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(316, 219);
            this.Controls.Add(this.checkBoxReplaceHtmlReports);
            this.Controls.Add(this.labelPropertyNamePlainText);
            this.Controls.Add(this.textBoxPropertyNamePlainText);
            this.Controls.Add(this.labelPropertyName);
            this.Controls.Add(this.textBoxPropertyName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
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
        private System.Windows.Forms.TextBox textBoxPropertyName;
        private System.Windows.Forms.Label labelPropertyName;
        private System.Windows.Forms.Label labelPropertyNamePlainText;
        private System.Windows.Forms.TextBox textBoxPropertyNamePlainText;
        private System.Windows.Forms.CheckBox checkBoxReplaceHtmlReports;
    }
}