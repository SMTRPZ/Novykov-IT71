using ClassLib.Decorator;
using ClassLib.Memento;
using ClassLib.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ClassLib.Factory_Method
{
    public abstract class Robot
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

        public Robot(string image, string name, string desc, double battery, double maxw)
        {
            Id = Guid.NewGuid();
            RobotImageBase64 = image;
            Name = name;
            Description = desc;
            BatteryCharge = battery;
            MaxWeight = maxw;
            Baggage = new List<Stone>();
            CurrentWeight = 0;
            Health = 100;
        }

        public abstract string GetInfo();

        public abstract string AddStone(params Stone[] stone);

        public abstract string DropStone();

        public abstract string GetBaggageInfo();

        public abstract string Turn();

        public abstract bool IsAlive();

        internal string DropCollapsedStone(Stone st)
        {
            string info = "Was droped(collapse destroy it). " + Baggage.FirstOrDefault(x => x == st).GetInfo();
            Baggage.Remove(st);
            return info;
        }

        public RobotMemento SaveState()
        {
            List<Stone> baggage = new List<Stone>();
            foreach (var item in Baggage)
            {
                baggage.Add((Stone)item.Clone());
            }
            return new RobotMemento(Id, RobotImageBase64, Name, Description, BatteryCharge, MaxWeight, CurrentWeight, Health, baggage);
        }

        public string RestoreState(RobotMemento robotMemento)
        {
            Id = robotMemento.Id;
            RobotImageBase64 = robotMemento.RobotImageBase64;
            Name = robotMemento.Name;
            Description = robotMemento.Description;
            BatteryCharge = robotMemento.BatteryCharge;
            MaxWeight = robotMemento.MaxWeight;
            Baggage = robotMemento.Baggage;
            CurrentWeight = robotMemento.CurrentWeight;
            Health = robotMemento.Health;
            return "State succesfully restored. " + GetInfo() + "\r\n";
        }

        public abstract string FinalInfo();
    }
}
