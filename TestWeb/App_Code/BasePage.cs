using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

using FW.HttpFlow;

namespace TestWeb
{
    public class BasePage : Page, IContextCare, IFlowCare
    {
        public object PageContext { get; set; }

        public virtual HttpContextItemCollection GetContext()
        {
            throw new NotImplementedException();
        }

        public virtual void SetContext(HttpContextItemCollection theContext)
        {
            throw new NotImplementedException();
        }

        public virtual void SetFlowFromPrevious(HttpFlowItemCollection theFlow)
        {
            throw new NotImplementedException();
        }
    }
}