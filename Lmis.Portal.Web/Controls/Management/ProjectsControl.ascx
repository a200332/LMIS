<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProjectsControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.ProjectsControl" %>

<ce:ASPxTreeList runat="server" KeyFieldName="ID" ParentFieldName="ParentID" ID="tlData">
    <Images>
        <ExpandedButton Width="12px" Height="10px" Url="~/App_Themes/Default/Images/Collapse.png"></ExpandedButton>
        <CollapsedButton Width="12px" Height="10px" Url="~/App_Themes/Default/Images/Expand.png"></CollapsedButton>
    </Images>
    <Columns>
        <dx:TreeListDataColumn>
            <DataCellTemplate>
                <div style="width: 200px;">
                    <ce:ImageLinkButton runat="server" ToolTip="Up" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/up.png" ID="btnUp" OnCommand="btnUp_OnCommand" />
                    <ce:ImageLinkButton runat="server" ToolTip="Down" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/down.png" ID="btnDown" OnCommand="btnDown_OnCommand" />

                    <ce:ImageLinkButton runat="server" ToolTip="Edit" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/edit.png" ID="btnEdit" OnCommand="btnEdit_OnCommand" />
                    <ce:ImageLinkButton runat="server" ToolTip="Delete" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/delete.png" ID="btnDelete" OnCommand="btnDelete_OnCommand" OnClientClick="return confirm('Are you sure you want to delete?');" />
                    <ce:ImageLinkButton runat="server" ToolTip="Add" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/add.png" ID="btnAddChild" OnCommand="btnAddChild_OnCommand" Visible='<%# GetAddChildVisible(Eval("ParentID")) %>' />
                </div>
            </DataCellTemplate>
        </dx:TreeListDataColumn>
        <dx:TreeListDataColumn FieldName="Title" Name="Title" Caption="Title">
        </dx:TreeListDataColumn>
        <dx:TreeListDataColumn FieldName="Number" Name="Number" Caption="Number">
        </dx:TreeListDataColumn>
        <dx:TreeListDataColumn FieldName="Description" Name="Description" Caption="Description">
        </dx:TreeListDataColumn>
        <dx:TreeListDataColumn FieldName="OrderIndex" Name="OrderIndex" Caption="OrderIndex">
        </dx:TreeListDataColumn>
        <dx:TreeListDataColumn FieldName="Language" Name="Language" Caption="Language">
        </dx:TreeListDataColumn>
    </Columns>
</ce:ASPxTreeList>