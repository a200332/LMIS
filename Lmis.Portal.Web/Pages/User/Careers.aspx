<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Careers.aspx.cs" Inherits="Lmis.Portal.Web.Pages.User.Careers" %>

<%@ Register Src="~/Controls/DataDisplay/CareersControl.ascx" TagPrefix="lmis" TagName="CareersControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<lmis:CareersControl runat="server" ID="careersControl" />
</asp:Content>

