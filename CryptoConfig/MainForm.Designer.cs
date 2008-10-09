namespace CryptographyConfig
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
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
         this.toolStrip1 = new System.Windows.Forms.ToolStrip();
         this.newButton = new System.Windows.Forms.ToolStripButton();
         this.openButton = new System.Windows.Forms.ToolStripButton();
         this.saveButton = new System.Windows.Forms.ToolStripButton();
         this.deleteAppButton = new System.Windows.Forms.ToolStripButton();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.algorithmComboBox = new System.Windows.Forms.ToolStripComboBox();
         this.keyTextBox = new System.Windows.Forms.TextBox();
         this.keyWrapperBindingSource = new System.Windows.Forms.BindingSource(this.components);
         this.ivGroupBox = new System.Windows.Forms.GroupBox();
         this.genIVButton = new System.Windows.Forms.Button();
         this.ivTextBox = new System.Windows.Forms.TextBox();
         this.keyGroupBox = new System.Windows.Forms.GroupBox();
         this.genKeyButton = new System.Windows.Forms.Button();
         this.statusStrip1 = new System.Windows.Forms.StatusStrip();
         this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
         this.appNameLabel = new System.Windows.Forms.Label();
         this.toolStrip1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.keyWrapperBindingSource)).BeginInit();
         this.ivGroupBox.SuspendLayout();
         this.keyGroupBox.SuspendLayout();
         this.statusStrip1.SuspendLayout();
         this.SuspendLayout();
         // 
         // toolStrip1
         // 
         this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newButton,
            this.openButton,
            this.saveButton,
            this.deleteAppButton,
            this.toolStripSeparator1,
            this.algorithmComboBox});
         this.toolStrip1.Location = new System.Drawing.Point(0, 0);
         this.toolStrip1.Name = "toolStrip1";
         this.toolStrip1.Size = new System.Drawing.Size(455, 26);
         this.toolStrip1.TabIndex = 0;
         this.toolStrip1.Text = "toolStrip1";
         // 
         // newButton
         // 
         this.newButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.newButton.Image = ((System.Drawing.Image)(resources.GetObject("newButton.Image")));
         this.newButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.newButton.Name = "newButton";
         this.newButton.Size = new System.Drawing.Size(23, 23);
         this.newButton.Text = "New Configuration Application";
         this.newButton.Click += new System.EventHandler(this.OnNewAppButtonClick);
         // 
         // openButton
         // 
         this.openButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.openButton.Image = ((System.Drawing.Image)(resources.GetObject("openButton.Image")));
         this.openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.openButton.Name = "openButton";
         this.openButton.Size = new System.Drawing.Size(23, 23);
         this.openButton.Text = "Open Configuration Application";
         this.openButton.Click += new System.EventHandler(this.OnOpenAppButtonClick);
         // 
         // saveButton
         // 
         this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.saveButton.Image = ((System.Drawing.Image)(resources.GetObject("saveButton.Image")));
         this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.saveButton.Name = "saveButton";
         this.saveButton.Size = new System.Drawing.Size(23, 23);
         this.saveButton.Text = "Save Changes";
         this.saveButton.Click += new System.EventHandler(this.OnSaveAppButtonClick);
         // 
         // deleteAppButton
         // 
         this.deleteAppButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.deleteAppButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteAppButton.Image")));
         this.deleteAppButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.deleteAppButton.Name = "deleteAppButton";
         this.deleteAppButton.Size = new System.Drawing.Size(23, 23);
         this.deleteAppButton.Text = "Delete Configuration Application";
         this.deleteAppButton.Click += new System.EventHandler(this.OnDeleteAppButtonClick);
         // 
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
         // 
         // algorithmComboBox
         // 
         this.algorithmComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.algorithmComboBox.Name = "algorithmComboBox";
         this.algorithmComboBox.Size = new System.Drawing.Size(121, 26);
         this.algorithmComboBox.SelectedIndexChanged += new System.EventHandler(this.OnAlgorithmComboBoxSelectedIndexChanged);
         // 
         // keyTextBox
         // 
         this.keyTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.keyWrapperBindingSource, "KeyString", true));
         this.keyTextBox.Location = new System.Drawing.Point(6, 21);
         this.keyTextBox.Multiline = true;
         this.keyTextBox.Name = "keyTextBox";
         this.keyTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
         this.keyTextBox.Size = new System.Drawing.Size(419, 63);
         this.keyTextBox.TabIndex = 0;
         this.keyTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.OnKeyTextBoxValidating);
         // 
         // keyWrapperBindingSource
         // 
         this.keyWrapperBindingSource.DataSource = typeof(CryptographyConfig.KeyWrapper);
         // 
         // ivGroupBox
         // 
         this.ivGroupBox.Controls.Add(this.genIVButton);
         this.ivGroupBox.Controls.Add(this.ivTextBox);
         this.ivGroupBox.Location = new System.Drawing.Point(12, 196);
         this.ivGroupBox.Name = "ivGroupBox";
         this.ivGroupBox.Size = new System.Drawing.Size(431, 126);
         this.ivGroupBox.TabIndex = 2;
         this.ivGroupBox.TabStop = false;
         this.ivGroupBox.Text = "Initialization Vector";
         // 
         // genIVButton
         // 
         this.genIVButton.Image = ((System.Drawing.Image)(resources.GetObject("genIVButton.Image")));
         this.genIVButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.genIVButton.Location = new System.Drawing.Point(335, 90);
         this.genIVButton.Name = "genIVButton";
         this.genIVButton.Size = new System.Drawing.Size(90, 30);
         this.genIVButton.TabIndex = 1;
         this.genIVButton.Text = "Generate";
         this.genIVButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.genIVButton.UseVisualStyleBackColor = true;
         this.genIVButton.Click += new System.EventHandler(this.OnGenerateIVButtonClick);
         // 
         // ivTextBox
         // 
         this.ivTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.keyWrapperBindingSource, "IVString", true));
         this.ivTextBox.Location = new System.Drawing.Point(6, 21);
         this.ivTextBox.Multiline = true;
         this.ivTextBox.Name = "ivTextBox";
         this.ivTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
         this.ivTextBox.Size = new System.Drawing.Size(419, 63);
         this.ivTextBox.TabIndex = 0;
         this.ivTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.OnIVTextBoxValidating);
         // 
         // keyGroupBox
         // 
         this.keyGroupBox.Controls.Add(this.genKeyButton);
         this.keyGroupBox.Controls.Add(this.keyTextBox);
         this.keyGroupBox.Location = new System.Drawing.Point(12, 64);
         this.keyGroupBox.Name = "keyGroupBox";
         this.keyGroupBox.Size = new System.Drawing.Size(431, 126);
         this.keyGroupBox.TabIndex = 1;
         this.keyGroupBox.TabStop = false;
         this.keyGroupBox.Text = "Key";
         // 
         // genKeyButton
         // 
         this.genKeyButton.Image = ((System.Drawing.Image)(resources.GetObject("genKeyButton.Image")));
         this.genKeyButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.genKeyButton.Location = new System.Drawing.Point(335, 90);
         this.genKeyButton.Name = "genKeyButton";
         this.genKeyButton.Size = new System.Drawing.Size(90, 30);
         this.genKeyButton.TabIndex = 1;
         this.genKeyButton.Text = "Generate";
         this.genKeyButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         this.genKeyButton.UseVisualStyleBackColor = true;
         this.genKeyButton.Click += new System.EventHandler(this.OnGenerateKeyButtonClick);
         // 
         // statusStrip1
         // 
         this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
         this.statusStrip1.Location = new System.Drawing.Point(0, 336);
         this.statusStrip1.Name = "statusStrip1";
         this.statusStrip1.Size = new System.Drawing.Size(455, 22);
         this.statusStrip1.TabIndex = 7;
         this.statusStrip1.Text = "statusStrip1";
         // 
         // statusLabel
         // 
         this.statusLabel.Name = "statusLabel";
         this.statusLabel.Size = new System.Drawing.Size(0, 17);
         // 
         // appNameLabel
         // 
         this.appNameLabel.BackColor = System.Drawing.SystemColors.Info;
         this.appNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
         this.appNameLabel.Location = new System.Drawing.Point(0, 26);
         this.appNameLabel.Name = "appNameLabel";
         this.appNameLabel.Size = new System.Drawing.Size(455, 23);
         this.appNameLabel.TabIndex = 8;
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(455, 358);
         this.Controls.Add(this.appNameLabel);
         this.Controls.Add(this.statusStrip1);
         this.Controls.Add(this.keyGroupBox);
         this.Controls.Add(this.ivGroupBox);
         this.Controls.Add(this.toolStrip1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Name = "MainForm";
         this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
         this.Text = "Cryptography Component Configuration";
         this.toolStrip1.ResumeLayout(false);
         this.toolStrip1.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.keyWrapperBindingSource)).EndInit();
         this.ivGroupBox.ResumeLayout(false);
         this.ivGroupBox.PerformLayout();
         this.keyGroupBox.ResumeLayout(false);
         this.keyGroupBox.PerformLayout();
         this.statusStrip1.ResumeLayout(false);
         this.statusStrip1.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.ToolStrip toolStrip1;
      private System.Windows.Forms.ToolStripButton openButton;
      private System.Windows.Forms.ToolStripButton newButton;
      private System.Windows.Forms.ToolStripButton saveButton;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.TextBox keyTextBox;
      private System.Windows.Forms.GroupBox ivGroupBox;
      private System.Windows.Forms.GroupBox keyGroupBox;
      private System.Windows.Forms.TextBox ivTextBox;
      private System.Windows.Forms.ToolStripComboBox algorithmComboBox;
      private System.Windows.Forms.Button genKeyButton;
      private System.Windows.Forms.Button genIVButton;
      private System.Windows.Forms.BindingSource keyWrapperBindingSource;
      private System.Windows.Forms.StatusStrip statusStrip1;
      private System.Windows.Forms.ToolStripStatusLabel statusLabel;
      private System.Windows.Forms.Label appNameLabel;
      private System.Windows.Forms.ToolStripButton deleteAppButton;
   }
}

