using ClassLib.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    public class Map : ICloneable
    {
        public List<Stone> Stones { get; private set; }

        private Random rand = new Random();

        public Map()
        {
            Stones = new List<Stone>();
            for (int i = 0; i < 100; i++)
            {
                Stones.Add(StoneGenerator());
            }
        }

        private Map(List<Stone> stones)
        {
            Stones = stones;
        }

        private Stone StoneGenerator()
        {
            Stone stone;
            if (rand.Next(3) > 0)
            {
                stone = new OrdinaryStone();
            }
            else
            {
                stone = new ValuableStone(Convert.ToDouble(rand.Next(1, 100)) / 10);

                if (rand.Next(5) == 0)
                    stone = new DecryptionStone(stone);

                if (rand.Next(6) == 0)
                    stone = new CollapseStone(rand.Next(1, 15), stone);

                if (rand.Next(8) == 0)
                    stone = new PoisonedStone(rand.Next(1, 10), stone);
            }

            return stone;
        }

        public object Clone()
        {
            List<Stone> stones = new List<Stone>();

            for (int i = 0; i < Stones.Count; i++)
            {
                Stone st = Stones[i];
                if (st != null)
                {
                    stones.Add((Stone)st.Clone());
                }
                else
                {
                    stones.Add(null);
                }
            }

            return new Map(stones);
        }
    }
}
