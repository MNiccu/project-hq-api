using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hq_api.Hubs
{

    public interface IClient
    {
        public Task ReceiveMessage(string username, string message);
        public Task ConfirmLogin(string message);
        
        
    }

    public class ChatHub : Hub<IClient>
    {

        public class ChatMessage
        {
            public string User { get; set; }
            public string Message { get; set; }
        }


        public async Task SendMessage(ChatMessage message)
        {
            await Clients.All.ReceiveMessage(message.User, message.Message);
        }

        public async Task Login(string username)
        {
            //gets username
            //check it?
            //save to hub context
            //string computerName = Clients.Caller.computerName;
            this.Context.Items["username"] = username;
            Console.WriteLine("called succesfully");
        }

        public async Task CheckLogin() {
            await Clients.Caller.ConfirmLogin((string)this.Context.Items["username"]);
        }


        public Task JoinGroup(string group)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }
        public async Task SendMessageToGroup(string group, ChatMessage message)
        {
            await Clients.Group(group).ReceiveMessage(message.User, message.Message);
        }
    }
}