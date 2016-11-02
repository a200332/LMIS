<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Pages_Login" %>
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
                <%--<div class="logo">
                    <asp:Image runat="server" ImageUrl="~/App_Themes/default/images/l2.png" />
                </div>--%>
                <div class="main_content">
                   

                    <div class="box">
                        <h1>მომხმარებელი</h1>
                        <div class="box_body">
                            <asp:TextBox runat="server" ID="tbLoginName"></asp:TextBox>
                        </div>
                    </div>
                    <div class="box">
                        <h1>პაროლი
                        </h1>
                        <div class="box_body">
                            <asp:TextBox runat="server" ID="tbPassword" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="checkbox">
                        <div class="box_body">
                            <mis:CheckBox runat="server" ID="chkRememberMe"></mis:CheckBox>
                        </div>
                        <h2>დამახსოვრება
                        </h2>
                    </div>
                    <div class="clear"></div>
                    <div class="wrapper">
                        <mis:ImageLinkButton ID="btOK" CssClass="login_button"
                            Text="შესვლა" runat="server" OnClick="btOK_Click" />
                    </div>
                    
                                        <mis:Label ID="lstErrorMessages" runat="server" />

                     <asp:Panel runat="server" ID="pnlWarning" Visible="False">
                        <mis:Label ID="lstWarningMessages" runat="server" ForeColor="Red" Style="font-weight: bold;" />
                        <asp:LinkButton runat="server" ID="btnGoToLicPage" OnClick="btnGoToLicPage_OnClick" Text="ლიცენზიის ატვირთვა"></asp:LinkButton>


                    </asp:Panel>
                </div>
            </section>
        </asp:Panel>
    </form>
</body>
</html>

