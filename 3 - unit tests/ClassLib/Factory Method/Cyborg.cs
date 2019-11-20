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

        protected override bool TryDecrypt(Stone stone)
        {
            return new Random().Next(5) < 3;
        }


        protected override bool IsDamagable()
        {
            return true;
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

    }
}
