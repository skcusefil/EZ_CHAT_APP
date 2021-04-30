using clientXamarin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace clientXamarin.Interfaces
{
    public interface IMemberService
    {
        Task<Member> GetMember(string username);
        Task<bool> EditMember(string displayName);
    }
}
