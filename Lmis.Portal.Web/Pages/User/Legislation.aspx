<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Legislation.aspx.cs" Inherits="Lmis.Portal.Web.Pages.User.Legislation" %>

<%@ Register Src="~/Controls/DataDisplay/LegislationsControl.ascx" TagPrefix="lmis" TagName="LegislationsControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<lmis:LegislationsControl runat="server" ID="legislationsControl" />
</asp:Content>

