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
    public class StateUnitTests
    {
        [TestMethod]
        public void GameGenerationTest()
        {
            Game game = new Game(new StartMenu());

            Assert.AreEqual(game.Robot, null);
            Assert.AreEqual(game.Map, null);
            Assert.AreEqual(game.GameHistory, null);

            game.GenerateNewGame();

            Assert.AreNotEqual(game.Robot, null);
            Assert.AreNotEqual(game.Map, null);
            Assert.AreNotEqual(game.GameHistory, null);
            Assert.AreEqual(game.MoveCounter, 0);
        }

        #region StartMenu

        [TestMethod]
        public void StartMenuHints()
        {
            Game game = new Game(new StartMenu());

            var res = game.Turn("help");

            Assert.AreEqual(res, "Print 'start' for main menu \r\n " +
                "'new' for new game \r\n " +
                "'end' for end menu \r\n " +
                "'help' for help message \r\n " +
                "'exit' for close application. \r\n");
        }

        //and so on .... simple check string to be equals ...
        #endregion

        #region GameProcess

        #endregion

        #region EndGame

        #endregion
    }
}
