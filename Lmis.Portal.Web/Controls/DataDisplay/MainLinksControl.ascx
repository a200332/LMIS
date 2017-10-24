<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainLinksControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.MainLinksControl" %>
<ul class="mainlink">
    <asp:Repeater runat="server" ID="rptItems">
        <ItemTemplate>
            <li>
                <div style="border: 1px solid #2D4E85; width: 182px;">
                    <asp:HyperLink NavigateUrl='<%# GetSubLinksUrl(Container.DataItem) %>' runat="server" Target='<%# GetUrlTarget(Container.DataItem) %>'>
					<asp:Panel runat="server" style='<%# GetPanelStyle(Container.DataItem)%>'>					  
                        <div  class="mainlinkbg1" ></div>
                        <ce:Label runat="server" Text='<%# Eval("Title") %>'/>
					</asp:Panel>
                    </asp:HyperLink>
                </div>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
