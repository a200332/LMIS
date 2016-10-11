<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Others.CategoryControl" %>
<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="CategoryModel.ID" />
<lmis:HiddenFieldValueControl runat="server" ID="hdParentID" Property="CategoryModel.ParentID" />
<ul>
	<li>Category Name</li>
	<li>
		<asp:TextBox runat="server" Width="150" ID="tbxName" Property="CategoryModel.Name"></asp:TextBox></li>
</ul>
<ul>
	<li>Category Icon</li>
	<li>
		<asp:FileUpload runat="server" Width="200" ID="fuImage" Property="CategoryModel.Image" /></li>
</ul>
