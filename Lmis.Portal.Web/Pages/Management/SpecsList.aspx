<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="SpecsList.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.SpecsList" %>

<%@ Register Src="~/Controls/Management/SpecTreeControl.ascx" TagPrefix="lmis" TagName="SpecTreeControl" %>
<%@ Register Src="~/Controls/Management/SpecCategoryControl.ascx" TagPrefix="lmis" TagName="SpecCategoryControl" %>
<%@ Register Src="~/Controls/Management/SpecDataControl.ascx" TagPrefix="lmis" TagName="SpecDataControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: left;">
        <ce:ImageLinkButton runat="server" ToolTip="Add Item" DefaultImageUrl="~/App_Themes/Default/images/add.png" ID="btnAddNew" OnClick="btnAddSpec_OnClick" />
    </div>
    <div>
        <lmis:SpecTreeControl runat="server" ID="specTreeControl" OnAddChild="specTreeControl_OnAddChild" OnEditItem="specTreeControl_OnEditItem" OnDeleteItem="specTreeControl_OnDeleteItem" OnUpItem="specTreeControl_OnUpItem" OnDownItem="specTreeControl_OnDownItem" />
    </div>
    <div>
        <act:ModalPopupExtender runat="server" ID="mpeAddEditSpec" TargetControlID="btnAddEditSpecFake"
            Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlAddEditSpec"
            CancelControlID="btnCancelSpec" />
        <asp:Button runat="server" ID="btnAddEditSpecFake" Style="display: none" />
        <asp:Panel runat="server" ID="pnlAddEditSpec">
            <div class="popup">

                <div class="popup_fieldset">

                    <div class="popup-title">
                        <ce:Label runat="server">Category</ce:Label></div>
                    <div class="title_separator"></div>
                    <div class="box">
                        <ce:RadioButtonList runat="server" ID="rbType" OnSelectedIndexChanged="rbType_OnSelectedIndexChanged" AutoPostBack="True" RepeatDirection="Horizontal">
                            <Items>
                                <asp:ListItem Text="Category" Value="Category" />
                                <asp:ListItem Text="Data" Value="Data" />
                            </Items>
                        </ce:RadioButtonList>

                        <lmis:SpecCategoryControl runat="server" ID="specCategoryControl" Visible="True" />
                        <lmis:SpecDataControl runat="server" ID="specDataControl" Visible="False" />
                    </div>
                </div>
                <div class="fieldsetforicons">
                    <div class="left" style="margin-right: 10px;">
                        <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnSaveSpec" ToolTip="დამატება" OnClick="btnSaveSpec_OnClick" />
                    </div>
                    <div class="left">
                        <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close.png" ID="btnCancelSpec" ToolTip="დახურვა" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

