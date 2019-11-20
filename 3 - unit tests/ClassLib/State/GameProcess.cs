using ClassLib.Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.State
{
    public class GameProcess : IGameState
    {
        private InputReceiver receiver = new GameProcessInputReceiver();


        public string NextStage(Game game)
        {
            game.State = new EndGame();
            return "Game ended!" + "\r\n" + game.State.HelpMessage(); ;
        }

        public string PreviousState(Game game)
        {
            game.State = new StartMenu();
            return "You are backed to main menu." + "\r\n" + game.State.HelpMessage(); ;
        }

        public string Turn(Game game, string input)
        {
            var map = game.Map.Stones[game.MoveCounter] != null ? game.Map.Stones[game.MoveCounter].GetInfo() + "\r\n What will U do?" : "Empty";
            if (game.Robot.IsAlive())
            {
                if (input.ToLower() == "help") return HelpMessage();
                return receiver.HandleInput(input, game, map);
            }
            else
            {
                return "Game over. " + game.NextStage();
            }
        }

        public string HelpMessage()
        {
            return "Print 'start' for main menu \r\n " +
                "'end' for end menu \r\n " +
                "'stone' for pick up the current stone \r\n " +
                "'next' for move forward on 1 step \r\n " +
                "'drop' for drop the last stone in your inventory \r\n " +
                "'info' for get a info about your robot \r\n " +
                "'baggage' for get info about the current baggage \r\n " +
                "'mback' for previous turn (if you made at least 1 move) \r\n " +
                "'help' for help message \r\n " +
                "'exit' for close application. \r\n";
        }
    }
}
