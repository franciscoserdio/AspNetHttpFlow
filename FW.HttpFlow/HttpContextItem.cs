using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.HttpFlow
{
    /// <summary>
    /// HTTP Context Item, for store/retrieve operations on the context of a Page. 
    /// </summary>
    /// <typeparam name="T">The type of Item of the context. </typeparam>
    public class HttpContextItem<T>
    {

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T Value { get; set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="HttpContextItem{T}"/> class from being created.
        /// </summary>
        private HttpContextItem() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpContextItem{T}"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public HttpContextItem(string key, T value)
        {
            this.Key = key; 
            this.Value = value;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="HttpContextItem{T}"/> to <see cref="HttpContextItem{System.Object}"/>.
        /// </summary>
        /// <param name="value">The <see cref="HttpContextItem{T}"/> to convert.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static explicit operator HttpContextItem<object>(HttpContextItem<T> value)
        {
            return (HttpContextItem<object>)Convert.ChangeType(value, typeof(HttpContextItem<object>));
        }
    }
}
