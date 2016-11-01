<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="Lmis.Portal.Web.Pages.User.News" %>

<%@ Register Src="~/Controls/DataDisplay/NewsControl.ascx" TagPrefix="lmis" TagName="NewsControl" %>
<%@ Register Src="~/Controls/DataDisplay/NewsListControl.ascx" TagPrefix="lmis" TagName="NewsListControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="news">
        <lmis:NewsControl runat="server" ID="newsControl" />
    </div>
    <div class="news-link">
        <lmis:NewsListControl runat="server" ID="newsListControl" />
    </div>
</asp:Content>

