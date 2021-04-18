using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Group
    {
        //Entity framework need empty constructor for creating table 
        public Group()
        {

        }

        public Group(string name)
        {
            this.Name = name;

        }

        [Key]
        public string Name { get; set; } //Just use group name as primary key 
        public ICollection<Connection> Connections { get; set; } = new List<Connection>();
    }
}
