using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.State
{
    public class StartMenu : IGameState
    {
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
            string res;
            switch (input.ToLower())
            {
                case "start":
                    return game.PreviousState();
                case "end":
                    res = "Your current progress deleted. ";
                    game.NextStage();
                    return game.NextStage();
                case "new":
                    game.GenerateNewGame();
                    //game.GameHistory.Add(game.Robot.SaveState());
                    //game.GameHistory.History.Peek().SetMoveCounter(game.MoveCounter);
                    res = game.NextStage() + "\r\n";
                    return "\r\n" + res + "\r\n Your robot: " + game.Robot.GetInfo(); ;
                case "help":
                    return HelpMessage();
                case "exit":
                    return "Application will close in 3 sec. Tnx U!";
                default:
                    return "Incorrect input. Try again!";
            }
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
