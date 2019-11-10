using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Decorator
{
    public class OrdinaryStone : Stone
    {
        public OrdinaryStone() : base(0, "Ordinary stone, nothing cost", 0, false, 0, false)
        {

        }

        public override double GetCost()
        {
            return 0;
        }

        public override string GetInfo()
        {
            string res = "Description: " + Description;
            return res;
        }

        public override object Clone()
        {
            return new OrdinaryStone();
        }
    }
}
