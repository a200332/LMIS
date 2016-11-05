<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportLogicControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.ReportLogicControl" %>

<ul runat="server" id="trChartType">

    <li>
        <ce:Label runat="server">Type</ce:Label>
    </li>
    <li>
        <dx:ASPxComboBox runat="server" ID="cbxType" Property="ReportLogicModel.Type" Width="180"  ValueType="System.String">
            <Items>
                <dx:ListEditItem Text="Line" Value="Line" />
                <dx:ListEditItem Text="Point" Value="Point" />
                <dx:ListEditItem Text="FastPoint" Value="FastPoint" />
                <dx:ListEditItem Text="Spline" Value="Spline" />
                <dx:ListEditItem Text="StepLine" Value="StepLine" />
                <dx:ListEditItem Text="FastLine" Value="FastLine" />
                <dx:ListEditItem Text="Bar" Value="Bar" />
                <dx:ListEditItem Text="StackedBar" Value="StackedBar" />
                <dx:ListEditItem Text="StackedBar100" Value="StackedBar100" />
                <dx:ListEditItem Text="Column" Value="Column" />
                <dx:ListEditItem Text="StackedColumn" Value="StackedColumn" />
                <dx:ListEditItem Text="StackedColumn100" Value="StackedColumn100" />
                <dx:ListEditItem Text="Area" Value="Area" />
                <dx:ListEditItem Text="SplineArea" Value="SplineArea" />
                <dx:ListEditItem Text="StackedArea" Value="StackedArea" />
                <dx:ListEditItem Text="StackedArea100" Value="StackedArea100" />
                <dx:ListEditItem Text="Pie" Value="Pie" />
                <dx:ListEditItem Text="Doughnut" Value="Doughnut" />
                <dx:ListEditItem Text="Stock" Value="Stock" />
                <dx:ListEditItem Text="Candlestick" Value="Candlestick" />
                <dx:ListEditItem Text="Range" Value="Range" />
                <dx:ListEditItem Text="SplineRange" Value="SplineRange" />
                <dx:ListEditItem Text="RangeBar" Value="RangeBar" />
                <dx:ListEditItem Text="RangeColumn" Value="RangeColumn" />
                <dx:ListEditItem Text="Radar" Value="Radar" />
                <dx:ListEditItem Text="Polar" Value="Polar" />
                <dx:ListEditItem Text="ErrorBar" Value="ErrorBar" />
                <dx:ListEditItem Text="BoxPlot" Value="BoxPlot" />
            </Items>
        </dx:ASPxComboBox>
    </li>

</ul>
<ul>
    <li>
        <ce:Label runat="server">Table</ce:Label>
    </li>
    <li>
        <dx:ASPxComboBox runat="server" ID="cbxTable" ValueType="System.Guid" Width="180"  ValueField="ID" TextField="Name" AutoPostBack="True" OnSelectedIndexChanged="cbxTable_OnSelectedIndexChanged" />
    </li>
</ul>
<ul>
    <li>
        <ce:Label runat="server">Logic</ce:Label>
    </li>
    <li>
        <dx:ASPxComboBox runat="server" ID="cbxLogic" ValueType="System.Guid" ValueField="ID" Width="180"  TextField="Name" AutoPostBack="True" OnSelectedIndexChanged="cbxLogic_OnSelectedIndexChanged" />
    </li>
</ul>
<ce:Label CssClass="title" runat="server">Bindings</ce:Label>



<asp:Panel runat="server" ID="pnlGridBinding">

    <ul>
        <li>
            <ce:Label runat="server">Caption</ce:Label>
        </li>
        <li>
            <asp:TextBox runat="server" Width="180"  ID="tbxGridCaption" />
        </li>
    </ul>
    <ul>
        <li>
            <ce:Label runat="server">Source</ce:Label>
        </li>
        <li>
            <dx:ASPxComboBox runat="server" ID="cbxGridSource" Width="180"  ValueType="System.String" />
        </li>
    </ul>
</asp:Panel>
<asp:Panel runat="server" ID="pnlChartBinding">
    <ul>
        <li>
            <ce:Label runat="server">Caption</ce:Label>
        </li>
        <li>
            <dx:ASPxComboBox runat="server" ID="cbxChartCaption" Width="180"  ValueType="System.String" />
        </li>
    </ul>
    <ul>
        <li>
            <ce:Label runat="server">XValue</ce:Label>
        </li>
        <li>
            <dx:ASPxComboBox runat="server" ID="cbxChartXValue" Width="180"  ValueType="System.String" />
        </li>
    </ul>
    <ul>
        <li>
            <ce:Label runat="server">YValue</ce:Label></li>
        <li>
            <dx:ASPxComboBox runat="server" ID="cbxChartYValue" Width="180"  ValueType="System.String" />
        </li>
    </ul>
</asp:Panel>

<ce:ImageLinkButton runat="server" ToolTip="Save" Target="_blank" DefaultImageUrl="~/App_Themes/Default/images/add.png" ID="btnSaveBinding" OnClick="btnSaveBinding_OnClick" />

<asp:Panel runat="server" ID="pnlChartBindings">
    <dx:ASPxGridView ID="gvChartBindings"
        runat="server"
        AutoGenerateColumns="False"
        ClientInstanceName="gvChartBindings"
        KeyFieldName="Key"
        Width="600"
        ClientIDMode="AutoID"
        EnableRowsCache="False"
        EnableViewState="False">
        <Columns>
            <dx:GridViewDataTextColumn Caption=" " FieldName="" VisibleIndex="0">
                <DataItemTemplate>
                    <ce:ImageLinkButton runat="server" ToolTip="Edit" Target="_blank" CommandArgument='<%# Eval("Key") %>' DefaultImageUrl="~/App_Themes/Default/images/edit.png" ID="btnEditChartBinding" OnCommand="btnEditChartBinding_OnCommand" Visible="False" />
                    <ce:ImageLinkButton runat="server" ToolTip="Delete" Target="_blank" CommandArgument='<%# Eval("Key") %>' DefaultImageUrl="~/App_Themes/Default/images/delete.png" ID="btnDeleteChartBinding" OnCommand="btnDeleteChartBinding_OnCommand" OnClientClick="return confirm('Are you sure you want to delete?');" />
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Caption" FieldName="Caption" />
            <dx:GridViewDataTextColumn Caption="XValue" FieldName="XValue" />
            <dx:GridViewDataTextColumn Caption="YValue" FieldName="YValue" />
        </Columns>
    </dx:ASPxGridView>
</asp:Panel>
<asp:Panel runat="server" ID="pnlGridBindings">
    <dx:ASPxGridView ID="gvGridBindings"
        runat="server"
        AutoGenerateColumns="False"
        ClientInstanceName="gvGridBindings"
        KeyFieldName="Key"
        Width="600"
        ClientIDMode="AutoID"
        EnableRowsCache="False"
        EnableViewState="False">
        <Columns>
            <dx:GridViewDataTextColumn Caption=" " FieldName="" VisibleIndex="0">
                <DataItemTemplate>
                    <ce:ImageLinkButton runat="server" ToolTip="Edit" Target="_blank" CommandArgument='<%# Eval("Key") %>' DefaultImageUrl="~/App_Themes/Default/images/edit.png" ID="btnEditGridBinding" OnCommand="btnEditGridBinding_OnCommand" Visible="False" />
                    <ce:ImageLinkButton runat="server" ToolTip="Delete" Target="_blank" CommandArgument='<%# Eval("Key") %>' DefaultImageUrl="~/App_Themes/Default/images/delete.png" ID="btnDeleteGridBinding" OnCommand="btnDeleteGridBinding_OnCommand" OnClientClick="return confirm('Are you sure you want to delete?');" />
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Caption" FieldName="Caption" />
            <dx:GridViewDataTextColumn Caption="Source" FieldName="Source" />
        </Columns>
    </dx:ASPxGridView>
</asp:Panel>
