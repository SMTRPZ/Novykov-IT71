using ClassLib.Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.State
{
    class GameProcessInputReceiver : InputReceiver
    {
        public GameProcessInputReceiver()
        {
            inputActionBinder.Add("stone",
                (Game game, string map) =>
                {
                    if (map != "Empty")
                    {
                        string res = "In fronr of you lies: " + map + "\r\n";
                        game.GameHistory.Add(new GameMemento(game.Robot.SaveState(), game.MoveCounter, (Map)game.Map.Clone()));

                        res = game.Robot.AddStone(game.Map.Stones[game.MoveCounter]);
                        game.Map.Stones[game.MoveCounter] = null;
                        res += "\r\nYou need to go forward, there nothing to do.";
                        return res;
                    }
                    return "You need to go forward, there nothing to do";
                });
            inputActionBinder.Add("next",
                (Game game, string map) =>
                {
                    game.GameHistory.Add(new GameMemento(game.Robot.SaveState(), game.MoveCounter, (Map)game.Map.Clone()));

                    game.IncrementMoveCounter();
                    var append = game.Robot.Turn();
                    string res;
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
                });
            inputActionBinder.Add("info",
                (Game game, string map) => game.Robot.GetInfo());
            inputActionBinder.Add("baggage",
                (Game game, string map) => game.Robot.GetBaggageInfo());
            inputActionBinder.Add("drop",
                (Game game, string map) => game.Robot.DropStone());
            inputActionBinder.Add("start",
                (Game game, string map) => {
                    string res = "Your current progress deleted. \r\n";
                    return "\r\n" + res + "\r\n" + game.PreviousState();
                });
            inputActionBinder.Add("end",
                (Game game, string map) => {
                    return game.NextStage();
                });
            inputActionBinder.Add("exit",
                (Game game, string map) => "Application will close in 3 sec. Tnx U!");
            inputActionBinder.Add("mback",
                (Game game, string map) => {
                    var memento = game.GameHistory.GetLastState();
                    if (memento != null)
                    {
                        game.SetMap(memento.Map);
                        game.SetMoveCounter(memento.MoveCounter);
                        map = game.Map.Stones[game.MoveCounter] != null ? game.Map.Stones[game.MoveCounter].GetInfo()
                            + "\r\n What will U do?" : "Empty";
                        if (map != "Empty")
                        {
                            return game.Robot.RestoreState(memento.GetRobotMemento()) + ". Move counter: "
                                + memento.MoveCounter + "\r\nIn fronr of you lies: " + map + "\r\n";
                        }
                        return game.Robot.RestoreState(memento.GetRobotMemento()) + ". Move counter: "
                            + memento.MoveCounter + "\r\nYou need to go forward, there nothing to do";
                    }
                    else
                    {
                        return "No previous state" + "\r\nIn fronr of you lies: " + game.Map.Stones[game.MoveCounter].GetInfo() + "\r\n";
                    }
                });
        }
    }
}
