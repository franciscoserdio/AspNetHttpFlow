using System;
using System.Collections.Generic;
using FW.HttpFlow;

public partial class _Page_1 : TestWeb.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (null != this.PageContext)
        {
            this.txt_context.Text = this.PageContext as string;
        }
    }

    private HttpFlowItemCollection getFlow()
    {
        HttpFlowItemCollection itc = new HttpFlowItemCollection();
        itc.Add(txt_flow.ID, txt_flow.Text);
        return itc;
    }

    protected void btn_default_Click(object sender, EventArgs e)
    {
        HttpFlowSystem.Forward(
            TestWeb.Names.Views.DEFAULT,
            this.getFlow());
    }

    protected void btn_page_1_Click(object sender, EventArgs e)
    {
        HttpFlowSystem.Forward(
            TestWeb.Names.Views.PAGE_1,
            this.getFlow());
    }

    protected void btn_page_2_Click(object sender, EventArgs e)
    {
        HttpFlowSystem.Forward(
            TestWeb.Names.Views.PAGE_2,
            this.getFlow());
    }

    protected void btn_back_Click(object sender, EventArgs e)
    {
        HttpFlowSystem.Backward(this.getFlow());
    }

    public override HttpContextItemCollection GetContext()
    {
        HttpContextItemCollection theContext = new HttpContextItemCollection();
        theContext.Add(new HttpContextItem<object>(this.txt_context.ID, this.txt_context.Text));
        return theContext;
    }

    public override void SetContext(HttpContextItemCollection theContext)
    {
        this.txt_context.Text = theContext[this.txt_context.ID] as string;
    }

    public override void SetFlowFromPrevious(HttpFlowItemCollection theFlow)
    {
        this.txt_flow.Text = theFlow[this.txt_flow.ID] as string;
    }
}