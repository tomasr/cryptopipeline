using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Winterdom.BizTalk.CryptoPipeline {
   /// <summary>
   /// Cryptographic Algorithm Key to use
   /// </summary>
   public class AlgorithmKey {
      private byte[] _key;
      private byte[] _iv;

      public byte[] Key {
         get { return _key; }
      }

      public byte[] IV {
         get { return _iv; }
      }

      public AlgorithmKey(byte[] key, byte[] iv) {
         _key = key;
         _iv = iv;
      }

   } // class AlgorithmKey
}