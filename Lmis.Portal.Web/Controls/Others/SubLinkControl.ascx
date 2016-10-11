<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubLinkControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Others.SubLinkControl" %>

<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="LinkModel.ID" />
<lmis:HiddenFieldValueControl runat="server" ID="hdParentID" Property="LinkModel.ParentID" />

<table>
	<tr>
		<td>
			<ce:Label runat="server">Title</ce:Label></td>
		<td>
			<asp:TextBox runat="server" ID="tbxTitle" Property="LinkModel.Title" /></td>
	</tr>
	<tr>
		<td>
			<ce:Label runat="server">Url</ce:Label></td>
		<td>
			<asp:TextBox runat="server" ID="tbxUrl" Property="LinkModel.Url" /></td>
	</tr>
	<tr>
		<td>
			<ce:Label runat="server">Description</ce:Label></td>
		<td>
			<asp:TextBox runat="server" ID="tbxDescription" Property="LinkModel.Description" /></td>
	</tr>
</table>
