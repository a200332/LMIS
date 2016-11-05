<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.NewsControl" %>
<div class="news-title">
    <asp:Label runat="server" ID="lblTitle" Property="NewsModel.Title"/>
</div>
<asp:Panel runat="server" ID="pnlImage">
    <asp:Image runat="server" ID="imgPhoto" />
</asp:Panel>
<asp:Panel runat="server" ID="pnlAttachment">
    <asp:HyperLink runat="server" ID="lnkAttachment" Target="_blank" />
</asp:Panel>
<div class="news-txt">
    <div id="dvFullText" runat="server">
    </div>
</div>
