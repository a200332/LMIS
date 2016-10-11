<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VideosControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.DataDisplay.VideosControl" %>
<div class="video-slider" style="width: 529px; height: 349px; margin-top: 1px; margin-right: 1px; float: left;">
	<ul class="bxslider">
		<asp:Repeater runat="server" ID="rptItems">
			<ItemTemplate>
				<li>
					<iframe width="530" height="348" src='<%# Eval("Url") %>' seamless="seamless" allowfullscreen="allowfullscreen"></iframe>
				</li>
			</ItemTemplate>
		</asp:Repeater>
	</ul>
</div>
