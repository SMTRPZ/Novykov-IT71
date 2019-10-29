using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Decorator
{
    public class DecryptionStone : StoneDecorator
    {
        public DecryptionStone(Stone stone) 
            : base(stone.Damage, stone.Description + ", need to be decrypted", stone.Weight, true, stone.StoneHealth, stone.Collapses, stone)
        {

        }

        private DecryptionStone(Stone stone, bool decrypt)
            : base(stone.Damage, stone.Description, stone.Weight, decrypt, stone.StoneHealth, stone.Collapses, stone)
        {

        }

        public override void DecreaseHealth()
        {
            if (StoneHealth > 0 && Collapses)
                StoneHealth--;
        }

        public override double GetCost()
        {
            return stone.GetCost() * 2.5;
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
            return new DecryptionStone(new Stone(Damage, Description, Weight, Decryption, StoneHealth, Collapses), true);
        }
    }
}
