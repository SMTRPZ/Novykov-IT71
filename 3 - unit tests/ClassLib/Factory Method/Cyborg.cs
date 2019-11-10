using ClassLib.Decorator;
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

        public override bool CanBeDamagedByStone => true;

        protected override bool CanDecrypt()
        {
            return Random.Next(5) < 3;
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

            foreach (var item in StonesToDrop)
            {
                CurrentWeight -= item.Weight;
                Baggage.Remove(item);
            }

            BatteryCharge--;
            if (CurrentWeight > MaxWeight * 0.8)
            {
                Random rnd = new Random();
                if (rnd.Next(11) < 3)
                {
                    BatteryCharge = 0;
                }
            }
            return "Turns harm: " + (health - Health) + ", battery charge: " + (BatteryCharge >= 0 ? BatteryCharge : 0) + ", battery lost: " + (battery - BatteryCharge) + ", " + res;
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
