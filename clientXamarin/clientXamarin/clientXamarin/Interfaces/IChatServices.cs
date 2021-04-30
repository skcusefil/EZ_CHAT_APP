using clientXamarin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace clientXamarin.Interfaces
{
    public interface IChatServices
    {
        Task<bool> Connect(string otherUsername);
        Task Stop();
        Task SendMessage(string otherUsername, string message); 
    }
}
