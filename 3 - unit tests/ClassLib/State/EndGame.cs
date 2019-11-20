using ClassLib.State.InputReceivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.State
{
    public class EndGame : IGameState
    {
        private InputReceiver receiver = new EndGameInputRececiver();
        public string NextStage(Game game)
        {
            game.State = new StartMenu();
            return "You are at the start menu" + "\r\n" + game.State.HelpMessage();
        }

        public string PreviousState(Game game)
        {
            game.State = new GameProcess();
            return "You are backed to game process." + "\r\n" + game.State.HelpMessage();
        }

        public string Turn(Game game, string input)
        {
            if (input.ToLower() == "help") return HelpMessage();
            return receiver.HandleInput(input, game, "");
        }

        public string HelpMessage()
        {
            return "Print 'start' for new game \r\n " +
                "'score' will show U last progress \r\n " +
                "'help' for help message \r\n" +
                "'exit' for close application. \r\n";
        }
    }
}
