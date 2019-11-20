using ClassLib.State.InputReceivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.State
{
    public class StartMenu : IGameState
    {
        private InputReceiver inputReceiver = new StartMenuInputReceiver();
        public string NextStage(Game game)
        {
            game.State = new GameProcess();
            var map = game.Map != null ? game.Map.Stones[game.MoveCounter].GetInfo() + "\r\n What will U do?" : "Empty";
            return "Welcome to the GAME:" + "\r\n" + game.State.HelpMessage() + "\r\n" + "In front of you lies: " + map;
        }

        public string PreviousState(Game game)
        {
            return "You are already at main menu!" + "\r\n" + HelpMessage();
        }

        public string Turn(Game game, string input)
        {
            if (input.ToLower()=="help") return HelpMessage();
            return inputReceiver.HandleInput(input, game, "");
        }

        public string HelpMessage()
        {
            return "Print 'start' for main menu \r\n " +
                "'new' for new game \r\n " +
                "'end' for end menu \r\n " +
                "'help' for help message \r\n " +
                "'exit' for close application. \r\n";
        }
    }
}
