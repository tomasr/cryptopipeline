using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Winterdom.BizTalk.CryptoPipeline {
   using Properties;

   internal class SRDescriptionAttribute : DescriptionAttribute {
      public SRDescriptionAttribute(string name)
         : base(Resources.ResourceManager.GetString(name)) {
      }
   } // class LocalizedDescriptionAttribute

}