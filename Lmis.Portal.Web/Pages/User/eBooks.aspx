<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="eBooks.aspx.cs" Inherits="Lmis.Portal.Web.Pages.User.eBooks" %>

<%@ Register Src="~/Controls/DataDisplay/EBooksControl.ascx" TagPrefix="lmis" TagName="EBooksControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
		<lmis:EBooksControl runat="server" ID="eBooksControl" />
</asp:Content>

