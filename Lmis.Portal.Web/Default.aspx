<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/Controls/DataDisplay/VideosControl.ascx" TagPrefix="lmis" TagName="VideosControl" %>
<%@ Register Src="~/Controls/DataDisplay/MainCategoriesControl.ascx" TagPrefix="lmis" TagName="MainCategoriesControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="App_Themes/Default/slide.css" rel="stylesheet" />
    <asp:Label runat="server" ID="lblRealHost" EnableViewState="False" Visible="False"></asp:Label>
    <div style="float: left;">
        <table>
            <tbody>
                <tr>
                    <td>
                        <div class="left">
                            <div style="padding: 1px;">
                                <asp:HyperLink runat="server" ID="lnkLabourMarket" NavigateUrl="~/Pages/User/Dashboard.aspx?CategoryCode=001">
                                    <div style="width: 160px; height: 172px; margin: 0 5px 5px 0; font-size: 14px; background-color: #dfe87b; color: white; background-image: url('App_Themes/Default/images/main-labour.png');">
                                        <div style="padding-top: 120px;">
                                            <asp:Label runat="server" ID="lblLabourMarket" Text="შრომის ბაზარი" />
                                        </div>
                                    </div>
                                </asp:HyperLink>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div class="left">
                            <div style="padding: 1px;">
                                <asp:HyperLink runat="server" ID="lnkEconomic" NavigateUrl="~/Pages/User/Dashboard.aspx?CategoryCode=600">
                                    <div style="width: 160px; height: 171px; margin: 0 5px 5px 0; font-size: 14px; background-color: #dfe87b; color: white; background-image: url('App_Themes/Default/images/main-economic.png');">
                                         <div style="padding-top: 120px;">
                                            <asp:Label runat="server" ID="lblEconomic" Text="მაკროეკონომიკა" />
                                        </div>
                                    </div>
                                </asp:HyperLink>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div class="left">
                            <div style="padding: 1px;">
                                <asp:HyperLink runat="server" ID="lnkEducation" NavigateUrl="~/Pages/User/Dashboard.aspx?CategoryCode=800">
                                    <div style="width: 160px; height: 171px; margin: 0 5px 5px 0; font-size: 14px; background-color: #dfe87b; color: white; background-image: url('App_Themes/Default/images/main-edu.png');">
                                       <div style="padding-top: 120px;">
                                            <asp:Label runat="server" ID="lblEducation" Text="განათლება" />
                                        </div>
                                    </div>
                                </asp:HyperLink>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="left">
                            <div style="padding: 1px;">
                                <asp:HyperLink runat="server" ID="lnkPopulation" NavigateUrl="~/Pages/User/Dashboard.aspx?CategoryCode=500">
                                    <div style="width: 160px; height: 171px; margin: 0 5px 5px 0; font-size: 14px; background-color: #dfe87b; color: white; background-image: url('App_Themes/Default/images/main-population.png');">
                                          <div style="padding-top: 120px;">
                                            <asp:Label runat="server" ID="lblPopulation" Text="მოსახლეობა" />
                                        </div>
                                    </div>
                                </asp:HyperLink>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div class="left">
                            <div style="padding: 1px;">
                                <asp:HyperLink runat="server" ID="lnkInvestitions" NavigateUrl="~/Pages/User/Dashboard.aspx?CategoryCode=700">
                                    <div style="text-align: center; vertical-align: bottom; width: 160px; height: 171px; margin: 0 5px 5px 0; font-size: 14px; background-color: #dfe87b; color: white; background-image: url('App_Themes/Default/images/main-interventions.png');">
                                    <div style="padding-top: 120px;">
                                            <asp:Label runat="server" ID="lblInvestitions" Text="ინვესტიციები" />
                                        </div>
                                    </div>
                                </asp:HyperLink>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div class="left">
                            <div style="padding: 1px;">
                                <asp:HyperLink runat="server" ID="lnkReportsClone" NavigateUrl="~/Pages/User/Dashboard.aspx?CategoryCode=900">
                                    <div style="width: 160px; height: 171px; margin: 0 5px 5px 0; font-size: 14px; background-color: #dfe87b; color: white; background-image: url('App_Themes/Default/images/main-config.png');">
                                            <div style="padding-top: 120px;">
                                            <ce:Label runat="server" ID="lblReportsClone" Text="საერთაშორისო შედარებები" />
                                        </div>
                                    </div>
                                </asp:HyperLink>
                            </div>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>


    <%--<lmis:VideosControl ID="videosControl" runat="server" />--%>

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
            <div style="padding-top: 10px;">
                <asp:Calendar runat="server" FirstDayOfWeek="Monday" CssClass="myCalendar" ID="clDefault" DayNameFormat="FirstTwoLetters" SelectedDate="<%# DateTime.Now %>">
                    <OtherMonthDayStyle ForeColor="#b0b0b0" />
                    <DayStyle CssClass="myCalendarDay" ForeColor="#2d3338" />
                    <DayHeaderStyle CssClass="myCalendarDayHeader" ForeColor="#2d3338" />
                    <SelectedDayStyle Font-Bold="True" Font-Size="12px" CssClass="myCalendarSelector" />
                    <TodayDayStyle CssClass="myCalendarToday" />
                    <SelectorStyle CssClass="myCalendarSelector" />
                    <NextPrevStyle CssClass="myCalendarNextPrev" />
                    <TitleStyle CssClass="myCalendarTitle" />
                </asp:Calendar>
            </div>
        </ul>
        <%--<img src="App_Themes/Default/images/left-article.png" />--%>
    </div>
</asp:Content>

