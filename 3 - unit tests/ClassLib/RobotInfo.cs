using ClassLib.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLib
{
    public abstract class RobotInfo
    {
        #region Properties
        public Guid Id { get; protected set; }
        public string RobotImageBase64 { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public double BatteryCharge { get; protected set; }
        public double MaxWeight { get; protected set; }
        public double CurrentWeight { get; protected set; }
        public int Health { get; protected set; }
        public List<Stone> Baggage { get; protected set; }
        #endregion

        public RobotInfo(RobotInfo info)
        {
            RestoreState(info);
        }

        public RobotInfo(Guid id, string image, string name, string desc,
            double charge, double maxw, double curw, int health, IEnumerable<Stone> baggage)
        {
            RestoreState(id, image, name, desc, charge, maxw, curw, health, baggage);
        }

        protected IEnumerable<T> DeepClone<T>(IEnumerable<T> iEnumerable) where T : ICloneable
        {
            return iEnumerable.Select(element => (T)element.Clone());
        }

        protected void RestoreState(Guid id, string image, string name, string desc,
            double charge, double maxw, double curw, int health, IEnumerable<Stone> baggage)
        {
            Id = id;
            RobotImageBase64 = image;
            Name = name;
            Description = desc;
            BatteryCharge = charge;
            MaxWeight = maxw;
            Baggage = DeepClone(baggage).ToList();
            CurrentWeight = curw;
            Health = health;
        }
        protected void RestoreState(RobotInfo info)
        {
            RestoreState(info.Id, info.RobotImageBase64, info.Name, info.Description,
                info.BatteryCharge, info.MaxWeight, info.CurrentWeight,
                info.Health, info.Baggage);
        }
    }
}
