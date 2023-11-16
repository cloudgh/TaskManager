using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Model
{
    
    public class Employers
    {
        public Employers(int id, string name, string lastname, string post)
        {
            Id = id;
            Name = name;
            Lastname = lastname;
            Post = post;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Post { get; set; }

        public string FullEmployers => $"{Name} {Lastname}-{Post}";
    }
}
