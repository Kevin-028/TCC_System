using System;
using System.Globalization;
using System.Linq;

namespace TCC_System_Domain.Core
{
    public class TextHelper
    {
        public static string RemoveAcents(string text)
        {
            if (text == null) return string.Empty;

            const string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            const string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (var i = 0; i < comAcentos.Length; i++)
                text = text.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());

            return text;
        }

        public static string TextForURLFormat(string text)
        {
            text = RemoveAcents(text);

            var returnText = text.Replace(" ", "");

            const string permited = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmonopqrstuvwxyz0123456789-_";

            for (var i = 0; i < text.Length; i++)
                if (!permited.Contains(text.Substring(i, 1))) { returnText = returnText.Replace(text.Substring(i, 1), ""); }

            return returnText;
        }

        public static string GetNumbers(string text)
        {
            return string.IsNullOrEmpty(text) ? "" : new string(text.Where(char.IsDigit).ToArray());
        }

        public static string AdjustText(string value, int size)
        {
            if (value.Length > size)
            {
                value = value.Substring(1, size);
            }
            return value;
        }

        public static string ToTitleCase(string text)
        {
            return ToTitleCase(text, false);
        }

        public static string ToTitleCase(string text, bool mantainWordsAlreadyInUpperCase)
        {
            text = text.Trim();

            if (!mantainWordsAlreadyInUpperCase)
                text = text.ToLower();

            var textInfo = new CultureInfo("pt-BR", false).TextInfo;
            return textInfo.ToTitleCase(text);
        }
    }
}
