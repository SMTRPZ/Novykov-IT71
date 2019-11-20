using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Factory_Method
{
    public class RobotInfo
    {
        public RobotInfo(Guid id, string robotImageBase64, string name, string description)
        {
            Id = id;
            RobotImageBase64 = robotImageBase64;
            Name = name;
            Description = description;
        }

        public Guid Id { get; protected set; }

        public string RobotImageBase64 { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }
    }
}
