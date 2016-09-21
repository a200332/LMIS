<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ProjectsList.aspx.cs" Inherits="Pages_ProjectsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">
    <h1>მოდულები
    </h1>
    <div class="page_title_separator"></div>
    <div class="fieldset">
        <asp:LinkButton runat="server" CssClass="icon" ID="btNewProject" ToolTip="მოდულის დამატება"
            Text="მოდულის დამატება"
            OnClick="btNewProject_Click" />
        <asp:UpdatePanel runat="server" ID="upnlMain" UpdateMode="Always">
            <ContentTemplate>
                <mis:DataGrid ID="gvProjects" runat="server" KeyFieldName="ID" Width="900" EnableRowsCache="False">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <mis:ImageLinkButton ID="lnkEdit" runat="server" CommandArgument='<% #Eval("ID")%>'
                                    DefaultImageUrl="~/App_Themes/default/images/edit.png" ToolTip="რედაქტირება"
                                    OnClick="lnkEdit_Click" />
                                <mis:ImageLinkButton ID="lnkAddMessage" runat="server" CommandArgument='<% #Eval("ID")%>'
                                    DefaultImageUrl="~/App_Themes/default/images/add_message.png" ToolTip="მესიჯის დამატება"
                                    OnClick="lnkAddMessage_Click" />
                                <mis:ImageLinkButton ID="lnkDelete" runat="server" CommandArgument='<% #Eval("ID")%>'
                                    DefaultImageUrl="~/App_Themes/default/images/delete.png" ToolTip="წაშლა" OnClick="lnkDelete_Click"
                                    OnClientClick="return confirm('გსურთ მოდულის წაშლა?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="ID" DataField="ID" />
	                    <asp:BoundField HeaderText="მოდული" DataField="Name" />
	                    <asp:BoundField HeaderText="სტატუსი" DataField="IsActive" />
                    </Columns>
                </mis:DataGrid>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <asp:Panel ID="pnlProject" runat="server" Style="display: none;" CssClass="modalWindow"
      DefaultButton="btProjectOK">
        <asp:UpdatePanel ID="upnlProject" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btProjectPopup" runat="server" Style="display: none;" />
                <act:ModalPopupExtender ID="mpeProject" TargetControlID="btProjectPopup" PopupControlID="pnlProject"
                    runat="server" Enabled="True" BackgroundCssClass="modalBackground" DynamicServicePath="" />
                <div class="popup">
                    <div class="popup_fieldset">
                        <asp:HiddenField ID="hdProjectID" runat="server" />
                        <asp:Label ID="lblProjectError" runat="server" ForeColor="Red"></asp:Label>
                        <h2>
                            <asp:Label ID="ModuleContext" runat="server"></asp:Label></h2>

                        <div class="clear"></div>


                        <div class="title_separator"></div>
                        <div class="box">
                            <h3>
                                <asp:Label runat="server">მოდულის სახელი</asp:Label></h3>
                            <div class="box_body">
                                <asp:TextBox ID="tbProjectName" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="box">
                            <h3>
                                <asp:Label ID="Label1" runat="server">მოდულის სტატუსი</asp:Label></h3>
                            <div class="box_body">
                                <mis:CheckBox ID="chkIsActive" runat="server" />
                            </div>
                        </div>

                    </div>
                    <div class="fieldsetforicons">
                        <div class="left">
                            <asp:LinkButton ID="btProjectOK" CssClass="icon" runat="server" Text="შენახვა" ToolTip="შენახვა"
                               OnClick="btProjectOK_Click" />

                        </div>
                        <div class="right">
                             <asp:LinkButton ID="btProjectCancel" CssClass="icon" runat="server" Text="დახურვა" ToolTip="დახურვა" />

                        </div>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <mis:MessageControl runat="server" ID="ucMessage" />
</asp:Content>
