<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="GeorgiaInNumbers.aspx.cs" Inherits="Lmis.Portal.Web.Pages.Management.GeorgiaInNumbers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/Images/save.png" ID="btnSave" ToolTip="დამატება" OnClick="btnSave_OnClick" />
    </div>
    <div>
        <asp:FileUpload runat="server" ID="fuAttachment" />
    </div>
</asp:Content>

