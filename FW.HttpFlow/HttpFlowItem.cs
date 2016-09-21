using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.HttpFlow
{
    /// <summary>
    /// HTTP Flow Item, to flow from one Page to another. 
    /// </summary>
    /// <typeparam name="T">The type of Item of the flow. </typeparam>
    public class HttpFlowItem<T>
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
        /// Prevents a default instance of the <see cref="HttpFlowItem{T}"/> class from being created.
        /// </summary>
        private HttpFlowItem() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpFlowItem{T}"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public HttpFlowItem(string key, T value)
        {
            this.Key = key; 
            this.Value = value;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="HttpFlowItem{T}"/> to <see cref="HttpFlowItem{System.Object}"/>.
        /// </summary>
        /// <param name="value">The <see cref="HttpFlowItem{T}"/> value to convert.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static explicit operator HttpFlowItem<object>(HttpFlowItem<T> value)
        {
            return (HttpFlowItem<object>)Convert.ChangeType(value, typeof(HttpFlowItem<object>));
        }
    }
}
