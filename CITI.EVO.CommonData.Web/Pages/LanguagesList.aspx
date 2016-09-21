<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LanguagesList.aspx.cs" Inherits="Pages_LanguagesList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
		<asp:ScriptManager runat="server" ID="ScriptManager1" />
		<asp:UpdatePanel runat="server" ID="upnlMain" UpdateMode="Always">
			<ContentTemplate>
				<div>
					<asp:Button runat="server" ID="btAddLanguage" Text="Add Language" OnClick="btAddLanguage_OnClick" />
				</div>
				<div>
					<mis:DataGrid ID="gvLanguages" runat="server" Width="100%">
						<columns>
	                        <asp:BoundField HeaderText="Display Name" DataField="DisplayName"/>
                            <asp:BoundField HeaderText="English Name" DataField="EngName"/>
                            <asp:BoundField HeaderText="Native Name" DataField="NativeName"/>
                            <asp:BoundField HeaderText="Pair" DataField="Pair"/>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" CommandArgument='<%# Eval("ID") %>' OnClick="lnkEditLang_Click">[Edit]</asp:LinkButton>
									<asp:LinkButton runat="server" CommandArgument='<%# Eval("ID") %>' OnClick="lnkDeleteLang_Click">[Delete]</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </columns>
					</mis:DataGrid>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
		<asp:UpdatePanel runat="server" UpdateMode="Always" ID="upnlAddEdit">
			<ContentTemplate>
				<asp:Button runat="server" ID="btShowAddEditPopup" Style="display: none;" />
				<act:ModalPopupExtender runat="server" TargetControlID="btShowAddEditPopup" PopupControlID="pnlAddEdit"
					ID="mpeAddEdit" />
				<asp:Panel runat="server" ID="pnlAddEdit" Style="padding: 10px; font-family: Trebuchet MS, Verdana, Tahoma; background-color: white; font-size: 12px;">
					<div>
						<table>
							<tr>
								<td>System Known Languages:
								</td>
								<td>
									<asp:DropDownList runat="server" ID="ddlSystemLanguages" DataTextField="DisplayName" DataValueField="Name" AutoPostBack="True" OnSelectedIndexChanged="ddlSystemLanguages_OnSelectedIndexChanged"></asp:DropDownList>
								</td>
							</tr>
						</table>
					</div>
					<div>
						<table>
							<tr>
								<td colspan="2" align="center">
									<asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
								</td>
							</tr>
							<tr>
								<td>Display Name:
								</td>
								<td>
									<asp:TextBox runat="server" ID="tbDisplayName" ReadOnly="True" Width="680px"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td>English Name:
								</td>
								<td>
									<asp:TextBox runat="server" ID="tbEngName" ReadOnly="True" Width="680px"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td>Native Name:
								</td>
								<td>
									<asp:TextBox runat="server" ID="tbNativeName" ReadOnly="True" Width="680px"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td>Pair:
								</td>
								<td>
									<asp:TextBox runat="server" ID="tbPair" ReadOnly="True" Width="680px"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td colspan="2" align="center">
									<asp:Button runat="server" ID="btSave" Text="Save" OnClick="btSave_Click" />
									<asp:Button runat="server" ID="btCancel" Text="Cancel" OnClick="btCancel_Click" />
								</td>
							</tr>
						</table>
					</div>
				</asp:Panel>
			</ContentTemplate>
		</asp:UpdatePanel>
	</form>
</body>
</html>
