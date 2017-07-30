<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UserReports.aspx.cs" Inherits="Lmis.Portal.Web.Pages.User.UserReports" %>

<%@ Register Src="~/Controls/DataDisplay/UserReportsControl.ascx" TagPrefix="lmis" TagName="UserReportsControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <lmis:UserReportsControl runat="server" ID="userReportsControl" />
</asp:Content>

