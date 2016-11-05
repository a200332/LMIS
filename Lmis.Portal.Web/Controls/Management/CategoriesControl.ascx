<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoriesControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.CategoriesControl" %>
<div>
    <dx:ASPxTreeList runat="server" KeyFieldName="ID" ParentFieldName="ParentID" ID="tlData">
        <Images>
            <ExpandedButton Width="12px" Height="10px" Url="~/App_Themes/Default/Images/Collapse.png"></ExpandedButton>
            <CollapsedButton Width="12px" Height="10px" Url="~/App_Themes/Default/Images/Expand.png"></CollapsedButton>
        </Images>
        <Columns>
            <dx:TreeListDataColumn>
                <DataCellTemplate>
                    <div>
                        <ce:ImageLinkButton runat="server" ToolTip="Up" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/up.png" ID="btnUp" OnCommand="btnUp_OnCommand" />
                        <ce:ImageLinkButton runat="server" ToolTip="Down" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/down.png" ID="btnDown" OnCommand="btnDown_OnCommand" />

                        <ce:ImageLinkButton runat="server" ToolTip="Edit" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/edit.png" ID="btnEdit" OnCommand="btnEdit_OnCommand" />
                        <ce:ImageLinkButton runat="server" ToolTip="Delete" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/delete.png" ID="btnDelete" OnCommand="btnDelete_OnCommand" OnClientClick="return confirm('Are you sure you want to delete?');" />
                        <ce:ImageLinkButton runat="server" ToolTip="Add" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/add.png" ID="btnAddChild" OnCommand="btnAddChild_OnCommand" />
                    </div>
                </DataCellTemplate>
            </dx:TreeListDataColumn>
            <dx:TreeListDataColumn FieldName="Number" Name="Number" Caption="Number">
            </dx:TreeListDataColumn>
            <dx:TreeListDataColumn FieldName="Name" Name="Name" Caption="Name">
            </dx:TreeListDataColumn>
        </Columns>
    </dx:ASPxTreeList>
</div>
