﻿using System;
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

        public override bool CanBeDamagedByStone => false;

        protected override bool CanDecrypt()
        {
            return Random.Next(11) == 0;
        }

        public override string GetInfo()
        {
            return "Id: " + Id + ", image: " + RobotImageBase64 + ", name: " + Name + ", description: " + Description
                + ", battery charge: " + BatteryCharge + ", max weight: " + MaxWeight + ", current weight: " + CurrentWeight
                + ", baggage count: " + Baggage.Count;
        }

        public override bool IsAlive()
        {
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
            return "Total baggage cost: " + cost + ", battery level: " + (BatteryCharge > 0 ? BatteryCharge : 0) + ".";
        }
    }
}