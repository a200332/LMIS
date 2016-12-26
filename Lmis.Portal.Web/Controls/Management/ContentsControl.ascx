<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentsControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.ContentsControl" %>
<div>
    <dx:ASPxGridView ID="gvData"
        runat="server"
        AutoGenerateColumns="False"
        ClientInstanceName="gvData"
        KeyFieldName="ID"
        Width="100%"
        ClientIDMode="AutoID"
        EnableRowsCache="False"
        EnableViewState="False">
        <Settings ShowFilterRow="True"></Settings>
        <Columns>
            <dx:GridViewDataTextColumn Caption=" " FieldName="" VisibleIndex="0" Name="colCommand">
                <DataItemTemplate>
                    <ce:ImageLinkButton runat="server" ToolTip="Download" Target="_blank" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/view.png" ID="btnDownload" />
                    <ce:ImageLinkButton runat="server" ToolTip="Delete" Target="_blank" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/delete.png" ID="btnDelete" OnCommand="btnDelete_OnCommand" OnClientClick="return confirm('Are you sure you want to delete?');" />
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="AttachmentSize" FieldName="AttachmentSize" />
            <dx:GridViewDataTextColumn Caption="AttachmentName" FieldName="AttachmentName" />
            <dx:GridViewDataTextColumn Caption="DateCreated" FieldName="DateCreated" />
            <dx:GridViewDataTextColumn Caption="Url" FieldName="Url" />
        </Columns>
    </dx:ASPxGridView>
</div>
