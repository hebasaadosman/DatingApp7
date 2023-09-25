

using System.ComponentModel.DataAnnotations;

namespace API.Entities
{




    public class Group
    {
        public Group()
        {

        }
        public Group(string name)
        {
            Name = name;
        }


        [Key]//data annotation to make this the primary key
        public string Name { get; set; }
        public ICollection<Connection> Connections { get; set; } = new List<Connection>();

    }
}