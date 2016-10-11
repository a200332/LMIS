<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/Controls/DataDisplay/VideosControl.ascx" TagPrefix="lmis" TagName="VideosControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<link href="App_Themes/Default/slide.css" rel="stylesheet" />

	<lmis:VideosControl ID="videosControl" runat="server" />

	<a href="Pages/User/eBooks.aspx" target="blanck">
		<div class="left e-book">
		</div>
	</a>
	<div class="right-menu">
		<ul>
			<li class="vertical-center right-menu-padding rm-search"><a href="#">Job Finder</a></li>
			<li class="vertical-center right-menu-padding rm-profile"><a href="#">Create  Profile</a></li>
			<li class="vertical-center right-menu-padding rm-postjob"><a href="#">Post the job</a></li>
			<li class="last vertical-center right-menu-padding rm-rate"><a href="#">Rate the website</a></li>
		</ul>
		<%--<img src="App_Themes/Default/images/left-article.png" />--%>
	</div>
</asp:Content>

