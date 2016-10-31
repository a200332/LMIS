<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SearchResult.aspx.cs" Inherits="Lmis.Portal.Web.Pages.User.SearchResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel runat="server" ID="pnlEmpty" Visible="False">
        <ce:Label runat="server">No Result Found</ce:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlResults">
        <asp:Repeater runat="server" ID="rptItems">
            <ItemTemplate>
                <div>
                    <asp:HyperLink runat="server" CssClass="book" NavigateUrl='<%# Eval("Url") %>' Target="blanck">
                        <div style="font-size: 7pt; text-align: left; color: blue; text-transform: uppercase;">
                            [<ce:Label runat="server" Text='<%# Eval("Type") %>' />]
                        </div>
                        <div class="book-title">
                            <ce:Label runat="server" Text='<%# Eval("Title") %>' />
                        </div>
                        <div class="book-description">
                            <ce:Label runat="server" Text='<%# Eval("Description") %>' />
                        </div>
                    </asp:HyperLink>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>
</asp:Content>

