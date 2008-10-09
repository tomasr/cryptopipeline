using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CryptographyConfig {
   public partial class EnterAppNameForm : Form {
      public string ApplicationName {
         get { return appNameTextBox.Text; }
         set { appNameTextBox.Text = value; }
      }
      public EnterAppNameForm() {
         InitializeComponent();
      }

      private void OnOKButtonClick(object sender, EventArgs e) {
         DialogResult = DialogResult.OK;
         Close();
      }
   }
}