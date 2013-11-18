using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web;
using umbraco.cms.businesslogic.language;

namespace CWSStart.Web.CWSExtensions
{
    public class DictionaryHelper
    {
        public static string GetDictItem(string key, string defaultValue)
        {
            var value = umbraco.library.GetDictionaryItem(key);
            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }
            AddDictionaryItemIfNotExist(key,defaultValue);
            return defaultValue;
        }
        private static void AddDictionaryItemIfNotExist(string key, string defaultText)
        {
            if (umbraco.cms.businesslogic.Dictionary.DictionaryItem.hasKey(key))
                return;
            if (!umbraco.cms.businesslogic.Dictionary.DictionaryItem.hasKey("CWS"))
                umbraco.cms.businesslogic.Dictionary.DictionaryItem.addKey("CWS", "");
            int dictionaryID = umbraco.cms.businesslogic.Dictionary.DictionaryItem.addKey(key, defaultText,"CWS");

            var dictionaryItem = new umbraco.cms.businesslogic.Dictionary.DictionaryItem(dictionaryID);
            var test = dictionaryItem.Value();
            foreach (Language l in Language.GetAllAsList())
                dictionaryItem.setValue(l.id,
                                        l.CultureAlias.Substring(0, 2).ToLowerInvariant() == "en"
                                            ? defaultText
                                            : string.Format("[{0}]", key));
            dictionaryItem.Save();
        }
    }
}