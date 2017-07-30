<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="CategoriesList.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.CategoriesList" %>

<%@ Register Src="~/Controls/Management/CategoriesControl.ascx" TagPrefix="lmis" TagName="CategoriesControl" %>
<%@ Register Src="~/Controls/Management/CategoryControl.ascx" TagPrefix="lmis" TagName="CategoryControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: left;">
        <ce:ImageLinkButton runat="server" ToolTip="Add Category" DefaultImageUrl="~/App_Themes/Default/images/add.png" ID="btnAddNew" OnClick="btnAddCategory_OnClick" />
    </div>
    <div>
        <lmis:CategoriesControl runat="server" ID="categoriesControl" OnAddChild="categoriesControl_OnAddChild" OnEditItem="categoriesControl_OnEditItem" OnDeleteItem="categoriesControl_OnDeleteItem" OnUpItem="categoriesControl_OnUpItem" OnDownItem="categoriesControl_OnDownItem" />
    </div>
    <div>
        <act:ModalPopupExtender runat="server" ID="mpeAddEditCategory" TargetControlID="btnAddEditCategoryFake"
            Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlAddEditCategory"
            CancelControlID="btnCancelCategory" />
        <asp:Button runat="server" ID="btnAddEditCategoryFake" Style="display: none" />
        <asp:Panel runat="server" ID="pnlAddEditCategory">
            <div class="popup">

                <div class="popup_fieldset">

                    <div class="popup-title">
                        <ce:Label runat="server">Choose</ce:Label>
                    </div>
                    <div class="title_separator"></div>
                    <div class="box">
                        <div>
                            <asp:Label runat="server" ID="lblErrorMessage" ForeColor="Red"></asp:Label>
                        </div>
                        <div>
                            <lmis:CategoryControl runat="server" ID="categoryControl" />
                        </div>
                    </div>
                </div>
                <div class="fieldsetforicons">
                    <div class="left" style="margin-right: 10px;">
                        <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnSaveCatevory" ToolTip="დამატება" OnClick="btnSaveCatevory_OnClick" />
                    </div>
                    <div class="left">
                        <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close.png" ID="btnCancelCategory" ToolTip="დახურვა" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

