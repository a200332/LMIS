<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="SurveysList.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.SurveysList" %>


<%@ Register Src="~/Controls/Others/SurveysControl.ascx" TagPrefix="lmis" TagName="SurveysControl" %>
<%@ Register Src="~/Controls/Others/SurveyControl.ascx" TagPrefix="lmis" TagName="SurveyControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: left;">
        <ce:ImageLinkButton runat="server" ToolTip="Add New" DefaultImageUrl="~/App_Themes/Default/images/add.png" ID="btnAddNew" OnClick="btnAddNew_OnClick" />
    </div>
    <div>
        <lmis:SurveysControl runat="server" ID="surveysControl" OnEditItem="surveysControl_OnEditItem" OnDeleteItem="surveysControl_OnDeleteItem" OnUpItem="surveysControl_OnUpItem" OnDownItem="surveysControl_OnDownItem" />
    </div>
    <div>
        <act:ModalPopupExtender runat="server" ID="mpeAddEdit" TargetControlID="btnAddEditFake"
            Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlAddEdit"
            CancelControlID="btnCancel" />
        <asp:Button runat="server" ID="btnAddEditFake" Style="display: none" />
        <asp:Panel runat="server" ID="pnlAddEdit">
            <div class="popup">
  
                <div class="popup_fieldset">

                    <div class="popup-title"><ce:Label runat="server">Survey</ce:Label></div>
                    <div class="title_separator"></div>
                    <div class="box">
                        <lmis:SurveyControl runat="server" ID="surveyControl" />
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


