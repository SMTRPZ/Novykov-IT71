using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Memento
{
    public class GameHistory
    {
        public Stack<GameMemento> History { get; private set; }

        public GameHistory()
        {
            History = new Stack<GameMemento>();
        }

        public void Add(GameMemento gameMemento)
        {
            History.Push(gameMemento);
        }

        public GameMemento GetLastState()
        {
            if (History.Count > 0)
            {
                return History.Pop();
            }
            return null;
        }
    }
}
