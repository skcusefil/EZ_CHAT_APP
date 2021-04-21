using clientXamarin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace clientXamarin.Interfaces
{
    public interface IChatServices
    {
        Task Connect();
        Task SendMessage(ChatMessage message, string groupName);
        Task AddToGroup(string groupName);
        event EventHandler<ChatMessage> OnMessageReceived;
    }
}
