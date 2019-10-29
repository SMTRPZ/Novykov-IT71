using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Decorator
{
    public class Stone : ICloneable
    {
        public int Damage { get; protected set; }

        public int StoneHealth { get; protected set; }

        public string Description { get; protected set; }

        public double Weight { get; protected set; }

        public bool Decryption { get; protected set; }

        public bool Collapses { get; protected set; }

        public Stone(int damage, string desc, double weight, bool decrypt, int stoneHealth, bool collapses)
        {
            Damage = damage;
            Description = desc;
            Weight = weight;
            Decryption = decrypt;
            StoneHealth = stoneHealth;
            Collapses = collapses;
        }

        public virtual string GetInfo()
        {
            return "Info: ";
        }

        public virtual double GetCost()
        {
            return Weight;
        }

        public virtual void DecreaseHealth()
        {

        }

        public virtual object Clone()
        {
            return new Stone(Damage, Description, Weight, Decryption, StoneHealth, Collapses);
        }
    }
}
