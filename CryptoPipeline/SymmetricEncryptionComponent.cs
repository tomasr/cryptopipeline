using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Security.Cryptography;
using System.Text;

using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Component;
using Microsoft.BizTalk.Messaging;

namespace Winterdom.BizTalk.CryptoPipeline {
   using Properties;

   /// <summary>
   /// Encrypts a message as it is sent by biztalk
   /// using a symmetric crypto algorithm
   /// </summary>
   [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
   [System.Runtime.InteropServices.Guid(GUID)]
   [ComponentCategory(CategoryTypes.CATID_Encoder)]
   [SRDescription("EncryptionComponentDesc")]
   public class SymmetricEncryptionComponent : BaseCryptographyComponent {
      const string GUID = "9596349f-c7dd-4f0d-8c0b-0c62a32817ba";

      #region Properties
      //
      // Properties
      //
      public override string Name {
         get { return Resources.EncryptionComponentName; }
      }

      public override string Description {
         get { return Resources.EncryptionComponentDesc; }
      }

      public override IntPtr Icon {
         get { return Resources.KeysIcon.Handle; }
      }

      public override string Version {
         get { return Resources.ComponentVersion; }
      }
      #endregion // Properties

      public SymmetricEncryptionComponent()
         : base(new Guid(GUID)) {
      }

      #region Overrides
      //
      // Overrides
      //

      protected override Stream CreateCryptoStream(Stream stream, AlgorithmKey key) {
         SymmetricAlgorithm algo = AlgorithmProvider.Create(this.Algorithm);

         ICryptoTransform transform = algo.CreateEncryptor(key.Key, key.IV);

         return new CryptoStream(stream, transform, CryptoStreamMode.Read);
      }
      #endregion // Overrides
   } // class EncryptionComponent
}