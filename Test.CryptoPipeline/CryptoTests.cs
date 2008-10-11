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
   public class CryptoTests {
      static byte[] KEY;
      static byte[] IV;

      [TestFixtureSetUp]
      public void FixtureSetUp() {
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

      [Test]
      public void CanDecryptMessage() {
         SymmetricDecryptionComponent crypto = 
            new SymmetricDecryptionComponent();
         crypto.Algorithm = Algorithm.TripleDES;
         crypto.AlgorithmKey = new AlgorithmKey(KEY, IV);

         ReceivePipelineWrapper pipeline = Pipelines.Receive()
            .WithDecoder(crypto)
            .WithDisassembler(Disassembler.Xml().AllowUnrecognized(true));
         IBaseMessage input = 
            MessageHelper.CreateFromStream(CreateEncryptedFile());
         MessageCollection output = pipeline.Execute(input);

         Assert.AreEqual(1, output.Count);
         Assert.Greater(DataPartStreamSize(output[0]), 0);
      }

      private Stream CreateEncryptedFile() {
         String filename = Path.GetTempFileName();
         TripleDES des = TripleDES.Create();
         des.IV = IV;
         des.Key = KEY;
         ICryptoTransform trans = des.CreateEncryptor();
         CryptoStream stream = new CryptoStream(
            File.OpenWrite(filename), trans, CryptoStreamMode.Write
            );
         using ( StreamWriter writer = new StreamWriter(stream) ) {
            writer.Write(PlainTextContent());
         }
         return File.OpenRead(filename);
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
         return MessageHelper.CreateFromString(PlainTextContent());
      }
      private String PlainTextContent() {
         StringBuilder text = new StringBuilder("<msg>");
         for ( int i = 0; i < 10000; i++ ) {
            text.Append("<entry>")
                .Append("abcdefghijklmnopqrstuvwxyz")
                .Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ")
                .Append("0123456789")
                .Append("</entry>");
         }
         text.Append("</msg>");
         return text.ToString();
      }
   }
}
