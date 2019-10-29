using ClassLib.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Memento
{
    public class RobotMemento
    {
        public Guid Id { get; protected set; }

        public string RobotImageBase64 { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public double BatteryCharge { get; protected set; }

        public double MaxWeight { get; protected set; }

        public double CurrentWeight { get; protected set; }

        public int Health { get; protected set; }

        public List<Stone> Baggage { get; protected set; }

        public RobotMemento(Guid id, string image, string name, string desc, double batcharge, double maxw, double curw, int health, List<Stone> baggage)
        {
            Id = id;
            RobotImageBase64 = image;
            Name = name;
            Description = desc;
            BatteryCharge = batcharge;
            MaxWeight = maxw;
            Baggage = baggage;
            CurrentWeight = curw;
            Health = health;
        }
    }
}
