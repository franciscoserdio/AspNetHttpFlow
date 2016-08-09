using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

namespace FW.HttpFlow
{
    /// <summary>
    /// Interface to implement in pages which are context care in beetween user pathways
    /// </summary>
    /// <see cref="FW.HttpFlow.HttpFlowSystem"/>
    /// <see cref="FW.HttpFlow.IFlowCare"/>
    public interface IContextCare
    {
        /// <summary>
        /// Gets the context of the page
        /// </summary>
        HttpContextItemCollection GetContext();

        /// <summary>
        /// Sets the context to the page
        /// </summary>
        /// <param name="theContext">The page's context.</param>
        void SetContext(HttpContextItemCollection theContext);
    }
}
