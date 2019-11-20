using ClassLib.Memento;
using ClassLib.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.State.InputReceivers
{
    class StartMenuInputReceiver : InputReceiver
    {
        public StartMenuInputReceiver()
        {
            inputActionBinder.Add("start",
               (Game game, string map) => game.PreviousState());
            inputActionBinder.Add("end",
                (Game game, string map) => {
                    game.NextStage();
                    return game.NextStage();
                    });
            inputActionBinder.Add("new",
                (Game game, string map) => {
                    game.GenerateNewGame();
                    string res = game.NextStage() + "\r\n";
                    return "\r\n" + res + "\r\n Your robot: " + game.Robot.GetInfo();
                });
            inputActionBinder.Add("exit",
                (Game game, string map) => "Application will close in 3 sec. Tnx U!");
        }
    }
}
