using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_hq_api.Hubs
{
    public class ChatHub : Hub
    {
        public class ChatMessage
        {
            public string User { get; set; }
            public string Message { get; set; }
        }
        
        public async Task SendMessage(ChatMessage message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message.User, message.Message);
        }
    }
}