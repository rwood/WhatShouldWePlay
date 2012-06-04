using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace WSWP
{
    public static class Extensions
    {
        public static T GetAttribute<T>(this XmlElement elem, string attribute, T defaultValue) where T : struct
        {
            string a = elem.GetAttribute(attribute);
            if ((a == null) || (typeof(T) != typeof(string) && a == string.Empty))
                return defaultValue;
            try
            {
                return (T)Convert.ChangeType(a, typeof(T));
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}