namespace CryptographyConfig
{
   partial class EnterAppNameForm
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
         if ( disposing && (components != null) )
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
         this.label1 = new System.Windows.Forms.Label();
         this.appNameTextBox = new System.Windows.Forms.TextBox();
         this.okButton = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.Location = new System.Drawing.Point(12, 9);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(365, 39);
         this.label1.TabIndex = 0;
         this.label1.Text = "Enter the name of the SSO Configuration Application to create or open:";
         // 
         // appNameTextBox
         // 
         this.appNameTextBox.Location = new System.Drawing.Point(15, 51);
         this.appNameTextBox.Name = "appNameTextBox";
         this.appNameTextBox.Size = new System.Drawing.Size(362, 22);
         this.appNameTextBox.TabIndex = 1;
         // 
         // okButton
         // 
         this.okButton.Location = new System.Drawing.Point(289, 79);
         this.okButton.Name = "okButton";
         this.okButton.Size = new System.Drawing.Size(88, 32);
         this.okButton.TabIndex = 2;
         this.okButton.Text = "OK";
         this.okButton.UseVisualStyleBackColor = true;
         this.okButton.Click += new System.EventHandler(this.OnOKButtonClick);
         // 
         // EnterAppNameForm
         // 
         this.AcceptButton = this.okButton;
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(389, 117);
         this.Controls.Add(this.okButton);
         this.Controls.Add(this.appNameTextBox);
         this.Controls.Add(this.label1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "EnterAppNameForm";
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.Text = "Application Name";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox appNameTextBox;
      private System.Windows.Forms.Button okButton;
   }
}