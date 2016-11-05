<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="VideosList.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.VideosList" %>

<%@ Register Src="~/Controls/Management/VideosControl.ascx" TagPrefix="lmis" TagName="VideosControl" %>
<%@ Register Src="~/Controls/Management/VideoControl.ascx" TagPrefix="lmis" TagName="VideoControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: left;">
        <ce:ImageLinkButton runat="server" ToolTip="Add New" DefaultImageUrl="~/App_Themes/Default/images/add.png" ID="btnAddNew" OnClick="btnAddNew_OnClick" />
    </div>
    <div>
        <lmis:VideosControl runat="server" ID="videosControl" OnEditItem="videosControl_OnEditItem" OnDeleteItem="videosControl_OnDeleteItem" />
    </div>
    <div>
        <act:ModalPopupExtender runat="server" ID="mpeAddEdit" TargetControlID="btnAddEditFake"
            Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlAddEdit"
            CancelControlID="btnCancel" />
        <asp:Button runat="server" ID="btnAddEditFake" Style="display: none" />
        <asp:Panel runat="server" ID="pnlAddEdit">
            <div class="popup">
  
                <div class="popup_fieldset">

                    <div class="popup-title"><ce:Label runat="server">Video</ce:Label></div>
                    <div class="title_separator"></div>
                    <div class="box">
                        <lmis:VideoControl runat="server" ID="videoControl" />
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

