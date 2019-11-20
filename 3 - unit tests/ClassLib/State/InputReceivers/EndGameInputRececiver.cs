using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.State.InputReceivers
{
    class EndGameInputRececiver : InputReceiver
    {
        public EndGameInputRececiver()
        {
            inputActionBinder.Add("start",
               (Game game, string map) =>
               {
                   game.PreviousState();
                   return "Your current progress deleted. " + "\r\n" + game.PreviousState();
               });
            inputActionBinder.Add("score",
                (Game game, string map) => "Your score: " + game.Robot.FinalInfo() + " Turns needed: " + game.MoveCounter);
            inputActionBinder.Add("exit",
                (Game game, string map) => "Application will close in 3 sec. Tnx U!");
        }
    }
}
