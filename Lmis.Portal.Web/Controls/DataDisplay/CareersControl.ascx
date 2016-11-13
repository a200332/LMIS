<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CareersControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.CareersControl" %>
<asp:Repeater runat="server" ID="rptItems">
    <ItemTemplate>
        <asp:HyperLink CssClass="book" NavigateUrl='<%# GetTargetUrl(Container.DataItem) %>' Target='<%# GetTarget(Container.DataItem) %>' runat="server">
			<div class="book-title">
				<ce:Label runat="server" Text='<%# Eval("Title") %>' />
			</div>
			<div class="book-description">
				<ce:Label runat="server" Text='<%# Eval("Description") %>' />
			</div>
		</asp:HyperLink>
    </ItemTemplate>
</asp:Repeater>