using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.HttpFlow
{
    public class HttpFlowItem<T>
    {
        public string Key { get; set; }
        public T Value { get; set; }

        private HttpFlowItem() { }

        public HttpFlowItem(string key, T value)
        {
            this.Key = key; 
            this.Value = value;
        }

        public static explicit operator HttpFlowItem<object>(HttpFlowItem<T> value)
        {
            return (HttpFlowItem<object>)Convert.ChangeType(value, typeof(HttpFlowItem<object>));
        }
    }
}
