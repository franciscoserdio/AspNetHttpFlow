<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Page_2.aspx.cs" Inherits="_Page_2" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <h2>Page 2</h2>
        <p>
        </p>
        <p>
            <asp:Button runat="server" ID="btn_default" Text="Go to Page Default" Width="200px" OnClick="btn_default_Click" />
            <br />
            <asp:Button runat="server" ID="btn_page_1" Text="Go to Page 1 " Width="200px" OnClick="btn_page_1_Click" />
            <br />
            <asp:Button runat="server" ID="btn_page_2" Text="Go to Page 2" Width="200px" OnClick="btn_page_2_Click" />
            <br />
            <asp:Button runat="server" ID="btn_back" Text="Go back with HttpFlowSystem" Width="200px" OnClick="btn_back_Click" />
            <br />
            <br />
            Context
            <br />
            <asp:TextBox runat="server" ID="txt_context" Width="200px" />
            <br />
            <br />
            Flow
            <br />
            <asp:TextBox runat="server" ID="txt_flow" Width="200px" />
        </p>
    </div>
</asp:Content>
