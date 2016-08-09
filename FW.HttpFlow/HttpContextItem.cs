using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.HttpFlow
{
    public class HttpContextItem<T>
    {
        public string Key { get; set; }
        public T Value { get; set; }

        private HttpContextItem() { }
        
        public HttpContextItem(string key, T value)
        {
            this.Key = key; 
            this.Value = value;
        }

        public static explicit operator HttpContextItem<object>(HttpContextItem<T> value)
        {
            return (HttpContextItem<object>)Convert.ChangeType(value, typeof(HttpContextItem<object>));
        }
    }
}
