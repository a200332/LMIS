<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VideoControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Others.VideoControl" %>

<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="VideoModel.ID" />

<table>
	<tr>
		<td>
			<ce:Label runat="server">Title</ce:Label></td>
		<td>
			<asp:TextBox runat="server" ID="tbxTitle" Property="VideoModel.Title" /></td>
	</tr>
	<tr>
		<td>
			<ce:Label runat="server">Url</ce:Label></td>
		<td>
			<asp:TextBox runat="server" ID="tbxUrl" Property="VideoModel.Url" /></td>
	</tr>
	<tr>
		<td>
			<ce:Label runat="server">Description</ce:Label></td>
		<td>
			<asp:TextBox runat="server" ID="tbxDescription" Property="VideoModel.Description" /></td>
	</tr>
</table>
