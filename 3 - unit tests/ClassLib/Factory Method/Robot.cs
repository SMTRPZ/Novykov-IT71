using ClassLib.Decorator;
using ClassLib.Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Factory_Method
{
    public abstract class Robot : RobotInfo
    {
        protected List<Stone> StonesToDrop { get; private set; }

        public Robot(string image, string name, string desc, double battery, double maxw) : base(Guid.NewGuid(),
            image, name, desc, battery, maxw, 0, 100, new List<Stone>())
        {
            StonesToDrop = new List<Stone>();
        }

        public abstract string GetInfo();

        public abstract string AddStone(params Stone[] stone);

        public abstract string DropStone();

        public abstract string GetBaggageInfo();

        public abstract string Turn();

        public abstract bool IsAlive();

        internal string DropCollapsedStone(Stone st)
        {
            string info = "Was dropped(collapse destroy it). " + Baggage.FirstOrDefault(x => x == st).GetInfo();
            StonesToDrop.Add(st);
            return info;
        }

        public RobotMemento SaveState()
        {
            return new RobotMemento(this);
        }

        public string RestoreState(RobotMemento robotMemento)
        {
            base.RestoreState(robotMemento);
            return "State successfully restored. " + GetInfo() + "\r\n";
        }

        public abstract string FinalInfo();
    }
}
