<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/Controls/DataDisplay/VideosControl.ascx" TagPrefix="lmis" TagName="VideosControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="App_Themes/Default/slide.css" rel="stylesheet" />

    <lmis:VideosControl ID="videosControl" runat="server" />

    <a href="Pages/User/eBooks.aspx" target="_blank">
        <div class="left e-book">
            <ce:Label CssClass="e-book-l" runat="server">E-BOOK</ce:Label>
        </div>
    </a>
    <div class="right-menu">
        <ul>
            <a href="http://worknet.gov.ge" target="_blank">
                <li class="vertical-center right-menu-padding rm-search">
                    <ce:Label runat="server">Job Finder</ce:Label></li>
            </a>
            <a href="#">
                <li class="vertical-center right-menu-padding rm-profile">
                    <ce:Label runat="server">Create  Profile</ce:Label></li>
            </a>
            <a href="#">
                <li class="vertical-center right-menu-padding rm-postjob">
                    <ce:Label runat="server">Post the job</ce:Label></li>
            </a>
            <a href="#">
                <li class="last vertical-center right-menu-padding rm-rate">
                    <ce:Label runat="server">Rate the website</ce:Label></li>
            </a>
        </ul>
        <%--<img src="App_Themes/Default/images/left-article.png" />--%>
    </div>
</asp:Content>

