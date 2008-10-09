using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using Microsoft.EnterpriseSingleSignOn.Interop;

namespace Winterdom.BizTalk.CryptoPipeline {
   /// <summary>
   /// Helper class used to access the ENTSSO
   /// configuration store
   /// </summary>
   static class SsoConfigReader {
      const string CRYPTO_KEY_FIELD = CommonSsoDefs.CRYPTO_KEY_FIELD;
      const string CRYPTO_IV_FIELD = CommonSsoDefs.CRYPTO_IV_FIELD;
      const string CONFIG_NAME = CommonSsoDefs.CONFIG_NAME;


      /// <summary>
      /// Loads a Key and IV from the SSO Store
      /// </summary>
      /// <param name="appName">Name of the application</param>
      /// <returns>The Algorithm keys read from the SSO</returns>
      public static AlgorithmKey LoadKeys(string appName) {
         PropertyBagImpl propBag = new PropertyBagImpl();
         ISSOConfigStore store = new ISSOConfigStore();
         store.GetConfigInfo(appName, CONFIG_NAME, SSOFlag.SSO_FLAG_RUNTIME, propBag);

         byte[] key, iv;
         key = Convert.FromBase64String(propBag.GetValue<string>(CRYPTO_KEY_FIELD));
         iv = Convert.FromBase64String(propBag.GetValue<string>(CRYPTO_IV_FIELD));

         return new AlgorithmKey(key, iv);
      }

      #region IPropertyBag implementation

      class PropertyBagImpl : IPropertyBag {
         private Dictionary<string, object> _dictionary =
            new Dictionary<string, object>();

         public T GetValue<T>(string propName) {
            if ( _dictionary.ContainsKey(propName) )
               return (T)_dictionary[propName];
            else
               return default(T);
         }
         public void SetValue<T>(string propName, T value) {
            _dictionary[propName] = value;
         }


         #region IPropertyBag Members

         void IPropertyBag.Read(string propName, out object ptrVar, int errorLog) {
            ptrVar = null;
            if ( _dictionary.ContainsKey(propName) )
               ptrVar = _dictionary[propName];
         }

         void IPropertyBag.Write(string propName, ref object ptrVar) {
            _dictionary[propName] = ptrVar;
         }

         #endregion
      }
      #endregion // IPropertyBag implementation

   } // class SsoConfigReader

}

