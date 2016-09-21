<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="AttributesSchemasNodesList.aspx.cs" Inherits="Pages_AttributesSchemasNodesList" %>
<%@ Register TagPrefix="mis" Namespace="CITI.EVO.Tools.Web.UI.Controls" Assembly="CITI.EVO.Tools" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">
	<asp:UpdatePanel runat="server" ID="upnlMain" UpdateMode="Always" RenderMode="Block">
		<ContentTemplate>
			<h1>
				<asp:Label runat="server">ატრიბუტების სქემები</asp:Label>
			</h1>
			<div class="page_title_separator"></div>
			<mis:ASPxTreeList ID="tlAttributes" runat="server" AutoGenerateColumns="False" Width="100%"
				KeyFieldName="ID" ParentFieldName="ParentID" ClientIDMode="AutoID"
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
					<dx:TreeListHyperLinkColumn VisibleIndex="2" Width="3px">
						<DataCellTemplate>
							<mis:ImageLinkButton ID="lnkEdit" runat="server" ToolTip="რედაქტირება" DefaultImageUrl="~/App_Themes/default/images/edit.png"
								CommandArgument='<% #Eval("Key")%>' Visible='<%# GetEditVisible(Eval("Type")) %>' OnClick="lnkEdit_Click" />
						</DataCellTemplate>
					</dx:TreeListHyperLinkColumn>
					<dx:TreeListHyperLinkColumn VisibleIndex="3" Width="3px">
						<DataCellTemplate>
							<mis:ImageLinkButton ID="lnkDelete" runat="server" ToolTip="წაშლა" DefaultImageUrl="~/App_Themes/default/images/delete.png"
								CommandArgument='<% #Eval("Key")%>' Visible='<%# GetDeleteVisible(Eval("Type")) %>' OnClick="lnkDelete_Click" />
						</DataCellTemplate>
					</dx:TreeListHyperLinkColumn>
					<dx:TreeListHyperLinkColumn VisibleIndex="4" Width="3px">
						<DataCellTemplate>
							<mis:ImageLinkButton ID="lnkNew" runat="server" ToolTip="დამატება" DefaultImageUrl="~/App_Themes/default/images/add.png"
								CommandArgument='<% #Eval("Key")%>' Visible='<%# GetNewVisible(Eval("Type")) %>' OnClick="lnkNew_Click" />
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
				</Columns>

			</mis:ASPxTreeList>

		</ContentTemplate>
	</asp:UpdatePanel>
	<asp:Panel ID="pnlAttributeSchema" runat="server" Style="display: none;" CssClass="modalWindow"
		Width="333px" DefaultButton="btAttributeSchemaOK">
		<asp:UpdatePanel ID="upnlAttributeSchema" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<asp:Button ID="btAttributeSchemaPopup" runat="server" Style="display: none;" />
				<act:ModalPopupExtender ID="mpeAttributeSchema" TargetControlID="btAttributeSchemaPopup"
					PopupControlID="pnlAttributeSchema" runat="server" Enabled="True" BackgroundCssClass="modalBackground"
					DynamicServicePath="" />
				<div class="ModalWindow" style="width: 330px;">
					<asp:HiddenField ID="hdAttributeSchemaID" runat="server" />
					<asp:HiddenField ID="hdAttributeSchemaParentID" runat="server" />
					<div class="popup_centerpanelcell">
						<div class="popup_center_top">
							<div class="tctpop">
								<div class="bctpop">
									<div class="lctpop">
										<div class="rctpop">
											<div class="blctpop">
												<div class="brctpop">
													<div class="tlctpop">
														<div class="trctpop">
															<div class="popup_top_title">
																<asp:Label runat="server">ატრიბუტების სქემა</asp:Label>
															</div>
															<div>
																<asp:UpdateProgress ID="updAttributeSchemaProcess" runat="server" AssociatedUpdatePanelID="upnlAttributeSchema">
																	<ProgressTemplate>
																		<img alt="" src="../Images/small-waiting.gif" />
																	</ProgressTemplate>
																</asp:UpdateProgress>
																<asp:Label ID="lblAttributeSchemaError" runat="server" ForeColor="Red"></asp:Label>
															</div>
														</div>
													</div>
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
						<div class="tpop">
							<div class="bpop">
								<div class="lpop">
									<div class="rpop">
										<div class="blpop">
											<div class="brpop">
												<div class="tlpop">
													<div class="trpop">
														<div class="stationary_popup">
															<div class="popup">
																<div style="float: left;">
																	<div class="box_body_popup">
																		<div class="box_title_popup" style="color: #5D5D5D; font-size: 12px; width: 150px;">
																			<asp:Label runat="server">სახელი</asp:Label>
																		</div>
																		<div class="box_popup">
																			<asp:TextBox ID="tbAttributeSchemaName" runat="server"></asp:TextBox>
																		</div>
																	</div>
																	<div style="text-align: left; padding: 13px 0 2px 3px; float: left;">
																		<mis:ImageLinkButton ID="btAttributeSchemaOK" runat="server" Text="შენახვა"
																			ToolTip="შენახვა" DefaultImageUrl="~/App_Themes/default/images/add_icon.png" OnClick="btAttributeSchemaOK_Click" />
																	</div>
																	<div style="text-align: left; padding: 13px 0 2px 15px; float: left;">
																		<mis:ImageLinkButton ID="btAttributeSchemaCancel" runat="server" Text="დახურვა"
																			ToolTip="დახურვა" DefaultImageUrl="~/App_Themes/default/images/close_icon.png" />
																	</div>
																</div>
															</div>
														</div>
													</div>
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
	</asp:Panel>
	<asp:Panel ID="pnlAttributeSchemaNode" runat="server" Style="display: none;" CssClass="modalWindow"
		Width="333px" DefaultButton="btAttributeSchemaNodeOK">
		<asp:UpdatePanel ID="upnlAttributeSchemaNode" runat="server" UpdateMode="Conditional">
			<ContentTemplate>
				<asp:Button ID="btAttributeSchemaNodePopup" runat="server" Style="display: none;" />
				<act:ModalPopupExtender ID="mpeAttributeSchemaNode" TargetControlID="btAttributeSchemaNodePopup"
					PopupControlID="pnlAttributeSchemaNode" runat="server" Enabled="True" BackgroundCssClass="modalBackground"
					DynamicServicePath="" />
				<div class="ModalWindow" style="width: 330px;">
					<asp:HiddenField ID="hdAttributeSchemaNodeParentID" runat="server" />
					<asp:HiddenField ID="hdAttributeSchemaNodeID" runat="server" />
					<div class="popup_centerpanelcell">
						<div class="popup_center_top">
							<div class="tctpop">
								<div class="bctpop">
									<div class="lctpop">
										<div class="rctpop">
											<div class="blctpop">
												<div class="brctpop">
													<div class="tlctpop">
														<div class="trctpop">
															<div class="popup_top_title">
																<asp:Label runat="server">ატრიბუტების სქემის სახელი</asp:Label>
															</div>
															<div>
																<asp:UpdateProgress ID="updAttributeSchemaNode" runat="server" AssociatedUpdatePanelID="upnlAttributeSchemaNode">
																	<ProgressTemplate>
																		<img alt="" src="../Images/small-waiting.gif" />
																	</ProgressTemplate>
																</asp:UpdateProgress>
																<asp:Label ID="lblAttributeCategories" runat="server" ForeColor="Red"></asp:Label>
															</div>
														</div>
													</div>
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
						<div class="tpop">
							<div class="bpop">
								<div class="lpop">
									<div class="rpop">
										<div class="blpop">
											<div class="brpop">
												<div class="tlpop">
													<div class="trpop">
														<div class="stationary_popup">
															<div class="popup">
																<div style="float: left;">
																	<div class="box_body_popup">
																		<div class="box_title_popup" style="color: #5D5D5D; font-size: 12px; width: 150px;">
																			<asp:Label runat="server">სახელი</asp:Label>
																		</div>
																		<div class="box_popup">
																			<asp:TextBox ID="tbAttributeSchemaNodeName" Width="178px" runat="server"></asp:TextBox>
																		</div>
																	</div>
																	<div class="box_body_popup">
																		<div class="box_title_popup" style="color: #5D5D5D; font-size: 12px; width: 150px;">
																			<asp:Label runat="server">კატეგორია</asp:Label>
																		</div>
																		<div class="box_popup">
																			<mis:DropDownList ID="cmbAttributeCategories" CssClass="content_left_dropdown" ValueField="ID" Width="180px" TextField="Name" ValueType="System.Guid"
																				runat="server" />
																		</div>
																	</div>
																	<div class="box_body_popup">
																		<div class="box_title_popup" style="color: #5D5D5D; font-size: 12px; width: 150px;">
																			<asp:Label runat="server">ტიპი</asp:Label>
																		</div>
																		<div class="box_popup">
																			<mis:DropDownList ID="cmbAttributeTypes" CssClass="content_left_dropdown" ValueField="ID" Width="180px" TextField="Name" ValueType="System.Guid"
																				runat="server" />
																		</div>
																	</div>
																	<div style="text-align: left; padding: 13px 0 2px 3px; float: left;">
																		<mis:ImageLinkButton ID="btAttributeSchemaNodeOK" runat="server" Text="შენახვა"
																			ToolTip="შენახვა" DefaultImageUrl="~/App_Themes/default/images/add_icon.png" OnClick="btAttributeSchemaNodeOK_Click" />
																	</div>
																	<div style="text-align: left; padding: 13px 0 2px 15px; float: left;">
																		<mis:ImageLinkButton ID="btAttributeSchemaNodeCancel" runat="server" Text="დახურვა"
																			ToolTip="დახურვა" DefaultImageUrl="~/App_Themes/default/images/close_icon.png" />
																	</div>
																</div>
															</div>
														</div>
													</div>
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
	</asp:Panel>
</asp:Content>
