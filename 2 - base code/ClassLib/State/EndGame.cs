using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.State
{
    public class EndGame : IGameState
    {
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
            string res;
            switch (input.ToLower())
            {
                case "start":
                    res = "Your current progress deleted. ";
                    game.PreviousState();
                    return res + "\r\n" + game.PreviousState();
                case "score":
                    res = "Your score: " + game.Robot.FinalInfo() + " Turns needed: " + game.MoveCounter;
                    return res;
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
            return "Print 'start' for new game \r\n " +
                "'score' will show U last progress \r\n " +
                "'help' for help message \r\n" +
                "'exit' for close application. \r\n";
        }
    }
}
