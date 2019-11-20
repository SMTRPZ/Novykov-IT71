using ClassLib.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Memento
{
    public class GameMemento
    {
        public int MoveCounter { get; protected set; }

        public Map Map { get; protected set; }

        public RobotMemento RobotMemento { get; private set; }

        public GameMemento(RobotMemento memento, int moveCounter, Map map)
        {
            RobotMemento = memento;
            MoveCounter = moveCounter;
            Map = map;
        }

        public RobotMemento GetRobotMemento()
        {
            return new RobotMemento(RobotMemento.RobotInfo, RobotMemento.BatteryCharge,
                RobotMemento.MaxWeight, RobotMemento.CurrentWeight, RobotMemento.Health, RobotMemento.Baggage);
        }
    }
}
