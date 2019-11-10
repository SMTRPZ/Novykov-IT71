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
            Random = new Random();
            StonesToDrop = new List<Stone>();
        }

        public abstract string GetInfo();
        protected Random Random { get; }

        public abstract bool CanBeDamagedByStone { get; }

        protected abstract bool CanDecrypt();

        public string AddStone(Stone stone)
        {
            if (stone.Weight + CurrentWeight <= MaxWeight)
            {
                if (!CanBeDamagedByStone || stone.Damage < Health)
                {
                    if (stone.Decryption)
                    {
                        if (CanDecrypt())
                        {
                            Baggage.Add(stone);
                            CurrentWeight += stone.Weight;
                            return stone.GetInfo() + "\r\n Succesfully decrypted and added";
                        }
                        else
                        {
                            return "Decryption failed";
                        }
                    }
                    else
                    {
                        Baggage.Add(stone);
                        CurrentWeight += stone.Weight;
                        return stone.GetInfo() + "\r\n Succesfully added";
                    }
                }
                else
                {
                    return "You are dead (9((9";
                }
            }
            else
            {
                return "You can`t lift this stone. Robot overload";
            }
        }

        public string DropStone()
        {
            if (Baggage.Count == 0) return "Baggage already empty";
            int lastIndex = Baggage.Count - 1;
            string info = Baggage[lastIndex].GetInfo();
            CurrentWeight -= Baggage[lastIndex].Weight;
            Baggage.RemoveAt(lastIndex);
            return "Succesfully droped: " + info;
        }

        public string GetBaggageInfo()
        {
            double cost = 0;
            double weight = 0;
            int damage = 0;

            foreach (var item in Baggage)
            {
                cost += item.GetCost();
                weight += item.Weight;
                damage += item.Damage;
            }

            string info = $"Total weight: {weight}, total cost: {cost}, free space: {MaxWeight - weight}";

            if (CanBeDamagedByStone) info += $", damage per turn: {damage}, current hp: {Health}";

            return info;
        }

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
