<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PublicChatControl.ascx.cs" Inherits="Controls_Common_PublicChatControl" %>

<script type="text/javascript">
    var chatHub = null;

    function startConversation() {
        console.log("Startingc conversation!!!");

        chatHub = $.connection.chatHub;
        console.log(chatHub);
        chatHub.client.sendMessage = chutHub_sendMessage;

        $.connection.hub.start().done(chatProcess_start);

        return false;
    };

    function send() {
        console.log("sending message!!!");
        var message = $("#txtMessage").val();

        if (message.length === 0) {
            return false;
        }

        $("#txtMessage").val('');
        var from = $("#txtName").val();
        var name = $("#lblAdmiName").val();



        var li = "<li> Me: " + message + "</li>";
        $("#ulChat").append(li);

        chatHub.server.send(name, message, from);
        return false;
    }

    function chatProcess_start() {
        if (chatHub != null) {
            var groupName = $("#txtName").val();
            console.log(groupName);
            chatHub.server.startConversation(groupName);

            var li = "<li>მოგესალმებით, გთხოვთ დაელოდოთ, დაკავშირება...</li>";
            $("#ulChat").append(li);


            $("#dvMessagenger").show();
            $("#dvConversation").hide();
        } else {
            console.log("chatHub is null!!!");
        }
        return false;
    }

    function disconnect() {
        if (chatHub != null) {
            console.log("disconnecting");
            $.connection.hub.stop();
        }
    }


    function chutHub_sendMessage(name, from, message) {
        $("#lblAdmiName").val(from);

        console.log("received message from: " + from);
        var li = "<li> " + from + ": " + message + "</li>";
        $("#ulChat").append(li);
        return false;
    }

    $(document).on('focus', '.panel-footer input.chat_input', function (e) {
        var $this = $(this);
        if ($('#minim_chat_window').hasClass('panel-collapsed')) {
            $this.parents('.panel').find('.panel-body').slideDown();
            $('#minim_chat_window').removeClass('panel-collapsed');
            $('#minim_chat_window').removeClass('glyphicon-plus').addClass('glyphicon-minus');
        }
    });

    function handle(e) {
        if (e.keyCode === 13) {
            send();
        }
    }

</script>
<div id="chat-body" class="hide">
    <div id="dvMessagenger" style="display: none;">
        <div class="row">
            <input type="hidden" id="lblAdmiName" />
        </div>
        <table>
            <tr>
                <td>
                    <ul class="chat" id="ulChat" style="display: block; padding: 5px; margin-top: 5px; height: 250px; overflow: auto;">
                    </ul>
                </td>
            </tr>
            <tr>
                <td>

                    <input id="txtMessage" type="text" placeholder="Message..." style="font-size: 11px; width: 180px; height: 24px; border: 1px gray solid;" onkeydown = "handle(event);"     />
                </td>
          
                <td>
                    <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/images/send.png" OnClientClick="send(); return false;" ToolTip="Send message" />
                </td><td>
                        <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/images/chat-close.png" OnClientClick="disconnect(); return false;" ToolTip="Leave Conversation" />
                    </td>
            </tr>
        </table>
    </div>

    <div id="dvConversation">
        <table>
            <tr>
                <td>
                    <input id="txtName" type="text" placeholder="Name" style="width:180px" />

                </td>
           
                <td>
                    <ce:ImageLinkButton runat="server" DefaultImageUrl="~/App_Themes/Default/images/start.png" OnClientClick="startConversation(); return false;" ToolTip="Start conversation" />
                </td>
                 
            </tr>
        </table>
    </div>
</div>
