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
            string res = "In fronr of you lies: " + map + "\r\n";
            if (game.Robot.IsAlive())
            {
                switch (input.ToLower())
                {
                    case "stone":
                        if (map != "Empty")
                        {
                            game.Save();

                            res = game.Robot.AddStone(game.Map.Stones[game.MoveCounter]);
                            game.Map.Stones[game.MoveCounter] = null;
                            res += "\r\nYou need to go forward, there nothing to do.";
                            return res;
                        }
                        return "You need to go forward, there nothing to do";
                    case "next":
                        game.Save();

                        game.IncrementMoveCounter();
                        var append = game.Robot.Turn();
                        if (game.Robot.IsAlive())
                        {
                            map = "\r\nWhat will U do?";
                            res = "In fronr of you lies: " + game.Map.Stones[game.MoveCounter].GetInfo() + map + "\r\n";
                            res += "\r\n" + append;
                        }
                        else
                        {
                            res = "Game ended. Your score: \r\n" + game.Robot.FinalInfo() + " Turns needed: " + game.MoveCounter;
                            game.NextStage();
                        }
                        return res;
                    case "info":
                        return game.Robot.GetInfo();
                    case "baggage":
                        return game.Robot.GetBaggageInfo();
                    case "drop":
                        return game.Robot.DropStone();
                    case "start":
                        res = "Your current progress deleted. \r\n";
                        return "\r\n" + res + "\r\n" + game.PreviousState();
                    case "end":
                        res = "Your current progress deleted. ";
                        return game.NextStage();
                    case "exit":
                        return "Application will close in 3 sec. Tnx U!";
                    case "help":
                        return HelpMessage();
                    case "mback":
                        var memento = game.GameHistory.GetLastState();
                        if (memento != null)
                        {
                            game.SetMap(memento.Map);
                            game.SetMoveCounter(memento.MoveCounter);
                            map = game.Map.Stones[game.MoveCounter] != null ? game.Map.Stones[game.MoveCounter].GetInfo() 
                                + "\r\n What will U do?" : "Empty";
                            if (map != "Empty")
                            {
                                return game.Robot.RestoreState(memento.RobotMemento) + ". Move counter: " 
                                    + memento.MoveCounter + "\r\nIn fronr of you lies: " + map + "\r\n";
                            }
                            return game.Robot.RestoreState(memento.RobotMemento) + ". Move counter: " 
                                + memento.MoveCounter + "\r\nYou need to go forward, there nothing to do";
                        }
                        else
                        {
                            return "No previous state" + "\r\nIn fronr of you lies: " + game.Map.Stones[game.MoveCounter].GetInfo() + "\r\n";
                        }
                    default:
                        return "Incorect command";
                }
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
