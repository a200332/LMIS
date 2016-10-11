<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubLinksControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.SubLinksControl" %>
<asp:Repeater runat="server" ID="rptItems">
	<ItemTemplate>
		<a class="book" href='<%# Eval("Url") %>' target="blanck">
			<div class="book-title">
				<%# Eval("Title") %>
			</div>
			<div class="book-description">
				<%# Eval("Description") %>
			</div>
		</a>
	</ItemTemplate>
</asp:Repeater>
