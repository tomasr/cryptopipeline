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

using System.Reflection;

namespace Winterdom.BizTalk.CryptoPipeline {
   using Properties;

   /// <summary>
   /// Decrypts a message as it is received by biztalk
   /// using a symmetric crypto algorithm
   /// </summary>
   [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
   [System.Runtime.InteropServices.Guid(GUID)]
   [ComponentCategory(CategoryTypes.CATID_Decoder)]
   [SRDescription("DecryptionComponentDesc")]
   public class SymmetricDecryptionComponent : BaseCryptographyComponent {
      const string GUID = "8e2b9d02-3c7a-4534-b142-576698b41f5a";

      #region Properties
      //
      // Properties
      //
      public override string Name {
         get { return Resources.DecryptionComponentName; }
      }

      public override string Description {
         get { return Resources.DecryptionComponentDesc; }
      }

      public override IntPtr Icon {
         get { return Resources.KeysIcon.Handle; }
      }

      public override string Version {
         get { return Resources.ComponentVersion; }
      }
      #endregion // Properties

      public SymmetricDecryptionComponent()
         : base(new Guid(GUID)) {
      }

      #region Overrides
      //
      // Overrides
      //

      protected override Stream CreateCryptoStream(Stream stream, AlgorithmKey key) {
         SymmetricAlgorithm algo = AlgorithmProvider.Create(this.Algorithm);

         ICryptoTransform transform = algo.CreateDecryptor(key.Key, key.IV);

         //
         // CryptoStream is not seekable, and BizTalk doesn't like that
         // We'll get a "Stream does not support seeking." error
         // at runtime. So we decrypt into an in-memory buffer
         // and then give BizTalk that instead.
         //
         //return new CryptoStream(stream, transform, CryptoStreamMode.Read);

         Stream cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Read);
         MemoryStream memStream = new MemoryStream();
         byte[] buffer = new byte[1024 * 64];
         int bytes = 0;
         do {
            bytes = cryptoStream.Read(buffer, 0, buffer.Length);
            memStream.Write(buffer, 0, bytes);
         } while ( bytes > 0 );
         memStream.Position = 0;
         return memStream;
      }
      #endregion // Overrides

   } // class EncryptionComponent

}
