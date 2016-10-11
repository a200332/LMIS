<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoriesControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.CategoriesControl" %>
<dx:ASPxTreeList runat="server" KeyFieldName="ID" ParentFieldName="ParentID" ID="tlData">
	<settings showtreelines="False" showcolumnheaders="False" />
	<settingsbehavior allowfocusednode="True" allowsort="False" allowellipsisintext="True" expandcollapseaction="NodeClick" />
	<images>
		<ExpandedButton Width="0px" Height="0px" Url="~/App_Themes/Default/Images/Collapse.png"></ExpandedButton>
		<CollapsedButton Width="0px" Height="0px" Url="~/App_Themes/Default/Images/Expand.png"></CollapsedButton>
	</images>
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