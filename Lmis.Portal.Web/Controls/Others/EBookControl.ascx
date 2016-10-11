<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EBookControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Others.EBookControl" %>

<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="EBookModel.ID" />

<table>
	<tr>
		<td>Title</td>
		<td>
			<asp:TextBox runat="server" ID="tbxTitle" Property="EBookModel.Title" /></td>
	</tr>
	<tr>
		<td>Url</td>
		<td>
			<asp:TextBox runat="server" ID="tbxUrl" Property="EBookModel.Url" /></td>
	</tr>
	<tr>
		<td>Description</td>
		<td>
			<asp:TextBox runat="server" ID="tbxDescription" Property="EBookModel.Description" /></td>
	</tr>
</table>