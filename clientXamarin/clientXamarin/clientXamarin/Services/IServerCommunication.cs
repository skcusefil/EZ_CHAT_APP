using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace clientXamarin.Services
{
    public interface IServerCommunication
    {
        Task<string> GetFromServerAsync(string URL);

    }
}
