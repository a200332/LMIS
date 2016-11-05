<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubLinkControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.SubLinkControl" %>

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
	<tr>
		<td>
			<ce:Label runat="server">Image</ce:Label></td>
		<td>
			<asp:FileUpload runat="server" ID="fuImage" Property="LinkModel.Image"></asp:FileUpload>
		</td>
	</tr>
</table>
