using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLib.Decorator;

namespace ClassLib.Factory_Method
{
    public class Worker : Robot
    {
        public Worker(string image)
            : base(image, "Worker", "Highest payload and battery, 10% decode cargo", 100, 200)
        {

        }

        protected override bool IsDamagable()
        {
            return false;
        }

        protected override bool TryDecrypt(Stone stone)
        {
            return  new Random().Next(11) == 0;
        }
    }
}