<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExpressionsLogicControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Common.ExpressionsLogicControl" %>

<%@ Register Src="~/Controls/Common/NamedExpressionsListControl.ascx" TagPrefix="local" TagName="NamedExpressionsListControl" %>
<%@ Register Src="~/Controls/Common/ExpressionsListControl.ascx" TagPrefix="local" TagName="ExpressionsListControl" %>

<div class="popup-title">Where conditions</div>
<div class="admin-title-separator"></div>
<div class="admin-fieldset">
    <local:ExpressionsListControl runat="server" ID="filterByControl" Property="ExpressionsLogicModel.FilterBy" />
</div>
<div class="popup-title">Group By</div>
<div class="admin-title-separator"></div>
<div class="admin-fieldset">
    <local:ExpressionsListControl runat="server" ID="groupByControl" Property="ExpressionsLogicModel.GroupBy" />

</div>
<div class="popup-title">Order By</div>
<div class="admin-title-separator"></div>
<div class="admin-fieldset">
    <local:ExpressionsListControl runat="server" ID="orderByControl" Property="ExpressionsLogicModel.OrderBy" />

</div>
<div class="popup-title">Select</div>
<div class="admin-title-separator"></div>
<div class="admin-fieldset">
    <local:NamedExpressionsListControl runat="server" ID="selectControl" Property="ExpressionsLogicModel.Select" />

</div>

