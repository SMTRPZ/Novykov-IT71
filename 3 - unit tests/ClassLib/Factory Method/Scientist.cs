using ClassLib.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Factory_Method
{
    public class Scientist : Robot
    {
        public Scientist(string image)
            : base(image, "Scientist", "Lowest payload and battery, 100% decode cargo.", 50, 100)
        {

        }

        public override bool CanBeDamagedByStone => false;

        protected override bool CanDecrypt()
        {
            return true;
        }

        public override string GetInfo()
        {
            return "Id: " + Id + ", image: " + RobotImageBase64 + ", name: " + Name + ", description: " + Description
                + ", battery charge: " + BatteryCharge + ", max weight: " + MaxWeight + ", current weight: " + CurrentWeight
                + ", baggage count: " + Baggage.Count;
        }
    }
}
