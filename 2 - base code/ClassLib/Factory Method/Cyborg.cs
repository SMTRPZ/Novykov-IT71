using ClassLib.Decorator;
using ClassLib.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Factory_Method
{
    public class Cyborg : Robot
    {
        public Cyborg(string image)
            : base(image, "Cyborg", "Medium payload and battery, 60% decode cargo. Can die from poisoned stones." +
                  " After 80% payload have 30% change to drop battery", 70, 135)
        {
            
        }

        public override string AddStone(params Stone[] stone)
        {
            Stone st = stone[0];
            if (st.Weight + CurrentWeight <= MaxWeight)
            {
                if (st.Damage < Health)
                {
                    if (st.Decryption)
                    {
                        Random random = new Random();
                        if (random.Next(5) < 3)
                        {
                            Baggage.Add(st);
                            return st.GetInfo() + "\r\n Succesfully decrypted and added";
                        }
                        else
                        {
                            return "Decryption failed";
                        }
                    }
                    else
                    {
                        Baggage.Add(st);
                        return st.GetInfo() + "\r\n Succesfully added";
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

        public override string DropStone()
        {
            if (Baggage.Count > 0)
            {
                string info = Baggage[Baggage.Count - 1].GetInfo();
                Baggage.RemoveAt(Baggage.Count - 1);
                return "Succesfully droped: " + info;
            }
            return "Baggage already empty";
        }

        public override string GetBaggageInfo()
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
            return "Total weight: " + weight + ", total cost: " + cost + ", free space: " + (MaxWeight - weight)
                + ", damage per turn: " + damage + ", current hp: " + Health;
        }

        public override string GetInfo()
        {
            return "Id: " + Id + ", image: " + RobotImageBase64 + ", name: " + Name + ", description: " + Description
                + ", battery charge: " + BatteryCharge + ", health: " + Health + ", max weight: " + MaxWeight + ", current weight: " + CurrentWeight
                + ", baggage count: " + Baggage.Count;
        }

        public override string Turn()
        {
            string res = "";
            int health = Health;
            double battery = BatteryCharge;
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
            BatteryCharge--;
            if (CurrentWeight * 0.8 > MaxWeight)
            {
                Random rnd = new Random();
                if (rnd.Next(11) < 3)
                {
                    BatteryCharge = 0;
                }
            }
            return "Turns harm: " + (health - Health) + ", battery charge: " + BatteryCharge + ", battery lost: " + (battery - BatteryCharge) + ", " + res;
        }

        public override bool IsAlive()
        {
            if (Health > 0)
                if (BatteryCharge > 0)
                    return true;
            return false;
        }

        public override string FinalInfo()
        {
            double cost = 0;
            foreach (var item in Baggage)
            {
                cost += item.GetCost();
            }
            return "Total baggage cost: " + cost + ", battery level: " + (BatteryCharge > 0 ? BatteryCharge : 0) + ", health: " + Health + ".";
        }
    }
}
