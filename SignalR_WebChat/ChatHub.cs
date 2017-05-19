using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace SignalR_WebChat
{
    [HubName("chat")]
    public class ChatHub : Hub
    {
        public ChatHub() //: this(IHub)
        {

        }
        public void SendMessage(string message)
        {
            /*//both are equivalent
            Clients.Caller.newMessage(message); //equivalent
            Clients.Client(Context.ConnectionId).newMessage(message); //equivalent */

            var msg = $"{Context.ConnectionId.ToString()} {message.ToString()}";
            Clients.All.newMessage(msg);
        }
        public void JoinRoom(string room)
        {
            //Groups are not persisted on the server
            Groups.Add(Context.ConnectionId, room);
        }

        public void SendMessageToRoom(string room, string message)
        {
            var msg = $"{Context.ConnectionId.ToString()}: {message.ToString()}";
            if(room != "")
            {
                Clients.Group(room).newMessage(msg);
            }

        }

        public override Task OnConnected()
        {
            SendMonitoringData("Connected", Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            SendMonitoringData("Disconnected", Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            SendMonitoringData("Reconnected", Context.ConnectionId);
            return base.OnReconnected();
        }

        private void SendMonitoringData(string eventType, string connection)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MonitorHub>();
            context.Clients.All.newEvent(eventType, connection);
        }

        public void SendMessageData(SendData data)
        {
            //process incoming data..
            //transform data..
            //craft new data...

            Clients.All.newData(data);
        }

        public Task JoinGroupAsync(string groupName)
        {
            return Groups.Add(Context.ConnectionId, groupName);
        }

        public Task LeaveGroupAsync(string groupName)
        {
            return Groups.Remove(Context.ConnectionId, groupName);
        }

    }
    public class SendData
    {
        public int Id { get; set; }
        public string Data { get; set; }
    }
}