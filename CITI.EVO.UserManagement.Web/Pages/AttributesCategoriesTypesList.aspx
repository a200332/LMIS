<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AttributesCategoriesTypesList.aspx.cs" Inherits="Pages_AttributesCategoriesTypesList" %>
<%@ Register TagPrefix="mis" Namespace="CITI.EVO.Tools.Web.UI.Controls" Assembly="CITI.EVO.Tools" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="upnlMain" UpdateMode="Always" RenderMode="Block">
        <ContentTemplate>
            <h1>
                <asp:Label runat="server">ატრიბუტების კატეგორიები/ტიპები</asp:Label>
            </h1>
            <div class="page_title_separator"></div>
            <mis:ASPxTreeList ID="tlAttributes" runat="server" AutoGenerateColumns="False"
                BorderTop-BorderWidth="0px" Width="100%"
                BorderBottom-BorderWidth="0px" KeyFieldName="ID" ParentFieldName="ParentID" ClientIDMode="AutoID"
                ViewStateMode="Disabled">
                <Settings ShowGroupFooter="false" ShowFooter="false" GridLines="Both" ShowTreeLines="True" />
                <SettingsBehavior ExpandCollapseAction="NodeDblClick" AllowSort="True" AllowFocusedNode="true" />
                <SettingsEditing Mode="EditFormAndDisplayNode" />
                <SettingsPager Position="Bottom" PageSize="25">
                    <Summary Text="{0} გვერდი {1}-დან (სულ {2})"></Summary>
                    <PageSizeItemSettings Items="25, 50, 100, 200" Visible="True" Caption="ჩანაწერების რაოდენობა" />
                </SettingsPager>
                <SettingsLoadingPanel Text="მიმდინარეობს მონაცემების დამუშავება&amp;hellip;" />
                <Styles>
                    <Header ForeColor="#5D5D5D" Wrap="true" HorizontalAlign="Center">
                        <Border BorderColor="#F7F7F7" BorderStyle="Solid"></Border>
                    </Header>
                    <AlternatingNode Enabled="true" />
                    <FocusedNode BackColor="#d7d7d7" ForeColor="#003399" />
                    <Cell HorizontalAlign="Left" VerticalAlign="Middle" Border-BorderColor="#cfcfcf"
                        Border-BorderWidth="1px">
                        <Border BorderColor="#CFCFCF" BorderWidth="1px" />
                    </Cell>
                    <Header HorizontalAlign="Center" />
                </Styles>
                <Columns>
                    <dx:TreeListHyperLinkColumn VisibleIndex="2" Width="3px">
                        <DataCellTemplate>
                            <mis:ImageLinkButton ID="lnkEdit" runat="server" ToolTip="რედაქტირება" DefaultImageUrl="~/App_Themes/default/images/edit.png"
                                CommandArgument='<% #Eval("Key")%>' Visible='<%# GetEditVisible(Eval("Type")) %>' OnClick="lnkEdit_Click" />
                        </DataCellTemplate>
                    </dx:TreeListHyperLinkColumn>
                    <dx:TreeListHyperLinkColumn VisibleIndex="3" Width="3px">
                        <DataCellTemplate>
                            <mis:ImageLinkButton ID="lnkDelete" runat="server" ToolTip="წაშლა" DefaultImageUrl="~/App_Themes/default/images/delete.png"
                                 CommandArgument='<% #Eval("Key")%>' Visible='<%# GetDeleteVisible(Eval("Type")) %>' OnClick="lnkDelete_Click" />
                        </DataCellTemplate>
                    </dx:TreeListHyperLinkColumn>
                    <dx:TreeListHyperLinkColumn VisibleIndex="4" Width="3px">
                        <DataCellTemplate>
                            <mis:ImageLinkButton ID="lnkNew" runat="server" ToolTip="დამატება" DefaultImageUrl="~/App_Themes/default/images/add.png"
                                CommandArgument='<% #Eval("Key")%>' Visible='<%# GetNewVisible(Eval("Type")) %>' OnClick="lnkNew_Click" />
                        </DataCellTemplate>
                    </dx:TreeListHyperLinkColumn>
                    <dx:TreeListTextColumn>
                        <HeaderCaptionTemplate>
                            <asp:Label runat="server" Text="დასახელება" />
                        </HeaderCaptionTemplate>
                        <DataCellTemplate>
                            <asp:Label runat="server" Text='<%#Eval("Name") %>' />
                        </DataCellTemplate>
                    </dx:TreeListTextColumn>
                </Columns>
            </mis:ASPxTreeList>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlAttributeCategory" runat="server" Style="display: block;" CssClass="modalWindow"
        DefaultButton="btAttributeCategoryOK">
        <asp:UpdatePanel ID="upnlAttributeCategory" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btAttributeCategoryPopup" runat="server" Style="display: none;" />
                <act:ModalPopupExtender ID="mpeAttributeCategory" TargetControlID="btAttributeCategoryPopup"
                    PopupControlID="pnlAttributeCategory" runat="server" Enabled="True" BackgroundCssClass="modalBackground"
                    DynamicServicePath="" />
                <div class="popup">
                    <div class="popup_fieldset">
                        <h2>
                            <asp:Label ID="lblCategoryContext" runat="server"></asp:Label></h2>
                        <asp:Label ID="lblAttributeCategoryError" runat="server" ForeColor="Red"></asp:Label>
                        <div class="title_separator"></div>
                        <div class="box">
                            <h3>
                                <asp:Label runat="server">კატეოგორიის სახელი</asp:Label></h3>
                            <div class="box_body">
                                <asp:TextBox ID="tbAttributeCategoryName" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="fieldsetforicons">
                        <div class="left">
                            <asp:LinkButton CssClass="icon" ID="btAttributeCategoryOK" runat="server" Text="შენახვა"
                                ToolTip="შენახვა" OnClick="btAttributeCategoryOK_Click" />
                        </div>
                        <div class="right">
                            <asp:LinkButton CssClass="icon" ID="btAttributeCategoryCancel" runat="server" Text="დახურვა"
                                ToolTip="დახურვა" />
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hdAttributeCategoryID" runat="server" />
                <asp:HiddenField ID="hdAttributeCategoryProjectID" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="pnlAttributeType" runat="server" Style="display: block;" CssClass="modalWindow"
        DefaultButton="btAttributeTypeOK">
        <asp:UpdatePanel ID="upnlAttributeTypes" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btAttributeTypePopup" runat="server" Style="display: none;" />
                <act:ModalPopupExtender ID="mpeAttributeType" TargetControlID="btAttributeTypePopup"
                    PopupControlID="pnlAttributeType" runat="server" Enabled="True" BackgroundCssClass="modalBackground"
                    DynamicServicePath="" />
                <asp:HiddenField ID="hdAttributeTypeID" runat="server" />
                <asp:HiddenField ID="hdAttributeTypeProjectID" runat="server" />
                <div class="popup">
                    <div class="popup_fieldset">
                        <h2>
                            <asp:Label ID="lblTypeContext" runat="server"></asp:Label>
                        </h2>
                        <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                        <div class="title_separator"></div>
                        <div class="box">
                            <h3>
                                <asp:Label runat="server">ტიპის სახელი</asp:Label>
                            </h3>
                            <div class="box_body">
                                <asp:TextBox ID="tbAttributeType" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="box">
                            <h3></h3>
                            <div class="box_body">
                            </div>
                        </div>
                    </div>
                    <div class="fieldsetforicons">
                        <div class="left">
                            <asp:LinkButton CssClass="icon" ID="btAttributeTypeOK" runat="server" ToolTip="შენახვა" Text="შენახვა" OnClick="btAttributeTypeOK_Click" />
                        </div>
                        <div class="right">
                            <asp:LinkButton CssClass="icon" ID="btAttributeTypeCancel" runat="server" ToolTip="დახურვა" Text="დახურვა" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
