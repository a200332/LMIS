<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainCategoriesControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.MainCategoriesControl" %>
<asp:DataList runat="server" ID="dtCategories" RepeatColumns="3" RepeatDirection="Vertical">
    <ItemTemplate>
        <div class="left">
            <div style="padding: 3px;">
                <asp:HyperLink runat="server" NavigateUrl='<%# GetTargetUrl(Eval("ID")) %>'>
                   <div style="width: 160px; height: 172px; margin:0 5px 5px 0; font-size: 14px; background-color: #dfe87b; color: black; ">
                        <div>
                            <asp:Image runat="server" ImageUrl='<%# GetImageLink(Eval("ID")) %>'/>
                        </div>
                        <div>
                            <%#Eval("Name") %>
                        </div>
                    </div>
                </asp:HyperLink>
            </div>
        </div>
    </ItemTemplate>
</asp:DataList>
