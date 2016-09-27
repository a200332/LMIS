<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoriesControl.ascx.cs" Inherits="Controls_DataDisplay_CategoriesControl" %>
<dx:ASPxTreeList runat="server" KeyFieldName="ID" ParentFieldName="ParentID" ID="tlData">
	<settingsbehavior allowfocusednode="True" allowsort="False" allowellipsisintext="True" />
	<columns>
			<dx:TreeListDataColumn FieldName="Name" Name="Name" Caption="Name">
				<DataCellTemplate>
					<div style="width: 80px;">
						<ce:ImageLinkButton runat="server" Text='<%# Eval("Name") %>' ToolTip='<%# Eval("Name") %>' NavigateUrl='<%# GetReportsLink(Eval("ID")) %>' CommandArgument='<%# Eval("ID") %>' DefaultImageUrl='<%# GetImageLink(Eval("ID")) %>' ID="btnCategory" />
					</div>
				</DataCellTemplate>
			</dx:TreeListDataColumn>
		</columns>
</dx:ASPxTreeList>