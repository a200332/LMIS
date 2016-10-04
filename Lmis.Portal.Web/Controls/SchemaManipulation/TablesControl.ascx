<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TablesControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.SchemaManipulation.TablesControl" %>
<dx:ASPxTreeList runat="server" KeyFieldName="ID" ParentFieldName="ParentID" ID="tlData">
	<images>
			<ExpandedButton Width="12px" Height="10px" Url="~/App_Themes/Default/Images/Collapse.png"></ExpandedButton>
			<CollapsedButton Width="12px" Height="10px" Url="~/App_Themes/Default/Images/Expand.png"></CollapsedButton>
		</images>
	<columns>
		<dx:TreeListDataColumn>
			<DataCellTemplate>
				<div style="width: 80px;">
					<ce:ImageLinkButton runat="server" ToolTip="Edit" 
						Visible='<%# GetEditVisible(Container.DataItem) %>' 
						CommandArgument='<%# Eval("Key") %>' 
						DefaultImageUrl="~/App_Themes/Default/images/edit.png" 
						ID="btnEdit" 
						OnCommand="btnEdit_OnCommand" />
					<ce:ImageLinkButton runat="server" ToolTip="Delete" 
						Visible='<%# GetDeleteVisible(Container.DataItem) %>' 
						CommandArgument='<%# Eval("Key") %>' 
						DefaultImageUrl="~/App_Themes/Default/images/delete.png" 
						ID="btnDelete" 
						OnCommand="btnDelete_OnCommand" />
					<ce:ImageLinkButton runat="server" ToolTip="Add" 
						Visible='<%# GetAddVisible(Container.DataItem) %>' 
						CommandArgument='<%# Eval("Key") %>' 
						DefaultImageUrl="~/App_Themes/Default/images/add.png" 
						ID="btnAddChild" 
						OnCommand="btnAddChild_OnCommand" />
					<ce:ImageLinkButton runat="server" ToolTip="Synchronize" 
						Visible='<%# GetSynchVisible(Container.DataItem) %>' 
						CommandArgument='<%# Eval("Key") %>' 
						DefaultImageUrl="~/App_Themes/Default/images/sync.png" 
						ID="btnSync" 
						OnCommand="btnSynch_OnCommand" />
					<ce:ImageLinkButton runat="server" ToolTip="Table Data" 
						Visible='<%# GetTableDataVisible(Container.DataItem) %>' 
						CommandArgument='<%# Eval("Key") %>' 
						DefaultImageUrl="~/App_Themes/Default/images/edit.png" 
						ID="btnTableData"
						NavigateUrl='<%# GetTableDataUrl(Container.DataItem) %>'  />
				</div>
			</DataCellTemplate>
		</dx:TreeListDataColumn>
		<dx:TreeListDataColumn FieldName="Name" Name="Name" Caption="Name">
			<HeaderStyle Wrap="True" HorizontalAlign="Center"></HeaderStyle>
			<CellStyle Wrap="True" HorizontalAlign="Center"></CellStyle>
		</dx:TreeListDataColumn>
		<dx:TreeListDataColumn FieldName="Type" Name="Type" Caption="Type">
			<HeaderStyle Wrap="True" HorizontalAlign="Center"></HeaderStyle>
			<CellStyle Wrap="True" HorizontalAlign="Center"></CellStyle>
		</dx:TreeListDataColumn>
	</columns>
</dx:ASPxTreeList>
