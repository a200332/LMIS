<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TableControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.SchemaManipulation.TableControl" %>
<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="TableModel.ID" />
<asp:TextBox runat="server" ID="tbxName" Property="TableModel.Name"></asp:TextBox>