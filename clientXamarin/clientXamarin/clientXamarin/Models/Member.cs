using System;
using System.Collections.Generic;
using System.Text;

namespace clientXamarin.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string PhotoUrl { get; set; }
        //public ICollection<PhotoDto> Photos { get; set; } = new List<PhotoDto>();
    }
}
