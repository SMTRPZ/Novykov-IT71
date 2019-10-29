using ClassLib;
using ClassLib.Decorator;
using ClassLib.Factory_Method;
using ClassLib.Memento;
using ClassLib.State;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsGameUnitTests
{
    [TestClass]
    public class MementoUnitTests
    {
        [TestMethod]
        public void GameHistoryTest()
        {
            GameHistory gameHistory = new GameHistory();
            Game game = new Game(new StartMenu());
            game.GenerateNewGame();

            var robotEnegry = game.Robot.BatteryCharge;

            game.State = new GameProcess();

            Assert.AreEqual(game.Robot.BatteryCharge, robotEnegry);

            gameHistory.Add(new GameMemento(game.Robot.SaveState(), game.MoveCounter, (Map)game.Map.Clone()));
            var res = game.Turn("next");

            Assert.AreEqual(res, "In fronr of you lies: " + game.Map.Stones[1].GetInfo() + "\r\nWhat will U do?\r\n\r\n" 
                + (game.Robot is Cyborg ? "Turns harm: 0, battery charge: " : "Battery charge: ") + 
                + (robotEnegry - 1) + ", battery lost: " + 1 + ", ");
            Assert.AreEqual(game.Robot.BatteryCharge, robotEnegry - 1);

            game.Robot.RestoreState(gameHistory.GetLastState().GetRobotMemento());

            Assert.AreEqual(game.Robot.BatteryCharge, robotEnegry);
        }
    }
}
