<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="Lmis.Portal.Web.Pages.User.ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="dvFullText" runat="server">
    </div>
    <div id="map"></div>
    <script>
        function initMap() {
            var uluru = { lat: 41.7051893, lng: 44.7880498 };
            var map = new google.maps.Map(document.getElementById('map'), {
                zoom: 15,
                center: uluru
            });
            var marker = new google.maps.Marker({
                position: uluru,
                map: map
            });
        }
    </script>
    <script async defer
        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAn4myKyIMvmnwLP9Ae3RrbialiEbTFyII&callback=initMap">
    </script>
</asp:Content>

