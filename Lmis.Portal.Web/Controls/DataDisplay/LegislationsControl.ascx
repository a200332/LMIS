<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LegislationsControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.LegislationsControl" %>
<asp:Repeater runat="server" ID="rptItems">
    <ItemTemplate>
        <asp:HyperLink CssClass="book" NavigateUrl='<%# GetTargeteUrl(Container.DataItem) %>' Target="_blank" runat="server">
			<div class="book-title">
				<ce:Label runat="server" Text='<%# Eval("Title") %>' />
			</div>
			<div class="book-description">
				<ce:Label runat="server" Text='<%# Eval("Description") %>' />
			</div>
		</asp:HyperLink>
    </ItemTemplate>
</asp:Repeater>
