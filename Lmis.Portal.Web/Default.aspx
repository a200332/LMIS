<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

	<div class="video-slider" style="width: 529px; height: 349px; margin-top: 1px; margin-right: 1px; float: left;">
		<!-- SLIDE 1 -->
		<div class="slide">
			<iframe width="530" height="348" src="https://www.youtube.com/embed/oBC7bA-PeSI"  seamless="seamless" allowfullscreen="allowfullscreen"></iframe>
		</div>
		<!-- SLIDE 2 -->
		<div class="slide">
			<iframe width="530" height="348" src="https://www.youtube.com/embed/uh57qksEeLE" seamless="seamless" allowfullscreen="allowfullscreen"></iframe>
		</div>
		<!-- END OF SLIDES -->
		<div class="slide-arrow left"></div>
		<div class="slide-arrow right"></div>
	</div>
	<%--<div class="left">
        <img src="App_Themes/Default/images/home-article.png" />
    </div>--%>
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

