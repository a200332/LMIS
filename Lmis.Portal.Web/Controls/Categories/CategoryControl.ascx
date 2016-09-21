<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Categories.CategoryControl" %>
<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="CategoryModel.ID" />
<lmis:HiddenFieldValueControl runat="server" ID="hdParentID" Property="CategoryModel.ParentID" />
<asp:TextBox runat="server" ID="tbxName" Property="CategoryModel.Name"></asp:TextBox>