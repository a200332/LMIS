<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoriesControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.CategoriesControl" %>

<div class="treelistscrollbar" id="style-1">
    <div class="force-overflow">
        <dx:ASPxTreeList runat="server" KeyFieldName="ID" ParentFieldName="ParentID" ID="tlData">
            <Settings ShowTreeLines="False" ShowColumnHeaders="False"  />
            <SettingsBehavior AllowFocusedNode="True" AllowSort="False" AllowEllipsisInText="True" ExpandCollapseAction="NodeClick" />
            <Images>
                <ExpandedButton Width="0px" Height="0px" Url="~/App_Themes/Default/Images/Collapse.png"></ExpandedButton>
                <CollapsedButton Width="0px" Height="0px" Url="~/App_Themes/Default/Images/Expand.png"></CollapsedButton>
            </Images>

            <Columns>
                <dx:TreeListDataColumn FieldName="Name" Name="Name" Caption="Name">

                    <DataCellTemplate>

                        <asp:Panel runat="server" Style="width: 280px; display: -webkit-inline-box" Visible='<%# GetLabelVisible(Eval("ID")) %>'>
                            <asp:Image runat="server" ImageUrl='<%# GetImageLink(Eval("ID")) %>' />
                            <asp:Label runat="server" Text='<%# GetShortName(Container.DataItem) %>' ToolTip='<%# GetFullName(Container.DataItem) %>' />
                        </asp:Panel>
                        <asp:Panel Style="width: 280px; display: -webkit-inline-box" runat="server" Visible='<%# GetLinkVisible(Eval("ID")) %>'>
                            <asp:HyperLink runat="server" NavigateUrl='<%# GetReportsLink(Eval("ID")) %>'>
					        <asp:Image runat="server" ImageUrl='<%# GetImageLink(Eval("ID")) %>'/>
                            <asp:Label runat="server" Text='<%# GetShortName(Container.DataItem) %>' ToolTip='<%# GetFullName(Container.DataItem) %>'/>
                            </asp:HyperLink>
                        </asp:Panel>

                    </DataCellTemplate>
                </dx:TreeListDataColumn>
            </Columns>
        </dx:ASPxTreeList>
    </div>
</div>
