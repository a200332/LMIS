<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdministratorChatControl.ascx.cs" Inherits="Controls_Common_AdministratorChatControl" %>

<script type="text/javascript">
    var chatHub = null;

    $(document).ready(function () {
        console.log("sssss");
        startConversation();
    });

    function startConversation() {
        console.log("Starting conversation!!!");

        chatHub = $.connection.chatHub;
        console.log(chatHub);
        chatHub.client.sendMessage = chutHub_sendMessage;
        chatHub.client.updateConnectedUsers = updateConnectedUsers;
        chatHub.client.disconnected = chutHub_disconnected;


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

    function chutHub_disconnected(user) {

        console.log("removing user:" + user);
        $("#onlineusers option[value=" + user + "]").remove();
    }


    function updateConnectedUsers(users) {
        $('#onlineusers').empty();

        console.log("adding user");

        for (var i = 0; i < users.length; i++) {
            var name = users[i];
            console.log("adding user - " + name);

            if (name != $('#currentUsername').val()) {
                $('#onlineusers').append("<option value=" + name + ">" + name + "</option>");
            }
        }

    }

    

    function send() {

        if (chatHub == null) {
            startConversation();
        }

        console.log("sending message!!!");

        var message = $("#txtMessage").val();
        var name = $("#currentUsername").val();


        if (message != null && message.length === 0) {
            return false;
        }

        var li = "<li> Me: " + message + "</li>";
        $("#ulChat").append(li);

        $("#txtMessage").val('');

        var client = $("#onlineusers").val();
        console.log("sending message:" + client);
        chatHub.server.send(client, message, name);
        return false;
    }

    function chatProcess_start() {
        if (chatHub != null) {
            var groupName = $("#currentUsername").val();
            console.log(groupName);
            chatHub.server.startConversation(groupName);

            $("#dvMessagenger").show();
            $("#dvConversation").hide();

            chat.server.notify($('#currentUsername').val(), $.connection.hub.id);
        } else {
            console.log("chatHub is null!!!");
        }
        return false;
    }


    function chutHub_sendMessage(name, from, message) {
        var capt = from;
        if (capt.length > 2) {
            capt = capt.substring(0, 2);
        }
        var li = "<li> " + capt + ": " + message + "</li>";
        $("#ulChat").append(li);
        return false;
    }

    function handle(e) {
        if (e.keyCode === 13) {
            send();
        }
    }

</script>

<input type="hidden" id="nickname" />
<input type="hidden" id="currentUsername" value="Administrator" />

<div id="dvConversation">
    <table>
        <tr>
            <td>
                <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/images/start.png" OnClientClick="startConversation(); return false;" ToolTip="Start conversation" />
            </td>
        </tr>
    </table>
</div>

<div id="chat-body" class="hide">
    <div id="dvMessagenger" style="display: none;">
        <div class="right-menu">
            <table>
                <tr>
                    <td>
                        <select id="onlineusers">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <ul class="chat" id="ulChat" style="display: block; padding: 5px; margin-top: 5px; height: 250px; overflow: auto;">
                        </ul>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="txtMessage" type="text" placeholder="Message..." style="font-size: 11px; width: 180px; height: 24px; border: 1px gray solid;" onkeydown="handle(event);" />
                    </td>
               
                    <td>
                        <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/images/send.png" OnClientClick="send(); return false;" ToolTip="Send message" />
                    </td>
                   
                </tr>
            </table>
        </div>
    </div>
</div>
