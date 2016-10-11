<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LogicControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataManipulation.LogicControl" %>

<%@ Register Src="~/Controls/Common/ExpressionsLogicControl.ascx" TagPrefix="local" TagName="ExpressionsLogicControl" %>


<asp:Panel runat="server" ID="pnlSourceType">
    <div class="left" style="padding-right: 10px;">
        <asp:RadioButtonList runat="server" ID="lstSourceType" Property="LogicModel.SourceType" AutoPostBack="True" RepeatDirection="Horizontal">
            <Items>
                <asp:ListItem Text="Table" Value="Table" Selected="True" />
                <asp:ListItem Text="Logic" Value="Logic" />
            </Items>
        </asp:RadioButtonList>
    </div>
    <div class="left" style="padding-right: 10px;">
        <dx:ASPxComboBox runat="server" ID="cbxSource" TextField="Name" ValueField="ID" Property="LogicModel.SourceID">
        </dx:ASPxComboBox>
    </div>

</asp:Panel>

<div class="left" style="padding-right: 10px;">
    <asp:Panel runat="server" ID="pnlType">
        <asp:RadioButtonList runat="server" ID="lstType" Property="LogicModel.Type" AutoPostBack="True" RepeatDirection="Horizontal">
            <Items>
                <asp:ListItem Text="Logic" Value="Logic" Selected="True" />
                <asp:ListItem Text="Query" Value="Query" />
            </Items>
        </asp:RadioButtonList>
    </asp:Panel>
</div>

<div class="left">
    <asp:Panel runat="server" ID="pnlName">
        <asp:TextBox runat="server" Property="LogicModel.Name"></asp:TextBox>
    </asp:Panel>
</div>
<div class="clear"></div>
<div class="admin-fieldset"  style="padding: 10px 10px 10px 0;">
    <asp:Panel runat="server" ID="pnlQuery">
        <asp:TextBox runat="server" TextMode="MultiLine" Property="LogicModel.Query"></asp:TextBox>
    </asp:Panel>
</div>




<div class="wrapper"></div>

<asp:Panel runat="server" ID="pnlLogic">
    <local:ExpressionsLogicControl runat="server" ID="expressionsLogicControl" Property="LogicModel.ExpressionsLogic"></local:ExpressionsLogicControl>
</asp:Panel>

