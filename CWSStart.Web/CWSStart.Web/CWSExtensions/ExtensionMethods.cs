using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using umbraco.MacroEngines;

namespace CWSStart.Web.CWSExtensions
{
    public static class ExtensionMethods
    {
        public static string GetGravatarUrl(this string emailAddress, int size, string fallbackURL)
        {
            //Get email to lower
            var emailToHash = emailAddress.ToLower();

            // Create a new instance of the MD5CryptoServiceProvider object.  
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.  
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(emailToHash));

            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            var hashedEmail = sBuilder.ToString();  // Return the hexadecimal string.

            //Return the gravatar URL
            return string.Format("http://www.gravatar.com/avatar/{0}?s={1}&d={2}", hashedEmail, size, fallbackURL);
        }
    }
}