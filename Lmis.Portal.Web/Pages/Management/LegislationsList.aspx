<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin.master" CodeFile="LegislationsList.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.LegislationsList" %>

<%@ Register Src="~/Controls/Others/LegislationsControl.ascx" TagPrefix="lmis" TagName="LegislationsControl" %>
<%@ Register Src="~/Controls/Others/MainLegislationControl.ascx" TagPrefix="lmis" TagName="MainLegislationControl" %>
<%@ Register Src="~/Controls/Others/SubLegislationControl.ascx" TagPrefix="lmis" TagName="SubLegislationControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: left;">
        <ce:ImageLinkButton runat="server" ToolTip="Add New" DefaultImageUrl="~/App_Themes/Default/images/add.png" ID="btnAddNew" OnClick="btnAddNew_OnClick" />
    </div>
    <div>
        <lmis:LegislationsControl runat="server" ID="legislationsControl" OnEditItem="legislationsControl_OnEditItem" OnDeleteItem="legislationsControl_OnDeleteItem" OnUpItem="legislationsControl_OnUpItem" OnDownItem="legislationsControl_OnDownItem" OnAddChild="legislationsControl_OnAddChild" />
    </div>
    <div>
        <act:ModalPopupExtender runat="server" ID="mpeAddEdit" TargetControlID="btnAddEditFake"
            Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlAddEdit"
            CancelControlID="btnCancel" />
        <asp:Button runat="server" ID="btnAddEditFake" Style="display: none" />
        <asp:Panel runat="server" ID="pnlAddEdit">
            <div class="popup">
                <div class="popup_fieldset">
                    <div class="popup-title">
                        <ce:Label runat="server">Choose</ce:Label></div>
                    <div class="title_separator"></div>
                    <div class="box">
                        <lmis:MainLegislationControl runat="server" ID="mainLegislationControl" />
                        <lmis:SubLegislationControl runat="server" ID="subLegislationControl" />
                    </div>
                </div>
                <div class="fieldsetforicons">
                    <div class="left" style="margin-right: 10px;">
                        <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnSave" ToolTip="დამატება" OnClick="btnSave_OnClick" />
                    </div>
                    <div class="left">
                        <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close.png" ID="btnCancel" ToolTip="დახურვა" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
