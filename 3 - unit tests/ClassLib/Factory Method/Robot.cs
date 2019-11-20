using ClassLib.Decorator;
using ClassLib.Memento;
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
        public RobotInfo RobotInfo { get; private set; }

        public double BatteryCharge { get; protected set; }

        public double MaxWeight { get; protected set; }

        public double CurrentWeight { get; protected set; }

        public int Health { get; protected set; }

        public List<Stone> Baggage { get; protected set; }

        protected List<Stone> StonesToDrop { get; private set; }

        public Robot(string image, string name, string desc, double battery, double maxw)
        {
            RobotInfo = new RobotInfo(Guid.NewGuid(), image, name, desc);
            BatteryCharge = battery;
            MaxWeight = maxw;
            Baggage = new List<Stone>();
            StonesToDrop = new List<Stone>();
            CurrentWeight = 0;
            Health = 100;
        }


        protected abstract bool TryDecrypt(Stone stone);
        protected abstract bool IsDamagable();

        public string GetInfo() {
            string res = "Id: " + RobotInfo.Id + ", image: " + RobotInfo.RobotImageBase64 + ", name: " + RobotInfo.Name + ", description: " + RobotInfo.Description
                + ", battery charge: " + BatteryCharge;
            if (IsDamagable())
            {
                res += ", health: " + Health;
            }
            res += ", max weight: " + MaxWeight + ", current weight: " + CurrentWeight + ", baggage count: " + Baggage.Count;
            return res;
        }

        public string AddStone(Stone stone) {
            if(stone.Weight + CurrentWeight > MaxWeight)
                return "You can`t lift this stone. Robot overload";
            if(stone.Damage >= Health)
                return "You are dead (9((9";


            if (stone.Decryption)
            {
                if (TryDecrypt(stone))
                {
                    Baggage.Add(stone);
                    CurrentWeight += stone.Weight;
                    return stone.GetInfo() + "\r\n Succesfully decrypted and added";
                };
                return "Decryption failed";

            }
            else
            {
                Baggage.Add(stone);
                CurrentWeight += stone.Weight;
                return stone.GetInfo() + "\r\n Succesfully added";
            }

           
            return "Decryption failed";

        }


        public string DropStone()
        {
            if (Baggage.Count > 0)
            {
                string info = Baggage[Baggage.Count - 1].GetInfo();
                CurrentWeight -= Baggage[Baggage.Count - 1].Weight;
                Baggage.RemoveAt(Baggage.Count - 1);
                return "Succesfully droped: " + info;
            }
            return "Baggage already empty";
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
            string res = "Total weight: " + weight + ", total cost: " + cost + ", free space: " + (MaxWeight - weight);
            if (IsDamagable())
            {
                res += ", damage per turn: " + damage + ", current hp: " + Health;
            }
            return res;
        }

        public virtual string Turn()
        {
            string res = "";
            string resStart = "";
            double battery = BatteryCharge;
            int health = Health;

            foreach (var item in Baggage)
            {
                if (item.Damage > 0)
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
            if (IsDamagable())
            {
                resStart = "Turns harm: " + (health - Health);
                if (CurrentWeight > MaxWeight * 0.8)
                {
                    Random rnd = new Random();
                    if (rnd.Next(11) < 3)
                    {
                        BatteryCharge = 0;
                    }
                }
            }
            string resRest = "attery charge: " + (BatteryCharge >= 0 ? BatteryCharge : 0) + 
                ", battery lost: " + (battery - BatteryCharge) + ", " + res;

            return resStart.Length > 0 ? resStart + ", b" :
                "B" + resRest;
        }

        public bool IsAlive()
        {
            return BatteryCharge > 0 && IsDamagable() ? Health > 0 : true;
        }

        internal string DropCollapsedStone(Stone st) //??????
        {
            string info = "Was droped(collapse destroy it). " + Baggage.FirstOrDefault(x => x == st).GetInfo();
            StonesToDrop.Add(st);
            return info;
        }

        public RobotMemento SaveState()
        {
            List<Stone> baggage = new List<Stone>();
            foreach (var item in Baggage)
            {
                baggage.Add((Stone)item.Clone());
            }
            return new RobotMemento(RobotInfo, BatteryCharge, MaxWeight, CurrentWeight, Health, baggage);
        }

        public string RestoreState(RobotMemento robotMemento)
        {
            RobotInfo = robotMemento.RobotInfo;
            BatteryCharge = robotMemento.BatteryCharge;
            MaxWeight = robotMemento.MaxWeight;
            Baggage = robotMemento.Baggage;
            CurrentWeight = robotMemento.CurrentWeight;
            Health = robotMemento.Health;
            return "State succesfully restored. " + GetInfo() + "\r\n";
        }

        public string FinalInfo()
        {
            double cost = 0;
            foreach (var item in Baggage)
            {
                cost += item.GetCost();
            }
            string res = "Total baggage cost: " + cost + ", battery level: " + (BatteryCharge > 0 ? BatteryCharge : 0);
            if (IsDamagable())
            {
                res += ", health: " + Health;
            }
            res += ".";
            return res;
        }
    }
}
