<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="CategoriesList.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.CategoriesList" %>

<%@ Register Src="~/Controls/Categories/CategoriesControl.ascx" TagPrefix="lmis" TagName="CategoriesControl" %>
<%@ Register Src="~/Controls/Categories/CategoryControl.ascx" TagPrefix="lmis" TagName="CategoryControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div>
		<ce:ImageLinkButton runat="server" ToolTip="Add Category" DefaultImageUrl="~/App_Themes/Default/images/add.png" ID="btnAddNew" OnClick="btnAddCategory_OnClick" />
	</div>
	<div>
		<lmis:CategoriesControl runat="server" ID="categoriesControl" OnAddChildCategory="categoriesControl_OnAddChildCategory" OnEditCategory="categoriesControl_OnEditCategory" OnDeleteCategory="categoriesControl_OnDeleteCategory" />
	</div>
	<div>
		<act:ModalPopupExtender runat="server" ID="mpeAddEditCategory" TargetControlID="btnAddEditCategoryFake"
			Enabled="true" BackgroundCssClass="modalBackground" PopupControlID="pnlAddEditCategory"
			CancelControlID="btnCancelCategory" />
		<asp:Button runat="server" ID="btnAddEditCategoryFake" Style="display: none" />
		<asp:Panel runat="server" ID="pnlAddEditCategory">
			<div class="popup">
				<div>
				</div>
				<div class="popup_fieldset">
					<h2>ალერგია</h2>
					<div class="title_separator"></div>
					<div class="box">
						<h3>ალერგია</h3>
					</div>
					<div class="box">
						<lmis:CategoryControl runat="server" ID="categoryControl" />
					</div>
				</div>
				<div class="fieldsetforicons">
					<div class="left">
						<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnSaveCatevory" ToolTip="დამატება" OnClick="btnSaveCatevory_OnClick" />
					</div>
					<div class="right">
						<ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/close.png" ID="btnCancelCategory" ToolTip="დახურვა" />
					</div>
				</div>
			</div>
		</asp:Panel>
	</div>
</asp:Content>

