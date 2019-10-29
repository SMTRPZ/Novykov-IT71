using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Decorator
{
    public abstract class StoneDecorator : Stone
    {
        protected Stone stone;

        public StoneDecorator(int damage, string description, double weight, bool decryption, int stoneHealth, bool collapses, Stone stone) 
            : base(damage, description, weight, decryption, stoneHealth, collapses)
        {
            this.stone = stone;
        }
    }
}
