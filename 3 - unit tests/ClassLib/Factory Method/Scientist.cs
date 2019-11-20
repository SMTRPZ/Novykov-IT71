﻿using ClassLib.Decorator;
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

        protected override bool IsDamagable()
        {
            return false;
        }

        protected override bool TryDecrypt(Stone stone)
        {
            return true;
        }
    }
}
