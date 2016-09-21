<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Pages_ChangePassword" %>
<%@ Register TagPrefix="mis" Namespace="CITI.EVO.Tools.Web.UI.Controls" Assembly="CITI.EVO.Tools" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" DefaultButton="btOK">
            <section id="login_page">
                <div class="logo">
                    <asp:Image runat="server" ImageUrl="~/App_Themes/default/images/l2.png" />
                </div>
                <div class="main_content">
                    <mis:Label ID="lstWarningMessages" runat="server" ForeColor="Red" Style="font-weight: bold;">
                    </mis:Label>
                    <mis:Label ID="lstErrorMessages" runat="server">
                    </mis:Label>
                    <div class="box">
                        <h1>ძველი პაროლი</h1>
                        <div class="box_body">
                            <asp:TextBox runat="server" ID="tbOldPassword" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="box">
                        <h1>ახალი პაროლი
                        </h1>
                        <div class="box_body">
                            <asp:TextBox runat="server" ID="tbNewPassword" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="box">
                        <h1>ახალი პაროლი გამეორებით
                        </h1>
                        <div class="box_body">
                            <asp:TextBox runat="server" ID="tbConfirmPassword" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clear"></div>
                    <div class="wrapper"> </div>
                        <mis:ImageLinkButton ID="btOK" CssClass="login_button"
                            Text="შეცვლა" runat="server" OnClick="btOK_Click" />
                           <div class="wrapper"> </div>
                        <mis:ImageLinkButton ID="btCancel" CssClass="login_button"
                            Text="უკან დაბრუნება" runat="server" OnClick="btCancel_Click" />
                   
                </div>
            </section>
        </asp:Panel>
    </form>
</body>
</html>
