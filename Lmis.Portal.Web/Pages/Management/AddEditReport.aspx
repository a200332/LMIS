<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="AddEditReport.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.AddEditReport" %>

<%@ Register Src="~/Controls/SchemaManipulation/ReportControl.ascx" TagPrefix="lmis" TagName="ReportControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	
	<div>
		<lmis:ReportControl runat="server" ID="reportControl" />
	</div>
    <div class="wrapper">	</div>
		<div class="left" style="padding-right: 10px;">
			<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnSaveReport" ToolTip="დამატება" OnClick="btnSaveReport_OnClick" />
		</div>
		<div class="left">
			<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close.png" ID="btnCancelReport" ToolTip="დახურვა" OnClick="btnCancelReport_OnClick" />
		</div>

</asp:Content>

