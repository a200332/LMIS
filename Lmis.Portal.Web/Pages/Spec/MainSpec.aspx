<%@ Page Title="" Language="C#" MasterPageFile="~/Spec.master" AutoEventWireup="true" CodeFile="MainSpec.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Spec.MainSpec" %>

<%@ Register Src="~/Controls/SpecDisplay/SpecControl.ascx" TagPrefix="lmis" TagName="SpecControl" %>
<%@ Register Src="~/Controls/SpecDisplay/CategoriesControl.ascx" TagPrefix="lmis" TagName="CategoriesControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel runat="server" ID="pnlSpecCategories">
        <lmis:CategoriesControl runat="server" ID="categoriesControl" />
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSpecData">
        <lmis:SpecControl runat="server" ID="specControl" />
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlBack">
        <a href="javascript:window.history.back();">
            <ce:Label runat="server" Text="Back" />
        </a>
    </asp:Panel>
</asp:Content>

