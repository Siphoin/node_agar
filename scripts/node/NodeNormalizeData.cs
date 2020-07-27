
using System.Collections.Generic;
using System.Globalization;
using System.Text;

public static    class NodeNormalizeData
    {
        public static string Normalize (string str)
        {
          string  str2 = str;
            str2 = str2.Substring(1);
            str2 = str2.Substring(0, str2.Length - 1);
        str2 = string.Format(str2);
            return str2;
        }

    public static string NormalizeString(string str)
    {
        string str2 = str;
        //   str2 = str2.Substring(1);
       //  str2 = str2.Substring(0, str2.Length - 1);
        return str2;
    }

    public static float NormalizeFloat(string str)
    {
        CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        ci.NumberFormat.CurrencyDecimalSeparator = ".";
        return float.Parse(string.Format("{0}", str), NumberStyles.Any, ci);
    }
}
