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
        protected Random Random { get; }
        protected List<Stone> StonesToDrop { get; private set; }

        public Robot(string image, string name, string desc, double battery, double maxw) : base(Guid.NewGuid(),
            image, name, desc, battery, maxw, 0, 100, new List<Stone>())
        {
            Random = new Random();
            StonesToDrop = new List<Stone>();
        }

        public abstract bool CanBeDamagedByStone { get; }

        protected abstract bool CanDecrypt();

        public string GetInfo()
        {
            string info = $"Id: {Id}, image: {RobotImageBase64}, name: {Name}" +
                          $", description: {Description}, battery charge: {BatteryCharge}";

            if(CanBeDamagedByStone) info += $", health: {Health}";

            info += $", max weight: {MaxWeight}, current weight: {CurrentWeight}" +
                    $", baggage count: {Baggage.Count}";

            return info;
        }

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

        protected virtual void ActionAfterTurn()
        { }

        public string Turn()
        {
            string res = "";
            int health = Health;
            double battery = BatteryCharge;
            foreach (var item in Baggage)
            {
                if (CanBeDamagedByStone && item.Damage > 0)
                    Health -= item.Damage;
                if (item.Collapses)
                {
                    if (item.StoneHealth > 1)
                        item.DecreaseHealth();
                    else
                        res += DropCollapsedStone(item);
                }
                BatteryCharge -= item.Weight * 0.1;
            }

            foreach (var item in StonesToDrop)
            {
                CurrentWeight -= item.Weight;
                Baggage.Remove(item);
            }

            BatteryCharge--;

            ActionAfterTurn();

            string info = "battery charge: " + (BatteryCharge >= 0 ? BatteryCharge : 0)
                   + ", battery lost: " + (battery - BatteryCharge) + ", " + res;

            if (CanBeDamagedByStone)
            {
                info = $"turns harm: {health - Health}, {info}";
            }

            info = char.ToUpper(info[0]) + info.Substring(1);

            return info;
        }

        public bool IsAlive()
        {
            return Health > 0 && BatteryCharge > 0;
        }

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

        public string FinalInfo()
        {
            double cost = Baggage.Sum(item => item.GetCost());

            string info = $"Total baggage cost: {cost}, battery level: {(BatteryCharge > 0 ? BatteryCharge : 0)}";

            if (CanBeDamagedByStone) info += ", health: " + Health;

            info += '.';

            return info;
        }
    }
}
