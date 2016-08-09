using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.HttpFlow
{
    public class HttpFlowItemCollection
    {
        public Dictionary<string, object> Values { get; set; }

        public HttpFlowItemCollection() : this(new List<HttpFlowItem<object>>()) { }

        public HttpFlowItemCollection(ICollection<HttpFlowItem<object>> values)
        {
            this.Values = new Dictionary<string, object>();
            foreach (HttpFlowItem<object> v in values)
            {
                this.Values.Add(v.Key, v.Value);
            }
        }

        public void Add(string key, object value)
        {
            this.Values.Add(key, value);
        }

        public void Add(HttpFlowItem<object> value)
        {
            this.Values.Add(value.Key, value.Value);
        }

        public void Add<T>(HttpFlowItem<T> value)
        {
            this.Values.Add(value.Key, value.Value);
        }

        public bool Contains(HttpFlowItem<object> value)
        {
            return this.Values.ContainsKey(value.Key);
        }

        public object this[string key]
        {
            get { return this.Values[key]; }
            set { this.Values[key] = value; }
        }

        public void Remove(HttpFlowItem<object> value)
        {
            this.Values.Remove(value.Key);
        }

        public void Remove<T>(HttpFlowItem<T> value)
        {
            this.Values.Remove(value.Key);
        }

        public void Clear()
        {
            this.Values.Clear();
        }
    }
}
