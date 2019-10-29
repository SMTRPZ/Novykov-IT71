using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.State
{
    public interface IGameState
    {
        string NextStage(Game game);

        string PreviousState(Game game);

        string Turn(Game game, string input);

        string HelpMessage();
    }
}
