<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Projects.aspx.cs" Inherits="Lmis.Portal.Web.Pages.User.Projects" %>

<%@ Register Src="~/Controls/DataDisplay/ProjectsControl.ascx" TagPrefix="lmis" TagName="ProjectsControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<lmis:ProjectsControl runat="server" ID="projectsControl" />
</asp:Content>

