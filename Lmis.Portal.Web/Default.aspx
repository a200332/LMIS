<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/Controls/DataDisplay/VideosControl.ascx" TagPrefix="lmis" TagName="VideosControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="App_Themes/Default/slide.css" rel="stylesheet" />
    <div>
    </div>
    <lmis:VideosControl ID="videosControl" runat="server" />

    <a href="Pages/User/eBooks.aspx" target="_blank">
        <div class="left e-book">
            <ce:Label CssClass="e-book-l" runat="server">E-BOOK</ce:Label>
        </div>
    </a>
    <div class="right-menu">
        <ul>
            <a href="http://worknet.gov.ge/Home/AllOpenVacancies" target="_blank">
                <li class="vertical-center right-menu-padding rm-search">
                    <ce:Label runat="server">Job Finder</ce:Label></li>
            </a>
            <a href="http://worknet.gov.ge" title="თქვენ შეგიძლიათ დარეგისტრირდეთ როგორც სამუშაოს მაძიებლად ან განათავსოთ ვაკანსია" target="_blank">
                <li class="vertical-center right-menu-padding rm-profile">
                    <ce:Label runat="server">Create  Profile</ce:Label></li>
            </a>
            <asp:Calendar runat="server" FirstDayOfWeek="Monday"  CssClass="myCalendar" ID="clDefault" SelectedDate="<%# DateTime.Now %>">
                <OtherMonthDayStyle ForeColor="#b0b0b0" />
                <DayStyle CssClass="myCalendarDay" ForeColor="#2d3338" />
                <DayHeaderStyle CssClass="myCalendarDayHeader" ForeColor="#2d3338" />
                <SelectedDayStyle Font-Bold="True" Font-Size="12px" CssClass="myCalendarSelector" />
                <TodayDayStyle CssClass="myCalendarToday" />
                <SelectorStyle CssClass="myCalendarSelector" />
                <NextPrevStyle CssClass="myCalendarNextPrev" />
                <TitleStyle CssClass="myCalendarTitle" />
            </asp:Calendar>
        </ul>
        <%--<img src="App_Themes/Default/images/left-article.png" />--%>
    </div>
</asp:Content>

