<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TableData.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.TableData" %>

<%@ Register Src="~/Controls/DataManipulation/TableDataControl.ascx" TagPrefix="lmis" TagName="TableDataControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<lmis:TableDataControl runat="server" ID="tableDataControl" />
</asp:Content>

