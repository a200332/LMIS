<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainLinksControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.MainLinksControl" %>
<ul>
	<asp:Repeater runat="server" ID="rptItems">
		<ItemTemplate>
			<li class="mainlink">
				<asp:HyperLink NavigateUrl='<%# GetSubLinksUrl(Eval("ID")) %>' runat="server">
					<asp:Panel runat="server" style='<%# GetPanelStyle(Container.DataItem)%>'>
						<ce:Label runat="server"><%# Eval("Title") %></ce:Label>
					</asp:Panel>
				</asp:HyperLink>
			</li>
		</ItemTemplate>
	</asp:Repeater>
</ul>
