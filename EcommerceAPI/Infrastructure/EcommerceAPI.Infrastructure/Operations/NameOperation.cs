using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EcommerceAPI.Infrastructure.Operations
{
    public static class NameOperation
    {
        public static string CharacterRegulatory(string name)
        //=> name.Replace("\"", "")
        //    .Replace("!", "")
        //    .Replace("'", "")
        //    .Replace("^", "")
        //    .Replace("+", "")
        //    .Replace("%", "")
        //    .Replace("&", "")
        //    .Replace("/", "")
        //    .Replace("(", "")
        //    .Replace(")", "")
        //    .Replace("=", "")
        //    .Replace("?", "")
        //    .Replace("_", "")
        //    .Replace(" ", "-")
        //    .Replace("@", "")
        //    .Replace("€", "")
        //    .Replace("¨", "")
        //    .Replace("~", "")
        //    .Replace(",", "")
        //    .Replace(";", "")
        //    .Replace(":", "")
        //    .Replace(".", "-")
        //    .Replace("Ö", "o")
        //    .Replace("ö", "o")
        //    .Replace("Ü", "u")
        //    .Replace("ü", "u")
        //    .Replace("ı", "i")
        //    .Replace("İ", "i")
        //    .Replace("ğ", "g")
        //    .Replace("Ğ", "g")
        //    .Replace("æ", "")
        //    .Replace("ß", "")
        //    .Replace("â", "a")
        //    .Replace("î", "i")
        //    .Replace("ş", "s")
        //    .Replace("Ş", "s")
        //    .Replace("Ç", "c")
        //    .Replace("ç", "c")
        //    .Replace("<", "")
        //    .Replace(">", "")
        //    .Replace("|", "");
        {
            name = Regex.Replace(name, @"[\!'""^+%&/\(\)=\?_@\€¨~,;\|<>]", "");
            name = Regex.Replace(name, @"[Öö]", "o");
            name = Regex.Replace(name, @"[Üü]", "u");
            name = Regex.Replace(name, @"[İı]", "i");
            name = Regex.Replace(name, @"[ğĞ]", "g");
            name = Regex.Replace(name, @"[çÇ]", "c");
            name = Regex.Replace(name, @"[şŞ]", "s");
            name = Regex.Replace(name, @"[æß]", "");
            name = Regex.Replace(name, @"[âî]", "i");

            // Replace spaces and dots with hyphens
            name = Regex.Replace(name, @"\s|\.+", "-");

            return name;
        }
    }
}
