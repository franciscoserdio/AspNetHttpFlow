using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.HttpFlow
{
    /// <summary>
    /// Collection of <see cref="HttpContextItem{T}"/> elements.
    /// </summary>
    public class HttpContextItemCollection
    {
        public Dictionary<string, object> Values { get; set; }
        
        public HttpContextItemCollection() : this(new List<HttpContextItem<object>>()) { }

        public HttpContextItemCollection(ICollection<HttpContextItem<object>> values)
        {
            this.Values = new Dictionary<string, object>();
            foreach (HttpContextItem<object> v in values)
            {
                this.Values.Add(v.Key, v.Value);
            }
        }

        public void Add(string key, object value)
        {
            this.Values.Add(key, value);
        }

        public void Add(HttpContextItem<object> value)
        {
            this.Values.Add(value.Key, value.Value);
        }

        public void Add<T>(HttpContextItem<T> value)
        {
            this.Values.Add(value.Key, value.Value);
        }

        public bool Contains(HttpContextItem<object> value)
        {
            return this.Values.ContainsKey(value.Key);
        }

        public object this[string key]
        {
            get { return this.Values[key]; }
            set { this.Values[key] = value; }
        }

        public void Remove(HttpContextItem<object> value)
        {
            this.Values.Remove(value.Key);
        }

        public void Remove<T>(HttpContextItem<T> value)
        {
            this.Values.Remove(value.Key);
        }

        public void Clear()
        {
            this.Values.Clear();
        }
    }
}
