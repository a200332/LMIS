<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EBooksControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.EBooksControl" %>
<asp:Repeater runat="server" ID="rptItems">
	<ItemTemplate>
		<a class="book" href='<%# Eval("Url") %>' target="blanck">
			<div class="book-title">
				<ce:Label runat="server" Text='<%# Eval("Title") %>' />
			</div>
			<div class="book-description">
				<ce:Label runat="server" Text='<%# Eval("Description") %>' />
			</div>
			<div class="book-description">
			</div>
		</a>
	</ItemTemplate>
</asp:Repeater>
