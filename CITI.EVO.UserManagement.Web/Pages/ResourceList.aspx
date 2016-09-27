<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="ResourceList.aspx.cs" Inherits="Pages_ResourceList" %>
<%@ Register TagPrefix="mis" Namespace="CITI.EVO.Tools.Web.UI.Controls" Assembly="CITI.EVO.Tools" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">
	<asp:UpdatePanel runat="server" ID="upnlMain" UpdateMode="Always">
		<ContentTemplate>
			<h1>
				<asp:Label runat="server">რესურსების მართვა</asp:Label>
			</h1>
			<div class="page_title_separator"></div>
			<div class="fieldset">
				<div class="box">
					<h3>პროექტების ჩამონათვალი
					</h3>
					<div class="box_body_short">
						<mis:ASPxComboBox ID="cmbProject" TextField="Name" ValueField="ID" runat="server"
							AutoPostBack="true" OnSelectedIndexChanged="cmbProject_SelectedIndexChanged">
						</mis:ASPxComboBox>
						<asp:TextBox runat="server" ID="tbxKeyword"></asp:TextBox>
						<asp:LinkButton ID="btnSearch" runat="server" ToolTip="დამატება" CssClass="icon"
							Text="ძებნა" OnClick="btSearch_OnClick" />
					</div>
				</div>
				<asp:LinkButton ID="btNew" runat="server" ToolTip="დამატება" CssClass="icon"
					Text="რესურსის დამატება" OnClick="btNew_OnClick" />
				<dx:ASPxTreeList ID="tlResources" runat="server" AutoGenerateColumns="False"
					KeyFieldName="ID" ParentFieldName="ParentID" ClientIDMode="AutoID" Width="100%"
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
						<dx:TreeListHyperLinkColumn VisibleIndex="5">
							<DataCellTemplate>
								<mis:ImageLinkButton ID="lnkEdit" runat="server" Style="float: right;" ToolTip="რედაქტირება"
									DefaultImageUrl="~/App_Themes/default/images/edit.png" CommandArgument='<% #Eval("ID")%>'
									Visible='<%# EditEnabled(Eval("ID")) %>' OnCommand="lnkEdit_OnCommand" />
								<mis:ImageLinkButton ID="lnkDelete" runat="server" Style="float: right;" ToolTip="წაშლა"
									DefaultImageUrl="~/App_Themes/default/images/delete.png" OnClientClick="return confirm('გსურთ რესურსის წაშლა?');"
									CommandArgument='<% #Eval("ID")%>' OnCommand="lnkDelete_OnCommand" Visible='<%# DeleteEnabled(Eval("ID")) %>' />
								<mis:ImageLinkButton ID="lnkNew" runat="server" ToolTip="დამატება" Style="float: right;"
									DefaultImageUrl="~/App_Themes/default/images/add.png" CommandArgument='<% #Eval("ID")%>'
									Visible='<%# NewEnabled(Eval("ID")) %>' OnCommand="lnkNew_OnCommand" />
							</DataCellTemplate>
						</dx:TreeListHyperLinkColumn>
						<dx:TreeListTextColumn VisibleIndex="0">
							<HeaderCaptionTemplate>
								<asp:Label runat="server" Text="სახელი" />
							</HeaderCaptionTemplate>
							<DataCellTemplate>
								<asp:Label runat="server" Text='<%#Eval("Name") %>' />
							</DataCellTemplate>
						</dx:TreeListTextColumn>
						<dx:TreeListTextColumn VisibleIndex="1">
							<HeaderCaptionTemplate>
								<asp:Label runat="server" Text="მნიშვნელობა" />
							</HeaderCaptionTemplate>
							<DataCellTemplate>
								<asp:Label runat="server" Text='<%#Eval("Value") %>' />
							</DataCellTemplate>
						</dx:TreeListTextColumn>
						<dx:TreeListTextColumn VisibleIndex="2">
							<HeaderCaptionTemplate>
								<asp:Label runat="server" Text="აღწერა" />
							</HeaderCaptionTemplate>
							<DataCellTemplate>
								<asp:Label runat="server" Text='<%#Eval("Description") %>' />
							</DataCellTemplate>
						</dx:TreeListTextColumn>
						<dx:TreeListTextColumn VisibleIndex="3">
							<HeaderCaptionTemplate>
								<asp:Label runat="server" Text="ტიპი" />
							</HeaderCaptionTemplate>
							<DataCellTemplate>
								<asp:Label runat="server" Text='<%#Eval("Type") %>' />
							</DataCellTemplate>
						</dx:TreeListTextColumn>
					</Columns>
				</dx:ASPxTreeList>
			</div>
		</ContentTemplate>
	</asp:UpdatePanel>
	<asp:Panel ID="pnlResource" runat="server" Style="display: none;" CssClass="modalWindow"
		DefaultButton="btResourceOK">
		<asp:UpdatePanel ID="upnlResource" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<asp:Button ID="btResourcePopup" runat="server" Style="display: none;" />
				<act:ModalPopupExtender ID="mpeResource" runat="server" PopupControlID="pnlResource"
					BackgroundCssClass="modalBackground" TargetControlID="btResourcePopup" />
				<asp:HiddenField ID="hdResourceID" runat="server" />
				<%--<asp:HiddenField ID="hdResourceParentID" runat="server" />--%>
				<div class="popup">
					<div class="popup_fieldset">
						<h2>
							<asp:Label runat="server">რესურსის დამატება</asp:Label>
						</h2>
						<asp:Label ID="lblResourceError" runat="server" ForeColor="Red"></asp:Label>
						<div class="title_separator"></div>
						<div class="box">
							<h3>
								<asp:Label runat="server">რესურსის სახელი</asp:Label></h3>
							<div class="box_body">
								<asp:TextBox ID="tbResourceName" runat="server"></asp:TextBox>
							</div>
						</div>
						<div class="box">
							<h3>
								<asp:Label runat="server">რესურსის ტიპი</asp:Label></h3>
							<div class="box_body_short">
								<mis:ASPxComboBox ID="cmbResourceType" TextField="Key" ValueField="Value"
									runat="server">
								</mis:ASPxComboBox>
							</div>
						</div>
						<div class="box">
							<h3>
								<asp:Label runat="server">მნიშვნელობა</asp:Label></h3>
							<div class="box_body">
								<asp:TextBox ID="tbResourceValue" runat="server"></asp:TextBox>
							</div>
						</div>
					</div>
					<div class="fieldsetforicons">
						<div class="left">
							<asp:LinkButton ID="btResourceOK" CssClass="icon" runat="server" Text="შენახვა" ToolTip="შენახვა"
								OnCommand="btResourceOK_Click" />
						</div>
						<div class="right">
							<asp:LinkButton ID="btResourceCancel" CssClass="icon" runat="server" Text="დახურვა" ToolTip="დახურვა" />
						</div>
					</div>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
	</asp:Panel>
</asp:Content>
