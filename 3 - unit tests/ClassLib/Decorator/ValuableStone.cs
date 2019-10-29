using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Decorator
{
    public class ValuableStone : Stone
    {
        public ValuableStone(double weight) : base(0, "Valuable stone", weight, false, 0, false)
        {

        }

        public override void DecreaseHealth()
        {

        }

        public override double GetCost()
        {
            return Weight;
        }

        public override string GetInfo()
        {
            string res = "Cost: " + GetCost() + ", weight: " + Weight + ", description: " + Description;
            return res;
        }

        public override object Clone()
        {
            return new ValuableStone(Weight);
        }
    }
}
