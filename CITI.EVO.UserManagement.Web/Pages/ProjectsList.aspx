<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ProjectsList.aspx.cs" Inherits="Pages_ProjectsList" %>
<%@ Register TagPrefix="mis" Namespace="CITI.EVO.Tools.Web.UI.Controls" Assembly="CITI.EVO.Tools" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">
    <h1>მოდულები
    </h1>
    <div class="page_title_separator"></div>
    <div class="fieldset">
        <asp:LinkButton runat="server" CssClass="icon" ID="btNewProject" ToolTip="მოდულის დამატება"
            Text="მოდულის დამატება"
            OnClick="btNewProject_Click" />
        <asp:UpdatePanel runat="server" ID="upnlMain" UpdateMode="Always">
            <ContentTemplate>
                <mis:ASPxGridView ID="gvProjects" ClientInstanceName="gvProjects" runat="server" KeyFieldName="ID"
                    Width="900" EnableRowsCache="False">
                    <GroupSummary>
                        <dx:ASPxSummaryItem DisplayFormat="რაოდენობა = {0:d}" SummaryType="Count" />
                    </GroupSummary>
                    <SettingsBehavior AllowGroup="true" AllowSort="true" />
                    <Settings ShowGroupPanel="true" ShowFilterBar="Hidden" />
                    <SettingsText HeaderFilterShowAll="ყველა" HeaderFilterShowBlanks="მნიშვნელობის გარეშე"
                        GroupContinuedOnNextPage="(გაგრძელება იხ. შემდეგ გვერდზე)" HeaderFilterShowNonBlanks="შევსებული"
                        GroupPanel="გადმოიტანეთ ის სვეტი რომლის მიხედვითაც გინდათ ცხრილის დაჯგუფება"
                        EmptyDataRow="მონაცემები არ მოიძებნა" />
                    <SettingsPager Position="Bottom" PageSize="25">
                        <Summary Text="{0} გვერდი {1}-დან (სულ {2})"></Summary>
                        <PageSizeItemSettings Items="25, 50, 100, 200" Visible="True" Caption="ჩანაწერების რაოდენობა" />
                    </SettingsPager>
                    <SettingsLoadingPanel Text="მიმდინარეობს მონაცემების დამუშავება&amp;hellip;" />
                    <Styles>
                        <Header ForeColor="#5D5D5D" Wrap="True" HorizontalAlign="Center">
                            <Border BorderColor="#F7F7F7" BorderStyle="Solid" />

                        </Header>
                        <AlternatingRow Enabled="True">
                        </AlternatingRow>
                        <FocusedRow BackColor="#d7d7d7">
                        </FocusedRow>
                        <GroupPanel BackColor="#003399" ForeColor="White">
                            <Border BorderColor="White" />
                            <BorderTop BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                            <BorderBottom BorderColor="White" BorderStyle="Solid" />
                        </GroupPanel>
                    </Styles>
                    <Columns>
                        <dx:GridViewDataColumn VisibleIndex="4" Width="3px" Name="Edit">
                            <DataItemTemplate>
                                <mis:ImageLinkButton ID="lnkEdit" runat="server" CommandArgument='<% #Eval("ID")%>'
                                    DefaultImageUrl="~/App_Themes/default/images/edit.png" ToolTip="რედაქტირება"
                                    OnClick="lnkEdit_Click" />
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="5" Width="3px" Name="AddMessage">
                            <DataItemTemplate>
                                <mis:ImageLinkButton ID="lnkAddMessage" runat="server" CommandArgument='<% #Eval("ID")%>'
                                    DefaultImageUrl="~/App_Themes/default/images/add_message.png" ToolTip="მესიჯის დამატება"
                                    OnClick="lnkAddMessage_Click" />
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn VisibleIndex="6" Width="3px" Name="Delete">
                            <DataItemTemplate>
                                <mis:ImageLinkButton ID="lnkDelete" runat="server" CommandArgument='<% #Eval("ID")%>'
                                    DefaultImageUrl="~/App_Themes/default/images/delete.png" ToolTip="წაშლა" OnClick="lnkDelete_Click"
                                    OnClientClick="return confirm('გსურთ მოდულის წაშლა?');" />
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="ID" FieldName="ID" VisibleIndex="1" Visible="true" />
                        <dx:GridViewDataColumn Caption="მოდული" FieldName="Name" VisibleIndex="2">
                            <HeaderCaptionTemplate>
                                <asp:Label runat="server">მოდული</asp:Label>
                            </HeaderCaptionTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="სტატუსი" FieldName="IsActive" VisibleIndex="3">
                            <HeaderCaptionTemplate>
                                <asp:Label ID="Label2" runat="server">სტატუსი</asp:Label>
                            </HeaderCaptionTemplate>
                        </dx:GridViewDataColumn>
                    </Columns>
                </mis:ASPxGridView>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <asp:Panel ID="pnlProject" runat="server" Style="display: none;" CssClass="modalWindow"
      DefaultButton="btProjectOK">
        <asp:UpdatePanel ID="upnlProject" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <asp:Button ID="btProjectPopup" runat="server" Style="display: none;" />
                <act:ModalPopupExtender ID="mpeProject" TargetControlID="btProjectPopup" PopupControlID="pnlProject"
                    runat="server" Enabled="True" BackgroundCssClass="modalBackground" DynamicServicePath="" />
                <div class="popup">
                    <div class="popup_fieldset">
                        <asp:HiddenField ID="hdProjectID" runat="server" />
                        <asp:Label ID="lblProjectError" runat="server" ForeColor="Red"></asp:Label>
                        <h2>
                            <asp:Label ID="ModuleContext" runat="server"></asp:Label></h2>

                        <div class="clear"></div>


                        <div class="title_separator"></div>
                        <div class="box">
                            <h3>
                                <asp:Label runat="server">მოდულის სახელი</asp:Label></h3>
                            <div class="box_body">
                                <asp:TextBox ID="tbProjectName" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="box">
                            <h3>
                                <asp:Label ID="Label1" runat="server">მოდულის სტატუსი</asp:Label></h3>
                            <div class="box_body">
                                <mis:CheckBox ID="chkIsActive" runat="server" />
                            </div>
                        </div>

                    </div>
                    <div class="fieldsetforicons">
                        <div class="left">
                            <asp:LinkButton ID="btProjectOK" CssClass="icon" runat="server" Text="შენახვა" ToolTip="შენახვა"
                               OnClick="btProjectOK_Click" />

                        </div>
                        <div class="right">
                             <asp:LinkButton ID="btProjectCancel" CssClass="icon" runat="server" Text="დახურვა" ToolTip="დახურვა" />

                        </div>
                    </div>

                </div>













            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <mis:MessageControl runat="server" ID="ucMessage" />
</asp:Content>
