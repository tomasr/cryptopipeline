using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace CryptographyConfig {
   public partial class MainForm : Form {
      private SymmetricAlgorithm _algorithm;
      private KeyWrapper _key;
      private string _currentApp;

      public MainForm() {
         InitializeComponent();
         object[] algos = { Algorithm.DES, Algorithm.TripleDES, Algorithm.RC2, Algorithm.Rijndael };
         algorithmComboBox.Items.AddRange(algos);
         algorithmComboBox.SelectedIndex = 0;
         _key = new KeyWrapper();

         keyWrapperBindingSource.DataSource = _key;
      }

      private void SetCurrentApplication(string appName) {
         _currentApp = appName;
         appNameLabel.Text = _currentApp;
      }

      private void OnAlgorithmComboBoxSelectedIndexChanged(object sender, EventArgs e) {
         Algorithm algo = (Algorithm)algorithmComboBox.SelectedItem;
         _algorithm = AlgorithmProvider.Create(algo);
      }

      private void OnGenerateKeyButtonClick(object sender, EventArgs e) {
         _algorithm.GenerateKey();
         _key.Key = _algorithm.Key;
      }

      private void OnGenerateIVButtonClick(object sender, EventArgs e) {
         _algorithm.GenerateIV();
         _key.IV = _algorithm.IV;
      }

      private void OnIVTextBoxValidating(object sender, CancelEventArgs e) {
         if ( String.IsNullOrEmpty(ivTextBox.Text) )
            return;
         try {
            byte[] iv = _key.StringToArray(ivTextBox.Text);
            if ( iv.Length * 8 != _algorithm.BlockSize )
               throw new FormatException(string.Format("IV should be {0} bits long", _algorithm.BlockSize));
            statusLabel.Text = "";
         } catch ( Exception ex ) {
            statusLabel.Text = "IV is invalid: " + ex.Message;
         }
      }

      private void OnKeyTextBoxValidating(object sender, CancelEventArgs e) {
         if ( String.IsNullOrEmpty(keyTextBox.Text) )
            return;
         try {
            byte[] key = _key.StringToArray(keyTextBox.Text);
            int length = key.Length * 8;
            bool isValid = false;
            foreach ( KeySizes size in _algorithm.LegalKeySizes ) {
               if ( length >= size.MinSize && length <= size.MaxSize )
                  if ( size.SkipSize == 0 || length % size.SkipSize == 0 )
                     isValid = true;
            }
            if ( !isValid )
               throw new FormatException("Key has an invalid size");
            statusLabel.Text = "";
         } catch ( Exception ex ) {
            statusLabel.Text = "Key is invalid: " + ex.Message;
         }

      }

      private void OnNewAppButtonClick(object sender, EventArgs e) {
         using ( EnterAppNameForm dlg = new EnterAppNameForm() ) {
            DialogResult res = dlg.ShowDialog(this);
            if ( res == DialogResult.OK ) {
               SsoConfiguration config = new SsoConfiguration();
               config.CreateApplication(dlg.ApplicationName);
               SetCurrentApplication(dlg.ApplicationName);

               keyTextBox.Text = "";
               ivTextBox.Text = "";

               statusLabel.Text = "Application created successfully";
            }
         }
      }

      private void OnSaveAppButtonClick(object sender, EventArgs e) {
         SsoConfiguration config = new SsoConfiguration();
         string key = Convert.ToBase64String(_key.Key);
         string iv = Convert.ToBase64String(_key.IV);
         config.SaveApplicationData(_currentApp, key, iv);
         statusLabel.Text = "Changes saved successfully";
      }

      private void OnOpenAppButtonClick(object sender, EventArgs e) {
         using ( EnterAppNameForm dlg = new EnterAppNameForm() ) {
            DialogResult res = dlg.ShowDialog(this);
            if ( res == DialogResult.OK ) {
               string key, iv;
               SsoConfiguration config = new SsoConfiguration();
               config.LoadApplicationData(dlg.ApplicationName, out key, out iv);
               SetCurrentApplication(dlg.ApplicationName);

               if ( !String.IsNullOrEmpty(key) )
                  _key.Key = Convert.FromBase64String(key);
               else
                  _key.KeyString = "";
               if ( !String.IsNullOrEmpty(iv) )
                  _key.IV = Convert.FromBase64String(iv);
               else
                  _key.IVString = "";
            }
         }
      }

      private void OnDeleteAppButtonClick(object sender, EventArgs e) {
         SsoConfiguration config = new SsoConfiguration();
         config.DeleteApplication(_currentApp);
         SetCurrentApplication("");

         keyTextBox.Text = "";
         ivTextBox.Text = "";

         statusLabel.Text = "Application deleted successfully";
      }

   }
}