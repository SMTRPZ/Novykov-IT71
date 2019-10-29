using ClassLib.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Memento
{
    public class GameMemento : RobotMemento
    {
        public int MoveCounter { get; protected set; }

        public Map Map { get; protected set; }

        public GameMemento(RobotMemento memento, int moveCounter, Map map) 
            : base(memento.Id, memento.RobotImageBase64, memento.Name, memento.Description, 
                  memento.BatteryCharge, memento.MaxWeight, memento.CurrentWeight, memento.Health, memento.Baggage)
        {
            MoveCounter = moveCounter;
            Map = map;
        }

        public RobotMemento GetRobotMemento()
        {
            return new RobotMemento(Id, RobotImageBase64, Name, Description, BatteryCharge, MaxWeight, CurrentWeight, Health, Baggage);
        }
    }
}
