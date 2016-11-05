<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="ProjectsList.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.ProjectsList" %>


<%@ Register Src="~/Controls/Management/ProjectsControl.ascx" TagPrefix="lmis" TagName="ProjectsControl" %>
<%@ Register Src="~/Controls/Management/ProjectControl.ascx" TagPrefix="lmis" TagName="ProjectControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: left;">
        <ce:ImageLinkButton runat="server" ToolTip="Add New" DefaultImageUrl="~/App_Themes/Default/images/add.png" ID="btnAddNew" OnClick="btnAddNew_OnClick" />
    </div>
    <div>
        <lmis:ProjectsControl runat="server" ID="projectsControl" OnEditItem="projectsControl_OnEditItem" OnDeleteItem="projectsControl_OnDeleteItem" OnUpItem="projectsControl_OnUpItem" OnDownItem="projectsControl_OnDownItem" />
    </div>
    <div>
        <act:ModalPopupExtender runat="server" ID="mpeAddEdit" TargetControlID="btnAddEditFake"
            Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlAddEdit"
            CancelControlID="btnCancel" />
        <asp:Button runat="server" ID="btnAddEditFake" Style="display: none" />
        <asp:Panel runat="server" ID="pnlAddEdit">
            <div class="popup">
  
                <div class="popup_fieldset">

                    <div class="popup-title"><ce:Label runat="server">Project</ce:Label></div>
                    <div class="title_separator"></div>
                    <div class="box">
                        <lmis:ProjectControl runat="server" ID="projectControl" />
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


