<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainLinksControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.MainLinksControl" %>
<ul>
	<asp:Repeater runat="server" ID="rptItems">
		<ItemTemplate>
			<li>
				<ce:ImageLinkButton NavigateUrl='<%# GetSubLinksUrl(Eval("ID")) %>' DefaultImageUrl='<%# GetImageLink(Eval("ID")) %>' runat="server" />
			</li>
		</ItemTemplate>
	</asp:Repeater>
</ul>
