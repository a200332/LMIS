<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="GroupsList.aspx.cs" Inherits="Pages_GroupsList" %>
<%@ Register TagPrefix="mis" Namespace="CITI.EVO.Tools.Web.UI.Controls" Assembly="CITI.EVO.Tools" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="upnlMain" UpdateMode="Always" RenderMode="Block">
        <ContentTemplate>
     <h1>
                   <asp:Label runat="server">ჯგუფები</asp:Label>
            </h1>
            <div class="page_title_separator"></div>
        
            <mis:ASPxTreeList ID="tlGroups" runat="server" EnableViewState="False" AutoGenerateColumns="False"
                KeyFieldName="ID" ParentFieldName="ParentID" ClientIDMode="AutoID" Width="100%"
                ViewStateMode="Disabled" OnVirtualModeCreateChildren="tlGroups_VirtualModeCreateChildren"
                OnVirtualModeNodeCreating="tlGroups_VirtualModeNodeCreating" OnVirtualModeNodeCreated="tlGroups_VirtualModeNodeCreated">
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
                    <dx:TreeListHyperLinkColumn VisibleIndex="2">
                        <DataCellTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" DefaultImageUrl="~/App_Themes/default/images/edit.png"
                                ToolTip="რედაქტირება" CommandArgument='<% #Eval("ID")%>' Visible='<%#Eval("EditVisible") %>'
                                OnClick="lnkEdit_Click" />
                        </DataCellTemplate>
                    </dx:TreeListHyperLinkColumn>
                    <dx:TreeListHyperLinkColumn VisibleIndex="3">
                        <DataCellTemplate>
                            <mis:ImageLinkButton ID="lnkDelete" runat="server" DefaultImageUrl="~/App_Themes/default/images/delete.png"
                                ToolTip="წაშლა" OnClientClick='<%#Eval("ConfirmationDialog") %>' CommandArgument='<% #Eval("ID")%>'
                                OnClick="lnkDelete_Click" Visible='<%#Eval("DeleteVisible") %>' />
                        </DataCellTemplate>
                    </dx:TreeListHyperLinkColumn>
                    <dx:TreeListHyperLinkColumn VisibleIndex="4">
                        <DataCellTemplate>
                            <mis:ImageLinkButton ID="lnkNew" runat="server" ToolTip='<%#Eval("ToolTip") %>'
                                DefaultImageUrl="~/App_Themes/default/images/add_group.png" CommandArgument='<% #Eval("ID")%>'
                                CommandName="AddUser" Visible='<%#Eval("NewVisible") %>' OnClick="lnkNew_Click" />
                        </DataCellTemplate>
                    </dx:TreeListHyperLinkColumn>
                    <dx:TreeListHyperLinkColumn VisibleIndex="5">
                        <DataCellTemplate>
                            <mis:ImageLinkButton ID="lnkNewSubGroup" runat="server" ToolTip="ქვე ჯგუფის დამატება"
                                DefaultImageUrl="~/App_Themes/default/images/add.png" CommandArgument='<% #Eval("ID")%>'
                                CommandName="AddSubGroup" Visible='<%#Eval("NewSubGroupVisible") %>' OnClick="lnkNew_Click" />
                        </DataCellTemplate>
                    </dx:TreeListHyperLinkColumn>
                    <dx:TreeListHyperLinkColumn VisibleIndex="6">
                        <DataCellTemplate>
                            <mis:ImageLinkButton CommandName="lnkAttributes" ID="lnkAttributes" runat="server"
                                ToolTip="ატრიბუტების დამატება" DefaultImageUrl="~/App_Themes/default/images/add_atributes.png"
                                CommandArgument='<% #Eval("ID")%>' Visible='<%#Eval("AttrVisible") %>' OnClick="lnkAttributes_Click" />
                        </DataCellTemplate>
                    </dx:TreeListHyperLinkColumn>
                    <dx:TreeListHyperLinkColumn VisibleIndex="7" Width="3px">
                        <DataCellTemplate>
                            <mis:ImageLinkButton CommandName="ShowAttributes" ID="lnkShowAttributes" runat="server"
                                DefaultImageUrl="~/App_Themes/default/images/view.png" ToolTip="ატრიბუტების ნახვა"
                                CommandArgument='<% #Eval("ID")%>' Visible='<%#Eval("AttrShowVisible") %>' OnClick="lnkAttributes_Click" />
                        </DataCellTemplate>
                    </dx:TreeListHyperLinkColumn>
                    <dx:TreeListHyperLinkColumn VisibleIndex="8" Width="3px">
                        <DataCellTemplate>
                            <mis:ImageLinkButton CommandName="Message" ID="lnkMessage" runat="server"
                                DefaultImageUrl="~/App_Themes/default/images/info.png" ToolTip="მესიჯის დამატება"
                                CommandArgument='<% #Eval("ID")%>' Visible='<%#Eval("MessageVisible") %>' OnClick="lnkMessage_Click" />
                        </DataCellTemplate>
                    </dx:TreeListHyperLinkColumn>
                    <dx:TreeListHyperLinkColumn VisibleIndex="0" Width="3px">
                        <DataCellTemplate>
                            <asp:Image runat="server" ImageUrl='<%#Eval("imgUrl") %>' />
                        </DataCellTemplate>
                    </dx:TreeListHyperLinkColumn>
                    <dx:TreeListTextColumn>
                        <HeaderCaptionTemplate>
                            <asp:Label ID="Label1" runat="server" Text="სახელი" />
                        </HeaderCaptionTemplate>
                        <DataCellTemplate>
                            <asp:Label ID="Label2"  runat="server" Text='<%#Eval("Name") %>' />
                        </DataCellTemplate>
                    </dx:TreeListTextColumn>
                </Columns>
            </mis:ASPxTreeList>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlGroup" runat="server" Style="display: none;" CssClass="modalWindow"
        Width="333px" DefaultButton="btGroupOK">
        <asp:UpdatePanel ID="upnlGroup" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hdMainGroupID" />
                <asp:HiddenField runat="server" ID="hdMainGroupParentID" />
                <asp:HiddenField runat="server" ID="hdParentID" />
                <asp:Button ID="btGroupPopup" runat="server" Style="display: none;" />
                <act:ModalPopupExtender ID="mpeGroup" TargetControlID="btGroupPopup" PopupControlID="pnlGroup"
                    runat="server" Enabled="True" BackgroundCssClass="modalBackground" DynamicServicePath="" />
                <div class="ModalWindow" style="width: 330px;">
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
                                                                <asp:Label ID="context" runat="server"></asp:Label>
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
                                                                    <div>
                                                                        <asp:UpdateProgress ID="updGroupProcess" runat="server" AssociatedUpdatePanelID="upnlGroup">
                                                                            <ProgressTemplate>
                                                                                <img alt="" src="../Images/small-waiting.gif" />
                                                                            </ProgressTemplate>
                                                                        </asp:UpdateProgress>
                                                                        <div class="popup_text_error">
                                                                            <asp:Label ID="lblGroupError" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="box_body_popup">
                                                                        <div class="box_title_popup" style="color: #5D5D5D; font-size: 12px; width: 150px;">
                                                                            <asp:Label runat="server">სახელი</asp:Label>
                                                                        </div>
                                                                        <div class="box_popup">
                                                                            <asp:TextBox ID="tbGroupName" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="box_body_popup">
                                                                        
                                                                    </div>
                                                                    <div style="text-align: left; padding: 13px 0 2px 3px; float: left;">
                                                                        <mis:ImageLinkButton ID="btGroupOK" runat="server" Text="შენახვა" ToolTip="შენახვა"
                                                                            DefaultImageUrl="~/App_Themes/default/images/add_icon.png" OnClick="btGroupOK_Click" />
                                                                    </div>
                                                                    <div style="text-align: left; padding: 13px 0 2px 15px; float: left;">
                                                                        <mis:ImageLinkButton ID="btGroupCancel" runat="server" Text="დახურვა" ToolTip="დახურვა"
                                                                            DefaultImageUrl="~/App_Themes/default/images/close_icon.png" />
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
    <asp:Panel ID="pnlGroupAttributes" runat="server" Style="display: none;" CssClass="modalWindow"
        Width="333px" DefaultButton="btGroupAttributesOK">
        <asp:UpdatePanel ID="upnlGroupAttributes" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hdAttributeGroupID" />
                <asp:HiddenField runat="server" ID="hdAttributeGroupParentID" />
                <asp:Button ID="btGroupAttributesPopup" runat="server" Style="display: none;" />
                <act:ModalPopupExtender ID="mpeGroupAttributes" TargetControlID="btGroupAttributesPopup"
                    PopupControlID="pnlGroupAttributes" runat="server" Enabled="True" BackgroundCssClass="modalBackground" />
                <div class="ModalWindow" style="width: 330px;">
                    <asp:HiddenField ID="hdGroupAttributeID" runat="server" />
                    <div class="ModalWindow" style="width: 330px;">
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
                                                                    <asp:Label runat="server">ატრიბუტების დამატება</asp:Label>
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
                                                                        <div class="ftitle">
                                                                            <asp:UpdateProgress ID="updGroupAttributes" runat="server" AssociatedUpdatePanelID="upnlGroupAttributes">
                                                                                <ProgressTemplate>
                                                                                    <img alt="" src="../Images/small-waiting.gif" />
                                                                                </ProgressTemplate>
                                                                            </asp:UpdateProgress>
                                                                            <asp:Label ID="lblGroupAttributesError" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                        <div class="box_body_popup">
                                                                            <div class="box_title_popup" style="color: #5D5D5D; font-size: 12px; width: 150px;">
                                                                                <asp:Label runat="server">ატრიბუტის სქემა</asp:Label>
                                                                            </div>
                                                                            <div class="box_popup">
                                                                                <mis:ASPxComboBox ID="cmbAttributeSchemas" CssClass="content_left_dropdown" TextField="Name"
                                                                                    ValueField="ID" Width="180px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbAttributeSchemas_SelectedIndexChanged" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="box_body_popup">
                                                                            <div class="box_title_popup" style="color: #5D5D5D; font-size: 12px; width: 150px;">
                                                                                <asp:Label runat="server">ატრიბუტის სახელი</asp:Label>
                                                                            </div>
                                                                            <div class="box_popup">
                                                                                <mis:ASPxComboBox ID="cmbAttributeSchemaNodes" CssClass="content_left_dropdown" TextField="Name"
                                                                                    ValueField="ID" Width="180px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbAttributeSchemaNodes_SelectedIndexChanged" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="box_body_popup">
                                                                            <div class="box_title_popup" style="color: #5D5D5D; font-size: 12px; width: 150px;">
                                                                                <asp:Label runat="server">სახელი</asp:Label>
                                                                            </div>
                                                                            <div class="box_popup">
                                                                                <asp:TextBox ID="tbGroupAttributesValue" Width="178px" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div style="text-align: left; padding: 13px 0 2px 3px; float: left;">
                                                                            <mis:ImageLinkButton ID="btGroupAttributesOK" runat="server" Text="შენახვა" ToolTip="შენახვა"
                                                                                DefaultImageUrl="~/App_Themes/default/images/add_icon.png" OnClick="btGroupAttributesOK_Click" />
                                                                        </div>
                                                                        <div style="text-align: left; padding: 13px 0 2px 15px; float: left;">
                                                                            <mis:ImageLinkButton ID="btGroupAttributesCancel" runat="server" Text="დახურვა"
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="pnlShowGroupAttributes" runat="server" Style="display: none;" CssClass="modalWindow"
        Width="333px" DefaultButton="btShowGroupAttributesCancel">
        <asp:UpdatePanel ID="upnlShowGroupAttributes" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btShowGroupAttributesPopup" runat="server" Style="display: none;" />
                <act:ModalPopupExtender ID="mpeShowGroupAttributes" TargetControlID="btShowGroupAttributesPopup"
                    PopupControlID="pnlShowGroupAttributes" runat="server" Enabled="True" BackgroundCssClass="modalBackground" />
                <div class="ModalWindow" style="width: 500px;">
                    <asp:HiddenField ID="hdGroupIDShow" runat="server" />
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
                                                                <asp:Label runat="server">ატრიბუტების ნახვა</asp:Label>
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
                                                                        <div class="box_popup">
                                                                            <asp:UpdateProgress ID="updShowGroupAttributes" runat="server" AssociatedUpdatePanelID="upnlShowGroupAttributes">
                                                                                <ProgressTemplate>
                                                                                    <img alt="" src="../Images/small-waiting.gif" />
                                                                                </ProgressTemplate>
                                                                            </asp:UpdateProgress>
                                                                        </div>
                                                                    </div>
                                                                    <div class="box_body_popup" style="float: left;">
                                                                        <div class="box_title_popup" style="color: #5D5D5D; font-size: 12px; width: 150px;">
                                                                            <asp:Label runat="server">ატრიბუტების სქემა</asp:Label>
                                                                        </div>
                                                                        <div class="box_popup">
                                                                            <mis:ASPxComboBox ID="cmbShowAttributeSchemas" CssClass="content_left_dropdown" TextField="Name"
                                                                                ValueField="ID" Width="184px" AppendDataBoundItems="true" runat="server"
                                                                                AutoPostBack="true" OnSelectedIndexChanged="cmbAttributeSchemas_SelectedIndexChanged">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="-- select schema --" Value="" />
                                                                                </Items>
                                                                            </mis:ASPxComboBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="box_body_popup">
                                                                        <div class="box_title_popup" style="color: #5D5D5D; font-size: 12px; width: 90px; padding-right: 65px;">
                                                                            <asp:Label runat="server">მნიშვნელობები</asp:Label>
                                                                        </div>
                                                                        <div class="box_popup" style="padding-top: 20px;">
                                                                            <mis:ASPxGridView ID="dwAttributeSchemaNodes" ClientInstanceName="dwAttributeSchemaNodes"
                                                                                Width="340px" runat="server" EnableViewState="false">
                                                                                <SettingsBehavior AllowSort="false" AllowDragDrop="false"></SettingsBehavior>
                                                                                <Columns>
                                                                                    <dx:GridViewDataColumn FieldName="Name" VisibleIndex="0">
                                                                                        <HeaderCaptionTemplate>
                                                                                            <asp:Label ID="Label3" runat="server">სახელი</asp:Label>
                                                                                        </HeaderCaptionTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                    <dx:GridViewDataColumn FieldName="Value" VisibleIndex="1">
                                                                                        <HeaderCaptionTemplate>
                                                                                            <asp:Label ID="Label3" runat="server">მნიშვნელიბა</asp:Label>
                                                                                        </HeaderCaptionTemplate>
                                                                                    </dx:GridViewDataColumn>
                                                                                </Columns>
                                                                            </mis:ASPxGridView>
                                                                        </div>
                                                                    </div>
                                                                    <div style="float: left; padding: 13px 0 2px 3px;">
                                                                        <mis:ImageLinkButton ID="btShowGroupAttributesCancel" DefaultImageUrl="~/App_Themes/default/images/close_icon.png"
                                                                            Text="დახურვა" ToolTip="დახურვა" runat="server" />
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
    <asp:Panel ID="pnlUsers" runat="server" Style="display: none;" CssClass="modalWindow"
        Width="333px" DefaultButton="btUsersOK">
        <asp:UpdatePanel ID="upnlUsers" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hdUsersGroupID" />
                <asp:HiddenField runat="server" ID="hdUsersGroupParentID" />
                <asp:Button ID="btUsersPopup" runat="server" Style="display: none;" />
                <act:ModalPopupExtender ID="mpeUsers" TargetControlID="btUsersPopup" PopupControlID="pnlUsers"
                    runat="server" Enabled="True" BackgroundCssClass="modalBackground" DynamicServicePath="" />
                <div class="ModalWindow" style="width: 490px;">
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
                                                                <asp:Label runat="server">მომხმარებლის გაწევრიანება</asp:Label>
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
                                                        <div class="stationary_popup" style="width: 490px;">
                                                            <div class="popup">
                                                                <div style="float: left;">
                                                                    <div class="ftitle">
                                                                        <asp:UpdateProgress ID="upUsers" runat="server" AssociatedUpdatePanelID="upnlUsers">
                                                                            <ProgressTemplate>
                                                                                <img alt="" src="../Images/small-waiting.gif" />
                                                                            </ProgressTemplate>
                                                                        </asp:UpdateProgress>
                                                                        <asp:Label ID="lblUsersError" runat="server" ForeColor="Red"></asp:Label>
                                                                    </div>
                                                                    <div class="box_body_popup" style="width: 490px;">
                                                                        <div class="box_title_popup" style="color: #5D5D5D; font-size: 12px; width: 120px;">
                                                                            <asp:Label runat="server">სახელი</asp:Label>
                                                                        </div>
                                                                        <div class="box_popup">
                                                                            <asp:TextBox ID="tbUsersKeyword" Width="320" runat="server"></asp:TextBox>
                                                                            <asp:ImageButton ID="btSearchUsers" ImageUrl="~/App_Themes/default/images/view.png"
                                                                                runat="server" OnClick="btSearchUsers_Click" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="box_body_popup" style="width: 490px;">
                                                                        <div class="box_title_popup" style="color: #5D5D5D; font-size: 12px; width: 120px;">
                                                                            <asp:Label runat="server">უფლებები</asp:Label>
                                                                        </div>
                                                                        <div class="box_popup">
                                                                            <mis:ASPxComboBox ID="ddlAccessLevels" Width="320" runat="server">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="სტანდარტული" Value="0" />
                                                                                    <dx:ListEditItem Text="ადმინისტრატორი" Value="1" />
                                                                                </Items>
                                                                            </mis:ASPxComboBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="box_body_popup" style="width: 490px;">
                                                                        <div class="box_title_popup" style="color: #5D5D5D; font-size: 12px; width: 120px;">
                                                                            <asp:Label runat="server">მომხმარებელები</asp:Label>
                                                                        </div>
                                                                        <div class="box_popup">
                                                                            <asp:ListBox ID="lstUsers" DataTextField="LoginName" Style="width: 320px; height: 150px;"
                                                                                DataValueField="ID" runat="server" />
                                                                        </div>
                                                                    </div>
                                                                    <div style="text-align: left; padding: 13px 0 2px 3px; float: left;">
                                                                        <mis:ImageLinkButton ID="btUsersOK" runat="server" Text="შენახვა" ToolTip="შენახვა"
                                                                            DefaultImageUrl="~/App_Themes/default/images/add_icon.png" OnClick="btUsersOK_Click" />
                                                                    </div>
                                                                    <div style="text-align: left; padding: 13px 0 2px 15px; float: left;">
                                                                        <mis:ImageLinkButton ID="btUsersCancel" runat="server" Text="დახურვა" ToolTip="დახურვა"
                                                                            DefaultImageUrl="~/App_Themes/default/images/close_icon.png" />
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
    <mis:MessageControl ID="ucMessage" runat="server" />
</asp:Content>
