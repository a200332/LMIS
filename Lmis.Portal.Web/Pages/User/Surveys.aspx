<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Surveys.aspx.cs" Inherits="Lmis.Portal.Web.Pages.User.Surveys" %>

<%@ Register Src="~/Controls/DataDisplay/SurveysControl.ascx" TagPrefix="lmis" TagName="SurveysControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<lmis:SurveysControl runat="server" ID="surveysControl" />
</asp:Content>

