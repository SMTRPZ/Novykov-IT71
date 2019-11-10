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
        public RobotMemento RobotMemento { get; }
        public int MoveCounter { get; protected set; }

        public Map Map { get; protected set; }

        public GameMemento(RobotMemento memento, int moveCounter, Map map)
        {
            RobotMemento = memento;
            MoveCounter = moveCounter;
            Map = map;
        }
    }
}
