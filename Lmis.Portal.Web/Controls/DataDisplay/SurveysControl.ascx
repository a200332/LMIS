<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SurveysControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.SurveysControl" %>
<asp:Repeater runat="server" ID="rptItems">
    <ItemTemplate>
        <asp:HyperLink CssClass="book" NavigateUrl='<%# GetFileUrl(Eval("ID")) %>' Target="blanck" runat="server">
			<div class="book-title">
				<ce:Label runat="server" Text='<%# Eval("Title") %>' />
			</div>
			<div class="book-description">
				<ce:Label runat="server" Text='<%# Eval("Description") %>' />
			</div>
		</asp:HyperLink>
    </ItemTemplate>
</asp:Repeater>