using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CWSStart.Web.CWSExtensions
{
    public class ConfigHelper
    {
        private const string MemberGroupNameKey = "CWSMemberGroupName";
        private const string MemberTypeNameKey = "CWSMemberTypeName";
        private const string MemberTypeAliasKey = "CWSMemberTypeAlias";
        private const string HomeDocTypeAliasKey = "CWSHomeDocTypeAlias";
        private const string DefaultMemberGroupName = "CWS-Members";
        private const string DefaultMemberTypeName = "[CWS] Member";
        private const string DefaultMemberTypeAlias = "CWS-Member";
        private const string DefaultHomeDocTypeAlias = "CWS-Home";
        public static string GetCWSMemberGroupName()
        {
            return GetValue(MemberGroupNameKey, DefaultMemberGroupName);
        }
        public static string GetCWSMemberTypeName()
        {
            return GetValue(MemberTypeNameKey, DefaultMemberTypeName);
        }
        public static string GetCWSMemberTypeAlias()
        {
            return GetValue(MemberTypeAliasKey, DefaultMemberTypeAlias);
        }

        // Optimized this one, as it's called on every page due to navi rendering
        private static string _homeDocTypeAlias = null;
        public static string GetCWSHomeDocTypeAlias()
        {
            return _homeDocTypeAlias ?? (_homeDocTypeAlias = GetValue(HomeDocTypeAliasKey, DefaultHomeDocTypeAlias));
        }

        private static string GetValue(string key, string defaultValue)
        {
            var value = ConfigurationManager.AppSettings[key];
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }
    }
}