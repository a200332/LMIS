<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UsersList.aspx.cs" Inherits="Pages_UsersList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">
	<h1>
		<asp:Label runat="server">მომხმარებლები</asp:Label>
	</h1>
	<div class="page_title_separator"></div>
	<asp:UpdatePanel runat="server" ID="upnlMain" UpdateMode="Always">
		<ContentTemplate>
			<h2>ფილტრები</h2>
			<div class="title_separator"></div>
			<div class="wrapper"></div>
			<div class="fieldset">
				<asp:Panel ID="pnlFilters" runat="server">
					<div>
						<asp:CheckBox ID="cbUserName" runat="server" Checked="false" /><asp:Label runat="server">მომხმარებელი</asp:Label>
						<asp:CheckBox ID="cbPassword" runat="server" Checked="false" /><asp:Label ID="Label2" runat="server">პაროლი</asp:Label>
						<asp:CheckBox ID="cbFirstNameFilter" runat="server" Checked="false" /><asp:Label ID="Label3" runat="server">სახელი</asp:Label>
						<asp:CheckBox ID="cbLastNameFilter" runat="server" Checked="false" /><asp:Label ID="Label4" runat="server">გვარი</asp:Label>
						<asp:CheckBox ID="cbEmail" runat="server" /><asp:Label ID="Label5" runat="server">ელ-ფოსტა</asp:Label>
						<asp:CheckBox ID="cbAddress" runat="server" /><asp:Label ID="Label6" runat="server">მისამართი</asp:Label>
					</div>
					<div class="wrapper"></div>

					<div class="left">
						<mis:DropDownList ID="ddlUserCategories" runat="server" Width="100" TextField="Name" ValueField="ID" />
					</div>
					<div class="left">
						<mis:DropDownList ID="ddlStatues" Width="100" runat="server">
							<Items>
								<asp:ListItem Text="ყველა" Value="-1" Selected="true" />
								<asp:ListItem Text="აქტიური" Value="true" />
								<asp:ListItem Text="პასიური" Value="false" />
							</Items>
						</mis:DropDownList>
					</div>

					<div>
						<asp:TextBox ID="txtVariousFilter" Width="100px" Height="27px" runat="server" />
					</div>

					<asp:LinkButton ID="btnBindData" runat="server" CssClass="icon" Text="ნახვა" OnClick="btnBindData_Click" />
					<asp:Label runat="server" Style="padding: 4px 0 0 7px;" ID="lblError" />

					<asp:LinkButton ID="btnAddUser" CssClass="icon" Text="მომხმარებლის დამატება" runat="server" OnClick="btnAddUser_Click" />
				</asp:Panel>

			</div>



			<mis:DataGrid ID="gvUsers" ClientInstanceName="gvUsers" runat="server" KeyFieldName="ID"
				Width="100%" EnableRowsCache="False">
				<Columns>
					<dx:GridViewDataColumn VisibleIndex="11" Width="3px" Name="Edit">
						<DataItemTemplate>
							<mis:ImageLinkButton ID="lnkEdit" runat="server" DefaultImageUrl="~/App_Themes/default/images/edit.png"
								ToolTip="რედაქტირება" CommandArgument='<% #Eval("ID")%>' OnClick="lnkEdit_Click" />
						</DataItemTemplate>
					</dx:GridViewDataColumn>
					<dx:GridViewDataColumn VisibleIndex="12" Width="3px" Name="View">
						<DataItemTemplate>
							<mis:ImageLinkButton ID="lnkView" runat="server" DefaultImageUrl="~/App_Themes/default/images/view.png"
								ToolTip="ნახვა" CommandArgument='<% #Eval("ID")%>' OnClick="lnkView_Click" />
						</DataItemTemplate>
					</dx:GridViewDataColumn>
					<dx:GridViewDataColumn VisibleIndex="15" Width="3px" Name="AddMessage">
						<DataItemTemplate>
							<mis:ImageLinkButton ID="lnkAddMessage" runat="server"
								DefaultImageUrl="~/App_Themes/default/images/add_message.png" ToolTip="მესიჯის დამატება"
								CommandArgument='<% #Eval("ID")%>' OnClick="lnkAddMessage_Click" />
						</DataItemTemplate>
					</dx:GridViewDataColumn>
					<dx:GridViewDataColumn VisibleIndex="13" Width="3px" Name="Delete">
						<DataItemTemplate>
							<mis:ImageLinkButton ID="lnkDelete" runat="server" DefaultImageUrl="~/App_Themes/default/images/delete.png"
								ToolTip="წაშლა" OnClientClick="return confirm('გსურთ მომხმარებილის წაშლა?');"
								CommandArgument='<% #Eval("ID")%>' OnClick="lnkDelete_Click" />
						</DataItemTemplate>
					</dx:GridViewDataColumn>
					<dx:GridViewDataColumn VisibleIndex="14" Width="3px" Name="AddAttributes">
						<DataItemTemplate>
							<mis:ImageLinkButton CommandName="AddAttributes" ID="lnkAttributes" runat="server"
								DefaultImageUrl="~/App_Themes/default/images/add.png" ToolTip="ატრიბუტების დამატება"
								CommandArgument='<% #Eval("ID")%>' OnClick="lnkAttributes_Click" />
						</DataItemTemplate>
					</dx:GridViewDataColumn>
					<dx:GridViewDataColumn VisibleIndex="15" Width="3px" Name="ShowAttributes">
						<DataItemTemplate>
							<mis:ImageLinkButton CommandName="ShowAttributes" ID="lnkShowAttributes" runat="server"
								DefaultImageUrl="~/App_Themes/default/images/view.png" ToolTip="ატრიბუტების ნახვა"
								CommandArgument='<% #Eval("ID")%>' OnClick="lnkAttributes_Click" />
						</DataItemTemplate>
					</dx:GridViewDataColumn>
					<dx:GridViewDataColumn Caption="ID" FieldName="ID" VisibleIndex="1" Visible="false" />
					<dx:GridViewDataColumn FieldName="LoginName" VisibleIndex="2">
						<HeaderCaptionTemplate>
							<asp:Label runat="server">მომხმარებელი</asp:Label>
						</HeaderCaptionTemplate>
					</dx:GridViewDataColumn>
					<dx:GridViewDataColumn FieldName="Password" VisibleIndex="3">
						<HeaderCaptionTemplate>
							<asp:Label runat="server">პაროლი</asp:Label>
						</HeaderCaptionTemplate>
					</dx:GridViewDataColumn>
					<dx:GridViewDataColumn Caption="სახელი" FieldName="FirstName" VisibleIndex="4">
						<HeaderCaptionTemplate>
							<asp:Label runat="server">სახელი</asp:Label>
						</HeaderCaptionTemplate>
					</dx:GridViewDataColumn>
					<dx:GridViewDataColumn FieldName="LastName" VisibleIndex="5">
						<HeaderCaptionTemplate>
							<asp:Label runat="server">გვარი</asp:Label>
						</HeaderCaptionTemplate>
					</dx:GridViewDataColumn>
					<dx:GridViewDataColumn FieldName="IsActive" VisibleIndex="6">
						<HeaderCaptionTemplate>
							<asp:Label runat="server">აქტივაცია</asp:Label>
						</HeaderCaptionTemplate>
					</dx:GridViewDataColumn>
					<dx:GridViewDataColumn FieldName="Email" VisibleIndex="7">
						<HeaderCaptionTemplate>
							<asp:Label runat="server">მეილი</asp:Label>
						</HeaderCaptionTemplate>
					</dx:GridViewDataColumn>
					<dx:GridViewDataColumn FieldName="Address" VisibleIndex="8">
						<HeaderCaptionTemplate>
							<asp:Label runat="server">მისამართი</asp:Label>
						</HeaderCaptionTemplate>
					</dx:GridViewDataColumn>
					<dx:GridViewDataColumn FieldName="PasswordExpirationDate" VisibleIndex="9">
						<HeaderCaptionTemplate>
							<asp:Label ID="Label9" runat="server">პაროლის ვალიდურობის თარიღი</asp:Label>
						</HeaderCaptionTemplate>
					</dx:GridViewDataColumn>
					<dx:GridViewDataColumn Caption="კატეგორია" FieldName="UserCategoryName" VisibleIndex="10">
						<HeaderCaptionTemplate>
							<asp:Label runat="server">კატეგორია</asp:Label>
						</HeaderCaptionTemplate>
					</dx:GridViewDataColumn>
					<dx:GridViewDataColumn Caption="პიროვნების პირადი ნომერი" FieldName="PersonPersonalID" VisibleIndex="11">
						<HeaderCaptionTemplate>
							<asp:Label runat="server">პიროვნების პირადი ნომერი</asp:Label>
						</HeaderCaptionTemplate>
					</dx:GridViewDataColumn>
					<dx:GridViewDataColumn Caption="პიროვნების სახელი" FieldName="PersonFirstName" VisibleIndex="11">
						<HeaderCaptionTemplate>
							<asp:Label runat="server">პიროვნების სახელი</asp:Label>
						</HeaderCaptionTemplate>
					</dx:GridViewDataColumn>
					<dx:GridViewDataColumn Caption="პიროვნების გვარი" FieldName="PersonLastName" VisibleIndex="11">
						<HeaderCaptionTemplate>
							<asp:Label runat="server">პიროვნების გვარი</asp:Label>
						</HeaderCaptionTemplate>
					</dx:GridViewDataColumn>
					<dx:GridViewDataColumn Caption="ორგანიზაცია" FieldName="BranchesName" VisibleIndex="11">
						<HeaderCaptionTemplate>
							<asp:Label runat="server">ორგანიზაცია</asp:Label>
						</HeaderCaptionTemplate>
					</dx:GridViewDataColumn>
				</Columns>
			</mis:DataGrid>

		</ContentTemplate>
	</asp:UpdatePanel>
	<asp:Panel ID="PanelUsers" runat="server" Style="display: none" DefaultButton="btProjectOK">
		<asp:UpdatePanel ID="UpdatePanelUsers" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<asp:Button ID="btUserPopup" runat="server" Style="display: none;" />
				<act:ModalPopupExtender ID="mpeUserForm" runat="server" PopupControlID="PanelUsers"
					BackgroundCssClass="modalBackground" TargetControlID="btUserPopup" />

				<div class="popup">
					<div class="popup_fieldset">
						<h2>
							<asp:Label ID="lblUserContext" runat="server"></asp:Label>
						</h2>
						<div class="title_separator"></div>
						<div class="box">
							<h3>
								<asp:Label runat="server">მომხმარებლის სახელი</asp:Label>
							</h3>
							<div class="box_body">
								<asp:TextBox ID="tbLoginName" runat="server" />
							</div>
						</div>
						<div class="box">
							<h3>
								<asp:Label runat="server">პაროლი</asp:Label>
							</h3>
							<div class="box_body">
								<asp:TextBox ID="tbPassword" runat="server" />
							</div>
						</div>
						<div class="box">
							<asp:CheckBox ID="chkChangePassword" runat="server" AutoPostBack="true" OnCheckedChanged="chkChangePassword_CheckChanged" />
						</div>
						<div class="box">
							<h3>
								<asp:Label runat="server">სახელი</asp:Label>
							</h3>
							<div class="box_body">
								<asp:TextBox ID="tbFirstName" runat="server" />
							</div>
						</div>
						<div class="box">
							<h3>
								<asp:Label runat="server">გვარი</asp:Label>
							</h3>
							<div class="box_body">

								<asp:TextBox ID="tbLastName" runat="server" />
							</div>
						</div>
						<div class="box">
							<h3>
								<asp:Label runat="server">ელ-ფოსტა</asp:Label>
							</h3>
							<div class="box_body">
								<asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
							</div>
						</div>
						<div class="box">
							<h3>
								<asp:Label runat="server">მისამართი</asp:Label>
							</h3>
							<div class="box_body">
								<asp:TextBox ID="tbAddress" runat="server" />
							</div>
						</div>
						<div class="box">
							<h3>
								<asp:Label runat="server">აქტივაცია</asp:Label>
							</h3>
							<div class="box_body">
								<asp:CheckBox ID="chkActivate" runat="server" />
							</div>
						</div>
						<div class="box">
							<h3>
								<asp:Label runat="server">ვალიდურობის თარიღი</asp:Label>
							</h3>
							<div class="box_body">
								<asp:TextBox ID="tbPasswordExpirationDate" runat="server" />
								<act:CalendarExtender runat="server" ID="tbxNotificationEndDateExtender" TargetControlID="tbPasswordExpirationDate"
									Format="dd.MM.yyyy" CssClass="ajaxCalendarClass" />
							</div>
						</div>
						<asp:Panel runat="server" ID="pnlAccessLevel">
							<div class="box">
								<h3>
									<asp:Label runat="server">უფლებები</asp:Label>
								</h3>
								<div class="box_body_short">
									<mis:DropDownList ID="ddlAccessLevels" runat="server">
										<Items>
											<asp:ListItem Text="სტანდარტული" Value="0" />
											<asp:ListItem Text="ადმინისტრატორი" Value="1" />
										</Items>
									</mis:DropDownList>
								</div>
							</div>
						</asp:Panel>
						<div class="wrapper"></div>
						<div class="popup_fieldset">
							<mis:TreeView ID="tlGroups" runat="server" Width="600"
								KeyFieldName="ID" ParentFieldName="ParentID" TextFieldName="Name">
							</mis:TreeView>
						</div>
					</div>
					<div class="fieldsetforicons">
						<div class="left">
							<asp:LinkButton ID="btProjectOK" CssClass="icon"
								Text="შენახვა" ToolTip="შენახვა" runat="server" OnClick="btnSave_Click" />

						</div>
						<div class="right">

							<asp:LinkButton ID="btProjectCancel" CssClass="icon"
								Text="დახურვა" ToolTip="დახურვა" runat="server" />

						</div>
					</div>

				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
	</asp:Panel>
	<asp:Panel ID="PanelUserview" runat="server" Style="display: none" DefaultButton="btUserViewCancel">
		<asp:UpdatePanel ID="UpdatePanelUserView" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<asp:Button ID="btUserViewPopup" runat="server" Style="display: none;" />
				<act:ModalPopupExtender ID="mpeUserView" runat="server" PopupControlID="PanelUserview"
					BackgroundCssClass="modalBackground" TargetControlID="btUserViewPopup" />
				<div class="popup">
					<div class="popup_fieldset">

						<asp:HiddenField ID="HiddenField1" runat="server" />

						<asp:Label ID="lblUserViewContext" runat="server"></asp:Label>


						<asp:Label runat="server">მომხმარებლის სახელი</asp:Label>

						<asp:Label ID="lbLoginName" runat="server" />

						<asp:Label runat="server">სახელი</asp:Label>

						<asp:Label ID="lbFirstName" runat="server" />

						<asp:Label runat="server">გვარი</asp:Label>

						<asp:Label ID="lbLastName" runat="server" />

						<asp:Label runat="server">ელ-ფოსტა</asp:Label>

						<asp:Label ID="lbEmail" runat="server"></asp:Label>


						<asp:Label runat="server">მისამართი</asp:Label>

						<asp:Label ID="lbAddress" runat="server" />

						<asp:Label runat="server">სტატუსი</asp:Label>

						<asp:CheckBox ID="chkStatus" runat="server" Enabled="False" />

						<asp:Label ID="Label11" runat="server">პაროლი ვალიდურია</asp:Label>

						<asp:Label ID="lbPasswordExpirationDate" runat="server" />
						<asp:Label ID="Label12" runat="server">-მდე</asp:Label>

						<asp:Label runat="server">ჯგუფები</asp:Label>

						<mis:ListBox runat="server" ID="lstUserGroups" Width="500" Height="150">
						</mis:ListBox>

						<mis:ImageLinkButton ID="btUserViewCancel" DefaultImageUrl="~/App_Themes/default/images/close_icon.png"
							Text="დახურვა" ToolTip="დახურვა" runat="server" />
					</div>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
	</asp:Panel>
	<asp:Panel ID="pnlUserAttributes" runat="server" Style="display: none;"
		DefaultButton="btUserAttributesOK">
		<asp:UpdatePanel ID="upnlUserAttributes" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<asp:Button ID="btUserAttributesPopup" runat="server" Style="display: none;" />
				<act:ModalPopupExtender ID="mpeUserAttributes" TargetControlID="btUserAttributesPopup"
					PopupControlID="pnlUserAttributes" runat="server" Enabled="True" BackgroundCssClass="modalBackground" />

				<asp:HiddenField ID="hdUserIDtest" runat="server" />

				<div class="popup">
					<div class="popup_fieldset">
						<h2>
							<asp:Label runat="server">ატრიბუტის დამატება</asp:Label>
						</h2>
						<asp:Label ID="lblUserAttributesError" runat="server" ForeColor="Red"></asp:Label>
						<div class="title_separator"></div>
						<div class="box">
							<h3>
								<asp:Label runat="server">პროექტი</asp:Label>
							</h3>
							<div class="box_body_short">
								<mis:DropDownList ID="cmbProject" TextField="Name"
									ValueField="ID" AppendDataBoundItems="true" runat="server"
									AutoPostBack="true" OnSelectedIndexChanged="cmbProject_SelectedIndexChanged">
									<Items>
										<asp:ListItem Text="-- select project --" Value="" />
									</Items>
								</mis:DropDownList>
							</div>
						</div>
						<div class="box">
							<h3>
								<asp:Label runat="server">ატრიბუტების სქემა</asp:Label></h3>
							<div class="box_body_short">
								<mis:DropDownList ID="cmbAttributeSchemas" TextField="Name"
									ValueField="ID" Width="180px" Enabled="false" AppendDataBoundItems="true"
									runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbAttributeSchemas_SelectedIndexChanged">
									<Items>
										<asp:ListItem Text="-- select schema --" Value="" />
									</Items>
								</mis:DropDownList>
							</div>
						</div>
						<div class="box">
							<h3>
								<asp:Label runat="server">ატრიბუტების სახელები</asp:Label></h3>
							<div class="box_body_short">
								<mis:DropDownList ID="cmbAttributeSchemaNodes" AppendDataBoundItems="true"
									TextField="Name" OnSelectedIndexChanged="cmbAttributeSchemaNodes_SelectedIndexChanged"
									AutoPostBack="true" Width="180px" ValueField="ID" runat="server" Enabled="false">
									<Items>
										<asp:ListItem Text="-- select node --" Value="" />
									</Items>
								</mis:DropDownList>
							</div>
						</div>
						<div class="box">
							<h3>
								<asp:Label runat="server">მნიშვნელობა</asp:Label></h3>
							<div class="box_body">
								<asp:TextBox ID="tbUserAttributesValue" runat="server"></asp:TextBox>
							</div>
						</div>
					</div>
					<div class="fieldsetforicons">
						<div class="left">
							<asp:LinkButton CssClass="icon" runat="server" ID="btUserAttributesOK"
								Text="შენახვა" ToolTip="შენახვა" OnClick="btUserAttributesOK_Click" />
						</div>
						<div class="right">
							<asp:LinkButton CssClass="icon" ID="btUserAttributesCancel"
								Text="დახურვა" ToolTip="დახურვა" runat="server" />
						</div>
					</div>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
	</asp:Panel>
	<asp:Panel ID="pnlShowUserAttributes" runat="server" Style="display: none;" CssClass="modalWindow"
		DefaultButton="btShowUserAttributesCancel">
		<asp:UpdatePanel ID="upnlShowUserAttributes" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<asp:Button ID="btShowUserAttributesPopup" runat="server" Style="display: none;" />
				<act:ModalPopupExtender ID="mpeShowUserAttributes" TargetControlID="btShowUserAttributesPopup"
					PopupControlID="pnlShowUserAttributes" runat="server" Enabled="True" BackgroundCssClass="modalBackground" />
				<div class="popup">
					<div class="popup_fieldset">
						<h2>
							<asp:Label runat="server">ატრიბუტების ნახვა</asp:Label>
						</h2>
						<div class="title_separator"></div>
						<div class="box">
							<h3>
								<asp:Label runat="server">პროექტი</asp:Label>
							</h3>
							<div class="box_body_short">
								<mis:DropDownList ID="cmbShowProjects" TextField="Name"
									ValueField="ID" AppendDataBoundItems="true" runat="server"
									AutoPostBack="true" OnSelectedIndexChanged="cmbProject_SelectedIndexChanged">
									<Items>
										<asp:ListItem Text="-- select project --" Value="" />
									</Items>
								</mis:DropDownList>
							</div>
						</div>
						<div class="box">
							<h3>
								<asp:Label runat="server">ატრიბუტების სქემა</asp:Label>
							</h3>
							<div class="box_body_short">
								<mis:DropDownList ID="cmbShowAttributeSchemas" TextField="Name"
									ValueField="ID" Enabled="false" AppendDataBoundItems="true"
									runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbAttributeSchemas_SelectedIndexChanged">
									<Items>
										<asp:ListItem Text="-- select schema --" Value="" />
									</Items>
								</mis:DropDownList>
							</div>
						</div>
						<div class="box">
							<h3>
								<asp:Label runat="server">მნიშვნელობები</asp:Label>
							</h3>
							<div class="box_body">
								<mis:DataGrid ID="dwAttributeSchemaNodes" Width="340px" runat="server" EnableViewState="false">
									<Columns>
										<asp:BoundField DataField="Name" HeaderText="სახელი">
										</asp:BoundField>
										<asp:BoundField DataField="Value" HeaderText="მნიშვნელიბა">
										</asp:BoundField>
									</Columns>
								</mis:DataGrid>
							</div>
						</div>

					</div>
					<div class="fieldsetforicons">
						<div class="left">
							<asp:LinkButton ID="btShowUserAttributesCancel" CssClass="icon" Text="დახურვა" ToolTip="დახურვა" runat="server" />
						</div>
					</div>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
	</asp:Panel>

	<mis:MessageControl ID="ucMessage" runat="server" />
</asp:Content>
