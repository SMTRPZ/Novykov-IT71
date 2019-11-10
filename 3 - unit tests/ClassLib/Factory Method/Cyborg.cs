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

        protected override void ActionAfterTurn()
        {
            if (CurrentWeight > MaxWeight * 0.8 &&
                Random.Next(11) < 3)
            {
                BatteryCharge = 0;
            }
        }
    }
}
