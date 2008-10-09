using System;
using System.Collections.Generic;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Transactions;

using Microsoft.EnterpriseSingleSignOn.Interop;

namespace CryptographyConfig {
   /// <summary>
   /// Helper class used to access the ENTSSO
   /// configuration store
   /// </summary>
   class SsoConfiguration {
      const string CRYPTO_KEY_FIELD = CommonSsoDefs.CRYPTO_KEY_FIELD;
      const string CRYPTO_IV_FIELD = CommonSsoDefs.CRYPTO_IV_FIELD;
      const string CONFIG_NAME = CommonSsoDefs.CONFIG_NAME;

      /// <summary>
      /// Creates a new Configuration Application
      /// </summary>
      /// <param name="name">Name of the Configuration Application</param>
      public void CreateApplication(string name) {
         int flags = SSOFlag.SSO_FLAG_APP_CONFIG_STORE
            | SSOFlag.SSO_FLAG_APP_ALLOW_LOCAL;
         string adminGroup = GetBizTalkAdminGroup();
         string userGroup = GetHostUserGroups();
         string description = "CryptographyComponent Configuration Application";
         string contact = "someone@microsoft.com";

         using ( TransactionScope scope = new TransactionScope() ) {
            ISSOAdmin admin = new ISSOAdmin();
            Enlist(admin, Transaction.Current);
            admin.CreateApplication(name, description, contact, userGroup, adminGroup, flags, 3);
            CreateApplicationFields(name, admin);
            EnableApplication(name, admin);

            scope.Complete();
         }
      }

      /// <summary>
      /// Saves a Key and IV to the SSO store
      /// </summary>
      /// <param name="appName">name of the application</param>
      /// <param name="key">Key to save</param>
      /// <param name="iv">IV to save</param>
      public void SaveApplicationData(string appName, string key, string iv) {
         PropertyBagImpl propBag = new PropertyBagImpl();
         propBag.SetValue(CRYPTO_KEY_FIELD, key);
         propBag.SetValue(CRYPTO_IV_FIELD, iv);

         ISSOConfigStore store = new ISSOConfigStore();
         store.SetConfigInfo(appName, CONFIG_NAME, propBag);
      }

      /// <summary>
      /// Loads a Key and IV from the SSO Store
      /// </summary>
      /// <param name="appName">Name of the application</param>
      /// <param name="key">Key loaded</param>
      /// <param name="iv">IV loaded</param>
      public void LoadApplicationData(string appName, out string key, out string iv) {
         PropertyBagImpl propBag = new PropertyBagImpl();
         ISSOConfigStore store = new ISSOConfigStore();
         store.GetConfigInfo(appName, CONFIG_NAME, SSOFlag.SSO_FLAG_RUNTIME, propBag);

         key = propBag.GetValue<string>(CRYPTO_KEY_FIELD);
         iv = propBag.GetValue<string>(CRYPTO_IV_FIELD);

      }

      /// <summary>
      /// Delete the configuration Application
      /// from the store
      /// </summary>
      /// <param name="appName">Application Name</param>
      public void DeleteApplication(string appName) {
         using ( TransactionScope scope = new TransactionScope() ) {
            ISSOAdmin admin = new ISSOAdmin();
            Enlist(admin, Transaction.Current);
            admin.DeleteApplication(appName);
            scope.Complete();
         }
      }

      #region Private Methods
      //
      // Private Methods
      //

      /// <summary>
      /// Enlist in the specified transaction
      /// </summary>
      /// <param name="obj"></param>
      /// <param name="tx"></param>
      private void Enlist(object obj, Transaction tx) {
         IPropertyBag propBag = (IPropertyBag)obj;
         object dtcTx = TransactionInterop.GetDtcTransaction(tx);
         object serverName = Environment.MachineName;
         propBag.Write("CurrentSSOServer", ref serverName);
         propBag.Write("Transaction", ref dtcTx);
      }

      /// <summary>
      /// Enables a configuration application
      /// </summary>
      /// <param name="appName">Name of the application</param>
      /// <param name="admin">SSO Admin object</param>
      private void EnableApplication(string appName, ISSOAdmin admin) {
         int f = SSOFlag.SSO_FLAG_ENABLED;
         admin.UpdateApplication(appName, null, null, null, null, f, f);
      }

      /// <summary>
      /// Creates the field definitions for a new 
      /// configuration application
      /// </summary>
      /// <param name="appName">Name of the application</param>
      /// <param name="admin">SSO Admin object</param>
      private void CreateApplicationFields(string appName, ISSOAdmin admin) {
         int flags = SSOFlag.SSO_FLAG_FIELD_INFO_SYNC;
         // the first field is always used by SSO, so we add
         // a dummy field to work around it.
         admin.CreateFieldInfo(appName, "someone@microsoft.com", flags);
         admin.CreateFieldInfo(appName, CRYPTO_KEY_FIELD, flags);
         admin.CreateFieldInfo(appName, CRYPTO_IV_FIELD, flags);
      }

      /// <summary>
      /// Queries the BizTalk Group for the name of
      /// the BizTalk Administrators Group
      /// </summary>
      /// <returns>The name of the bts admin group</returns>
      private string GetBizTalkAdminGroup() {
         string query = "SELECT * FROM MSBTS_GroupSetting";
         ManagementObjectSearcher searcher =
            new ManagementObjectSearcher("root\\MicrosoftBizTalkServer", query);
         foreach ( ManagementObject groupSettings in searcher.Get() ) {
            return (string)groupSettings.Properties["BizTalkAdministratorGroup"].Value;
         }
         return null;
      }

      /// <summary>
      /// Queries the BizTalk Group for the name
      /// of the User groups for each biztalk host configured
      /// </summary>
      /// <returns>The names of the groups, separated by ';'</returns>
      private string GetHostUserGroups() {
         List<string> groups = new List<string>();

         string query = "SELECT * FROM MSBTS_HostSetting";
         ManagementObjectSearcher searcher =
            new ManagementObjectSearcher("root\\MicrosoftBizTalkServer", query);
         foreach ( ManagementObject hostSettings in searcher.Get() ) {
            string group = (string)hostSettings.Properties["NTGroupName"].Value;

            if ( !groups.Contains(group) )
               groups.Add(group);
         }

         StringBuilder builder = new StringBuilder();
         foreach ( string g in groups ) {
            builder.Append(g + ";");
         }
         return builder.ToString();
      }

      #endregion // Private Methods


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
   }

} // namespace CryptographyConfig
