using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.State
{
    public abstract class InputReceiver
    {
        protected Dictionary<string, Func<Game, string, string>> inputActionBinder = new Dictionary<string, Func<Game, string, string>>();
        public string HandleInput(string input, Game game, string map) {
            return inputActionBinder.Keys.Contains(input) ? inputActionBinder[input].Invoke(game, map) : "Incorect command";
        }
    }
}
