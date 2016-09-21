using System;
using System.Collections.Generic;
using System.Text;

namespace FW.HttpFlow
{
    /// <summary>
    /// Interface to implement in pages which are flow care in beetween user pathways
    /// </summary>
    /// <see cref="FW.HttpFlow.HttpFlowSystem"/>
    /// <see cref="FW.HttpFlow.IContextCare"/>
    public interface IFlowCare
    {
        /// <summary>
        /// Sets the flow from previous page.
        /// </summary>
        /// <param name="theFlow">The flow from previous page.</param>
        void SetFlowFromPrevious(HttpFlowItemCollection theFlow);
    }
}
