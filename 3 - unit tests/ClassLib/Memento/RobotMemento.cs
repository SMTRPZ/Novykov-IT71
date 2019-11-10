using ClassLib.Decorator;
using ClassLib.Factory_Method;
using System;
using System.Collections.Generic;

namespace ClassLib.Memento
{
    public class RobotMemento : RobotInfo
    {
        public  RobotMemento(Robot robot) : base(robot)
        { }

        public RobotMemento(Guid id, string image, string name, string desc,
            double charge, double maxw, double curw, int health, List<Stone> baggage) :
            base(id, image, name, desc, charge, maxw, curw, health, baggage)
        { }
    }
}
