using System;
using System.Linq;
using System.Web.Configuration;

namespace Nop.Integration.Umbraco.Core.Core
{
    public class WebConfigurationProvider : IConfigurationProvider
    {
        public T GetCongurationValue<T>(string key, T defaultValue)
        {
            var result = defaultValue;
            if (!WebConfigurationManager.AppSettings.AllKeys.Contains(key) )
                return result;

               var value= WebConfigurationManager.AppSettings[key];

            result= (T)Convert.ChangeType(value, typeof(T));

            return result;
        }
    }
}
