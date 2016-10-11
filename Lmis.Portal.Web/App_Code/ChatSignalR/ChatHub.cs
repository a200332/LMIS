using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using CITI.EVO.Tools.Extensions;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

[HubName("chatHub")]
public class ChatHub : Hub
{
    private static readonly ConcurrentDictionary<String, String> clientsDictionary = new ConcurrentDictionary<String, String>();

    public void StartConversation(string name)
    {
        Groups.Add(Context.ConnectionId, name);
        clientsDictionary.TryAdd(name, Context.ConnectionId);

        var users = clientsDictionary.Select(n => n.Key).ToList();
        Clients.All.updateConnectedUsers(users);
    }

    public void Send(string name, string message, string from)
    {
        var key = clientsDictionary.GetValueOrDefault(name);
        if (key != null)
        {
            Clients.Client(key).sendMessage(name, from, message);
        }
    }

    public override Task OnDisconnected(bool stopCalled)
    {
        var name = clientsDictionary.FirstOrDefault(x => x.Value == Context.ConnectionId.ToString());
        if (name.Key != null)
        {
            string s;
            clientsDictionary.TryRemove(name.Key, out s);
            return Clients.All.disconnected(name.Key);
        }

	    return null;// Task.CompletedTask;
    }
}
