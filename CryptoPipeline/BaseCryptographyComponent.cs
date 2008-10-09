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

   /// <summary>
   /// Base class for the Encryption and Decryption
   /// pipeline components
   /// </summary>
   public abstract class BaseCryptographyComponent :
      Microsoft.BizTalk.Component.Interop.IComponent,
      IBaseComponent,
      IPersistPropertyBag,
      IComponentUI {
      private string _ssoConfigApp;
      private Algorithm _algorithm;
      private AlgorithmKey _algorithmKey;
      private Guid _guid;

      #region Public Properties
      //
      // Public Properties
      //

      /// <summary>
      /// Name of the SSO Config App where the Key and IV are stored
      /// </summary>
      [SRDescription("SsoConfigAppDesc")]
      public string SsoConfigApp {
         get { return _ssoConfigApp; }
         set { _ssoConfigApp = value; }
      }

      /// <summary>
      /// Crypto Algorithm to use
      /// </summary>
      [SRDescription("AlgorithmDesc")]
      public Algorithm Algorithm {
         get { return _algorithm; }
         set { _algorithm = value; }
      }

      [Browsable(false)]
      public AlgorithmKey AlgorithmKey {
         get { return _algorithmKey; }
         set { _algorithmKey = value; }
      }

      #endregion // Public Properties

      protected BaseCryptographyComponent(Guid guid) {
         _guid = guid;
      }

      #region IBaseComponent members
      /// <summary>
      /// Name of the component
      /// </summary>
      [Browsable(false)]
      public abstract string Name {
         get;
      }

      /// <summary>
      /// Version of the component
      /// </summary>
      [Browsable(false)]
      public abstract string Version {
         get;
      }

      /// <summary>
      /// Description of the component
      /// </summary>
      [Browsable(false)]
      public abstract string Description {
         get;
      }
      #endregion

      #region IPersistPropertyBag members
      /// <summary>
      /// Gets class ID of component for usage from unmanaged code.
      /// </summary>
      /// <param name="classid">
      /// Class ID of the component
      /// </param>
      public void GetClassID(out System.Guid classid) {
         classid = _guid;
      }

      /// <summary>
      /// not implemented
      /// </summary>
      public void InitNew() {
      }

      /// <summary>
      /// Loads configuration properties for the component
      /// </summary>
      /// <param name="pb">Configuration property bag</param>
      /// <param name="errlog">Error status</param>
      public virtual void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, int errlog) {
         object val = null;
         val = ReadPropertyBag(pb, "SsoConfigApp");
         if ( (val != null) ) {
            SsoConfigApp = ((string)(val));
         }
         val = ReadPropertyBag(pb, "Algorithm");
         if ( (val != null) ) {
            Algorithm = ((Algorithm)(val));
         }
      }

      /// <summary>
      /// Saves the current component configuration into the property bag
      /// </summary>
      /// <param name="pb">Configuration property bag</param>
      /// <param name="fClearDirty">not used</param>
      /// <param name="fSaveAllProperties">not used</param>
      public virtual void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, bool fClearDirty, bool fSaveAllProperties) {
         this.WritePropertyBag(pb, "SsoConfigApp", SsoConfigApp);
         this.WritePropertyBag(pb, "Algorithm", Algorithm);
      }

      #region utility functionality
      /// <summary>
      /// Reads property value from property bag
      /// </summary>
      /// <param name="pb">Property bag</param>
      /// <param name="propName">Name of property</param>
      /// <returns>Value of the property</returns>
      private object ReadPropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName) {
         object val = null;
         try {
            pb.Read(propName, out val, 0);
         } catch ( System.ArgumentException ) {
            return val;
         } catch ( System.Exception e ) {
            throw new System.ApplicationException(e.Message);
         }
         return val;
      }

      /// <summary>
      /// Writes property values into a property bag.
      /// </summary>
      /// <param name="pb">Property bag.</param>
      /// <param name="propName">Name of property.</param>
      /// <param name="val">Value of property.</param>
      private void WritePropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName, object val) {
         try {
            pb.Write(propName, ref val);
         } catch ( System.Exception e ) {
            throw new System.ApplicationException(e.Message);
         }
      }
      #endregion
      #endregion

      #region IComponentUI members
      /// <summary>
      /// Component icon to use in BizTalk Editor
      /// </summary>
      [Browsable(false)]
      public abstract IntPtr Icon {
         get;
      }

      /// <summary>
      /// The Validate method is called by the BizTalk Editor during the build 
      /// of a BizTalk project.
      /// </summary>
      /// <param name="obj">An Object containing the configuration properties.</param>
      /// <returns>The IEnumerator enables the caller to enumerate through a collection of strings containing error messages. These error messages appear as compiler error messages. To report successful property validation, the method should return an empty enumerator.</returns>
      public System.Collections.IEnumerator Validate(object obj) {
         // example implementation:
         // ArrayList errorList = new ArrayList();
         // errorList.Add("This is a compiler error");
         // return errorList.GetEnumerator();
         return null;
      }
      #endregion

      #region IComponent members
      /// <summary>
      /// Implements IComponent.Execute method.
      /// </summary>
      /// <param name="pc">Pipeline context</param>
      /// <param name="inmsg">Input message</param>
      /// <returns>Original input message</returns>
      /// <remarks>
      /// IComponent.Execute method is used to initiate
      /// the processing of the message in this pipeline component.
      /// </remarks>
      public IBaseMessage Execute(IPipelineContext pc, IBaseMessage inmsg) {
         AlgorithmKey key = AlgorithmKey ?? GetAlgorithmKey();
         Stream cryptoStream = CreateCryptoStream(inmsg.BodyPart.Data, key);
         inmsg.BodyPart.Data = cryptoStream;
         return inmsg;
      }

      #endregion


      #region Overridable Methods
      //
      // Overridable Methods
      //

      /// <summary>
      /// Creates the crypto stream used to encrypt/decrypt the message
      /// </summary>
      /// <param name="stream"></param>
      /// <param name="key"></param>
      /// <returns></returns>
      protected abstract Stream CreateCryptoStream(Stream stream, AlgorithmKey key);

      #endregion // Overridable Methods

      #region Utility Methods
      //
      // Utility Methods
      //

      protected AlgorithmKey GetAlgorithmKey() {
         return SsoConfigReader.LoadKeys(SsoConfigApp);
      }

      #endregion // Utility Methods

   } // class BaseCryptographyComponent

}