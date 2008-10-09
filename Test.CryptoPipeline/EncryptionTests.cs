using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

using Microsoft.BizTalk.Message.Interop;
using NUnit.Framework;
using Winterdom.BizTalk.CryptoPipeline;
using Winterdom.BizTalk.PipelineTesting;
using Winterdom.BizTalk.PipelineTesting.Simple;

namespace Test.CryptoPipeline {
   [TestFixture]
   public class EncryptionTests {
      static byte[] KEY;
      static byte[] IV;

      static EncryptionTests() {
         TripleDES des = TripleDES.Create();
         des.GenerateIV();
         des.GenerateKey();
         KEY = des.Key;
         IV = des.IV;
      }

      [Test]
      public void CanEncryptMessage() {
         SymmetricEncryptionComponent crypto = 
            new SymmetricEncryptionComponent();
         crypto.Algorithm = Algorithm.TripleDES;
         crypto.AlgorithmKey = new AlgorithmKey(KEY, IV);
         
         SendPipelineWrapper pipeline = Pipelines.Send()
            .WithEncoder(crypto);

         IBaseMessage output = pipeline.Execute(PlainTextMessage());
         Assert.IsNotNull(output);
         Assert.Greater(DataPartStreamSize(output), 0);
      }

      private long DataPartStreamSize(IBaseMessage msg) {
         using ( Stream stream = msg.BodyPart.Data ) {
            byte[] buffer = new byte[4096];
            int read;
            long totalRead = 0;
            while ( (read = stream.Read(buffer, 0, buffer.Length)) > 0 )
               totalRead += read;
            return totalRead;
         }
      }
      private IBaseMessage PlainTextMessage() {
         StringBuilder text = new StringBuilder();
         for ( int i = 0; i < 100; i++ ) {
            text.Append("abcdefghijklmnopqrstuvwxyz")
                .Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ")
                .Append("0123456789");
         }
         return MessageHelper.CreateFromString(text.ToString());
      }
   }
}
