<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainLinkControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Others.MainLinkControl" %>

<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="LinkModel.ID" />

<table>
	<tr>
		<td>Title</td>
		<td>
			<asp:TextBox runat="server" ID="tbxTitle" Property="LinkModel.Title"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td></td>
		<td>
			<asp:FileUpload runat="server" ID="fuImage" Property="LinkModel.Image"></asp:FileUpload>
		</td>
	</tr>
</table>
