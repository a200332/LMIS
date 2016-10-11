<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="LinksList.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.LinksList" %>

<%@ Register Src="~/Controls/Others/LinksControl.ascx" TagPrefix="lmis" TagName="LinksControl" %>
<%@ Register Src="~/Controls/Others/MainLinkControl.ascx" TagPrefix="lmis" TagName="MainLinkControl" %>
<%@ Register Src="~/Controls/Others/SubLinkControl.ascx" TagPrefix="lmis" TagName="SubLinkControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: left;">
        <ce:ImageLinkButton runat="server" ToolTip="Add New" DefaultImageUrl="~/App_Themes/Default/images/add.png" ID="btnAddNew" OnClick="btnAddNew_OnClick" />
    </div>
    <div>
        <lmis:LinksControl runat="server" ID="linksControl" OnAddChild="linksControl_OnAddChild" OnEditItem="linksControl_OnEditItem" OnDeleteItem="linksControl_OnDeleteItem" />
    </div>
    <div>
        <act:ModalPopupExtender runat="server" ID="mpeAddEdit" TargetControlID="btnAddEditFake"
            Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlAddEdit"
            CancelControlID="btnCancel" />
        <asp:Button runat="server" ID="btnAddEditFake" Style="display: none" />
        <asp:Panel runat="server" ID="pnlAddEdit">
            <div class="popup">
  
                <div class="popup_fieldset">

                    <div class="popup-title">Video</div>
                    <div class="title_separator"></div>
                    <div class="box">
                        <lmis:MainLinkControl runat="server" ID="mainLinkControl" Visible="False" />
                        <lmis:SubLinkControl runat="server" ID="subLinkControl" Visible="False" />
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

