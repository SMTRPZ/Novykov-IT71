using ClassLib.Decorator;
using ClassLib.Factory_Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Memento
{
    public class RobotMemento
    {
        public RobotInfo RobotInfo { get; private set; }

        public double BatteryCharge { get; protected set; }

        public double MaxWeight { get; protected set; }

        public double CurrentWeight { get; protected set; }

        public int Health { get; protected set; }

        public List<Stone> Baggage { get; protected set; }

        public RobotMemento(RobotInfo info, double batcharge, double maxw, double curw, int health, List<Stone> baggage)
        {
            RobotInfo = info;
            BatteryCharge = batcharge;
            MaxWeight = maxw;
            Baggage = baggage;
            CurrentWeight = curw;
            Health = health;
        }
    }
}
