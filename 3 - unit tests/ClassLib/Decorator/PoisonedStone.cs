using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Decorator
{
    public class PoisonedStone : StoneDecorator
    {
        public PoisonedStone(int damage, Stone stone)
            : base(damage, stone.Description + ", will harm on every turn (ciborgs only)", stone.Weight, stone.Decryption, stone.StoneHealth, stone.Collapses, stone)
        {

        }

        private PoisonedStone(Stone stone)
            : base(stone.Damage, stone.Description, stone.Weight, true, stone.StoneHealth, stone.Collapses, stone)
        {

        }

        public override void DecreaseHealth()
        {
            if (StoneHealth > 0 && Collapses)
                StoneHealth--;
        }

        public override double GetCost()
        {
            return stone.GetCost() * 2;
        }

        public override string GetInfo()
        {
            string res = "Cost: " + GetCost() + ", weight: " + Weight + ", description: " + Description;
            if (Collapses)
                res += ", stone health: " + StoneHealth;
            if (Damage > 0)
                res += ", damage: " + Damage;
            return res;
        }

        public override object Clone()
        {
            return new PoisonedStone(new Stone(Damage, Description, Weight, Decryption, StoneHealth, Collapses));
        }
    }
}
