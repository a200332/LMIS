<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Links.aspx.cs" Inherits="Lmis.Portal.Web.Pages.User.Links" %>

<%@ Register Src="~/Controls/DataDisplay/SubLinksControl.ascx" TagPrefix="lmis" TagName="SubLinksControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<lmis:SubLinksControl runat="server" ID="subLinksControl" />
</asp:Content>

