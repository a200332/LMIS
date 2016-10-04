<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdministratorChatControl.ascx.cs" Inherits="Controls_Common_AdministratorChatControl" %>
<link href="../../App_Themes/Default/chat-theme.css" rel="stylesheet" />


<script type="text/javascript">
    var chatHub = null;

    function startConversation() {
        console.log("Starting conversation!!!");

        chatHub = $.connection.chatHub;
        console.log(chatHub);
        chatHub.client.sendMessage = chutHub_sendMessage;
        chatHub.client.updateConnectedUsers = updateConnectedUsers;

        $.connection.hub.start().done(chatProcess_start);

        chatHub.client.disconnected = function (name) {
            $("#onlineusers").remove("<option value=" + name + ">" + name + "</option>");
        }


        chatHub.client.online = function (name) {
            if (name != $('#nickname').val()) {
                var newOption = "<option value=" + name + ">" + name + "</option>";
                $("#onlineusers").append(newOption);
            }
        };

        return false;
    };



    function updateConnectedUsers(users) {
        $('#onlineusers').empty();

        console.log("adding user");

        for (var i = 0; i < users.length; i++) {
            var name = users[i];
            console.log("adding user - " + name);

            if (name != $('#txtName').val()) {
                $('#onlineusers').append("<option value=" + name + ">" + name + "</option>");
            }
        }

    }

    function send() {
        console.log("sending message!!!");

        var message = $("#txtMessage").val();
        var name = $("#txtName").val();

        var li = "<li> Me: " + message + "</li>";
        $("#ulChat").append(li);

        var client = $("#onlineusers").val();
        console.log("sending message:" + client);
        chatHub.server.send(client, message, name);
        return false;
    }

    function chatProcess_start() {
        if (chatHub != null) {
            var groupName = $("#txtName").val();
            console.log(groupName);
            chatHub.server.startConversation(groupName);

            $("#dvMessagenger").show();
            $("#dvConversation").hide();

            chat.server.notify($('#txtName').val(), $.connection.hub.id);
        } else {
            console.log("chatHub is null!!!");
        }
        return false;
    }


    function chutHub_sendMessage(name, from, message) {
        console.log("sssqewqewqeqw");
        var capt = from;
        if (capt.length > 2) {
            capt = capt.substring(0, 2);
        }
        var li = "<li> " + capt + ": " + message + "</li>";
        $("#ulChat").append(li);
        return false;
    }



</script>

<input type="hidden" id="nickname" />

<div id="dvConversation">
    <div class="container">
        <div class="row">
            <p>
                <input id="txtName" class="form-control input-lg" placeholder="სახელი" style="width: 200px" />
            </p>
            <p>
                <input type="button" id="btnStart" class="btn btn-primary" onclick="startConversation()" value="დაწყება">
            </p>
        </div>
    </div>
</div>

<div id="dvMessagenger" style="display: none;">
    <div class="container">
        <div class="row">

            <select id="onlineusers">
            </select>
        </div>
        <table>
            <tr>
                <td>
                    <ul class="chat" id="ulChat">
                    </ul>
                </td>
            </tr>
            <tr>
                <td>
                    <input id="txtMessage" type="text"  placeholder="Type your message here..." />

                </td>
            </tr>
            <tr>
                <td>
                    <input type="button" id="btn-chat" value="გაგზავნა" onclick="send()" />
                </td>
            </tr>
        </table>
    </div>
</div>
