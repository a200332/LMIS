<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TableControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.TableControl" %>
<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="TableModel.ID" />
<ul>
	<li>
		<ce:Label runat="server">Table Name</ce:Label></li>
	<li>
		<asp:TextBox runat="server" ID="tbxName" Property="TableModel.Name"></asp:TextBox></li>
</ul>
