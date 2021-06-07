using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryMailUp.Utils
{
    public class HelperUtils
    {
        public enum ContentType
        {
            Json,
            Xml
        }
        public string GetContentTypeString(ContentType cType)
        {
            if (cType == ContentType.Json) return "application/json";
            else return "application/xml";
        }
    }
}
