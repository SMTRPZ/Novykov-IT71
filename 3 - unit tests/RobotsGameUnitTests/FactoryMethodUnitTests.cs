using ClassLib.Decorator;
using ClassLib.Factory_Method;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RobotsGameUnitTests
{
    [TestClass]
    public class FactoryMethodUnitTests
    {
        public Robot CreateWorkerRobot()
        {
            return new WorkerCreator().Create("workerimage");
        }
        public Robot CreateCyborgRobot()
        {
            return new CyborgCreator().Create("cyborgimage");
        }
        public Robot CreateScientistRobot()
        {
            return new ScientistCreator().Create("scientistimage");
        }

        [TestMethod]
        public void RobotCollapsedStoneDrop()
        {
            var robot = CreateWorkerRobot();

            Stone stone = new ValuableStone(Convert.ToDouble(10));
            stone = new CollapseStone(1, stone);
            var res = robot.AddStone(stone);

            Assert.AreEqual(res, stone.GetInfo() + "\r\n Succesfully added");
            Assert.AreEqual(robot.Baggage.Count, 1);
            Assert.AreEqual(robot.GetBaggageInfo(), "Total weight: " + 10 + ", total cost: " + 15 + ", free space: " + 190);

            res = robot.Turn();
            Assert.AreEqual(res, "Battery charge: " + 98 + ", battery lost: " + 2 + ", " + "Was droped(collapse destroy it). " + stone.GetInfo());
            Assert.AreEqual(robot.Baggage.Count, 0);
            Assert.AreEqual(robot.GetBaggageInfo(), "Total weight: " + 0 + ", total cost: " + 0 + ", free space: " + 200);
        }

        #region Worker Tests

        [TestMethod]
        public void WorkerStats()
        {
            var robot = CreateWorkerRobot();

            Assert.AreEqual(robot.Description, "Highest payload and battery, 10% decode cargo");
            Assert.AreEqual(robot.BatteryCharge, 100);
            Assert.AreEqual(robot.MaxWeight, 200);
            Assert.AreEqual(robot.Name, "Worker");
            Assert.AreEqual(robot.RobotImageBase64, "workerimage");

            Assert.AreEqual(robot.GetInfo(), "Id: " + robot.Id + ", image: " + "workerimage" + ", name: " + "Worker" + ", description: " +
                "Highest payload and battery, 10% decode cargo" + ", battery charge: " + 100 + ", max weight: " + 200 + ", current weight: " + 0
                + ", baggage count: " + 0);
            Assert.IsTrue(robot.IsAlive());
        }

        [TestMethod]
        public void WorkerAddBaggage()
        {
            var robot = CreateWorkerRobot();

            Stone stone = new ValuableStone(Convert.ToDouble(10));
            stone = new PoisonedStone(5, stone);
            var res = robot.AddStone(stone);

            Assert.AreEqual(res, stone.GetInfo() + "\r\n Succesfully added");
            Assert.AreEqual(robot.Baggage.Count, 1);
            Assert.AreEqual(robot.GetBaggageInfo(), "Total weight: " + 10 + ", total cost: " + 20 + ", free space: " + 190);

            res = robot.AddStone(stone);

            Assert.AreEqual(res, stone.GetInfo() + "\r\n Succesfully added");
            Assert.AreEqual(robot.Baggage.Count, 2);
            Assert.AreEqual(robot.GetBaggageInfo(), "Total weight: " + 20 + ", total cost: " + 40 + ", free space: " + 180);
        }

        [TestMethod]
        public void WorkerDeleteBaggage()
        {
            var robot = CreateWorkerRobot();

            Stone stone = new ValuableStone(Convert.ToDouble(10));
            stone = new PoisonedStone(5, stone);
            var res = robot.AddStone(stone);
            res = robot.AddStone(stone);

            Assert.AreEqual(robot.Baggage.Count, 2);
            Assert.AreEqual(robot.GetBaggageInfo(), "Total weight: " + 20 + ", total cost: " + 40 + ", free space: " + 180);

            res = robot.DropStone();
            Assert.AreEqual(res, "Succesfully droped: " + stone.GetInfo());
            Assert.AreEqual(robot.Baggage.Count, 1);

            res = robot.DropStone();
            Assert.AreEqual(res, "Succesfully droped: " + stone.GetInfo());
            Assert.AreEqual(robot.Baggage.Count, 0);

            res = robot.DropStone();
            Assert.AreEqual(res, "Baggage already empty");
            Assert.AreEqual(robot.Baggage.Count, 0);
        }

        [TestMethod]
        public void WorkerDecryptedStoneFails()
        {
            var robot = CreateWorkerRobot();

            List<Stone> stones = new List<Stone>();
            for (int i = 0; i < 100; i++)
            {
                Stone stone = new ValuableStone(Convert.ToDouble(1));
                stone = new DecryptionStone(stone);
                stones.Add(stone);
                var res = robot.AddStone(stone);
            }

            Assert.IsTrue(robot.Baggage.Count < 100);
        }

        [TestMethod]
        public void WorkerTurnCheck()
        {
            var robot = CreateWorkerRobot();

            Assert.AreEqual(robot.BatteryCharge, 100);

            var res = robot.Turn();
            Assert.AreEqual(res, "Battery charge: " + 99 + ", battery lost: " + 1 + ", ");

            Stone stone = new ValuableStone(Convert.ToDouble(10));
            stone = new PoisonedStone(5, stone);
            res = robot.AddStone(stone);

            Assert.AreEqual(res, stone.GetInfo() + "\r\n Succesfully added");
        }

        #endregion

        #region Cyborg Tests

        [TestMethod]
        public void CyborgStats()
        {
            var robot = CreateCyborgRobot();

            Assert.AreEqual(robot.Description, "Medium payload and battery, 60% decode cargo. Can die from poisoned stones." +
                  " After 80% payload have 30% change to drop battery");
            Assert.AreEqual(robot.BatteryCharge, 70);
            Assert.AreEqual(robot.MaxWeight, 135);
            Assert.AreEqual(robot.Name, "Cyborg");
            Assert.AreEqual(robot.RobotImageBase64, "cyborgimage");

            Assert.AreEqual(robot.GetInfo(), "Id: " + robot.Id + ", image: " + "cyborgimage" + ", name: " + "Cyborg" + ", description: " +
                "Medium payload and battery, 60% decode cargo. Can die from poisoned stones. After 80% payload have 30% change to drop battery" + 
                ", battery charge: " + 70 + ", health: " + 100 + ", max weight: " + 135 + ", current weight: " + 0 + ", baggage count: " + 0);
            Assert.IsTrue(robot.IsAlive());
        }

        [TestMethod]
        public void CyborgAddBaggage()
        {
            var robot = CreateCyborgRobot();

            Stone stone = new ValuableStone(Convert.ToDouble(10));
            stone = new PoisonedStone(5, stone);
            var res = robot.AddStone(stone);

            Assert.AreEqual(res, stone.GetInfo() + "\r\n Succesfully added");
            Assert.AreEqual(robot.Baggage.Count, 1);
            Assert.AreEqual(robot.GetBaggageInfo(), "Total weight: " + 10 + ", total cost: " + 20 + ", free space: " + 125 + 
                ", damage per turn: " + 5 + ", current hp: " + 100);

            res = robot.AddStone(stone);

            Assert.AreEqual(res, stone.GetInfo() + "\r\n Succesfully added");
            Assert.AreEqual(robot.Baggage.Count, 2);
            Assert.AreEqual(robot.GetBaggageInfo(), "Total weight: " + 20 + ", total cost: " + 40 + ", free space: " + 115 +
                ", damage per turn: " + 10 + ", current hp: " + 100);
        }

        [TestMethod]
        public void CyborgDeleteBaggage()
        {
            var robot = CreateCyborgRobot();

            Stone stone = new ValuableStone(Convert.ToDouble(10));
            stone = new PoisonedStone(5, stone);
            var res = robot.AddStone(stone);
            res = robot.AddStone(stone);

            Assert.AreEqual(robot.Baggage.Count, 2);
            Assert.AreEqual(robot.GetBaggageInfo(), "Total weight: " + 20 + ", total cost: " + 40 + ", free space: " + 115 +
                ", damage per turn: " + 10 + ", current hp: " + 100);

            res = robot.DropStone();
            Assert.AreEqual(res, "Succesfully droped: " + stone.GetInfo());
            Assert.AreEqual(robot.Baggage.Count, 1);

            res = robot.DropStone();
            Assert.AreEqual(res, "Succesfully droped: " + stone.GetInfo());
            Assert.AreEqual(robot.Baggage.Count, 0);

            res = robot.DropStone();
            Assert.AreEqual(res, "Baggage already empty");
            Assert.AreEqual(robot.Baggage.Count, 0);
        }

        [TestMethod]
        public void CyborgTurnCheck()
        {
            var robot = CreateCyborgRobot();

            Assert.AreEqual(robot.BatteryCharge, 70);

            var res = robot.Turn();
            Assert.AreEqual(res, "Turns harm: " + 0 + ", battery charge: " + 69 + ", battery lost: " + 1 + ", ");

            Stone stone = new ValuableStone(Convert.ToDouble(10));
            stone = new PoisonedStone(5, stone);
            res = robot.AddStone(stone);

            Assert.AreEqual(res, stone.GetInfo() + "\r\n Succesfully added");

            res = robot.Turn();
            Assert.AreEqual(res, "Turns harm: " + 5 + ", battery charge: " + 67 + ", battery lost: " + 2 + ", ");
        }

        [TestMethod]
        public void CyborgDeadByPoison()
        {
            var robot = CreateCyborgRobot();

            Stone stone = new ValuableStone(Convert.ToDouble(110));
            stone = new PoisonedStone(110, stone);
            var res = robot.AddStone(stone);

            Assert.AreEqual(res, "You are dead (9((9");
        }

        #endregion

        #region Scientist Tests

        [TestMethod]
        public void ScientistStats()
        {
            var robot = CreateScientistRobot();

            Assert.AreEqual(robot.Description, "Lowest payload and battery, 100% decode cargo.");
            Assert.AreEqual(robot.BatteryCharge, 50);
            Assert.AreEqual(robot.MaxWeight, 100);
            Assert.AreEqual(robot.Name, "Scientist");
            Assert.AreEqual(robot.RobotImageBase64, "scientistimage");

            Assert.AreEqual(robot.GetInfo(), "Id: " + robot.Id + ", image: " + "scientistimage" + ", name: " + "Scientist" + ", description: " +
                "Lowest payload and battery, 100% decode cargo." + ", battery charge: " + 50 + ", max weight: " + 100 + ", current weight: " + 0
                + ", baggage count: " + 0);
            Assert.IsTrue(robot.IsAlive());
        }

        [TestMethod]
        public void ScientistAddBaggage()
        {
            var robot = CreateScientistRobot();

            Stone stone = new ValuableStone(Convert.ToDouble(10));
            stone = new PoisonedStone(5, stone);
            var res = robot.AddStone(stone);

            Assert.AreEqual(res, stone.GetInfo() + "\r\n Succesfully added");
            Assert.AreEqual(robot.Baggage.Count, 1);
            Assert.AreEqual(robot.GetBaggageInfo(), "Total weight: " + 10 + ", total cost: " + 20 + ", free space: " + 90);

            res = robot.AddStone(stone);

            Assert.AreEqual(res, stone.GetInfo() + "\r\n Succesfully added");
            Assert.AreEqual(robot.Baggage.Count, 2);
            Assert.AreEqual(robot.GetBaggageInfo(), "Total weight: " + 20 + ", total cost: " + 40 + ", free space: " + 80);
        }

        [TestMethod]
        public void ScientistDeleteBaggage()
        {
            var robot = CreateScientistRobot();

            Stone stone = new ValuableStone(Convert.ToDouble(10));
            stone = new PoisonedStone(5, stone);
            var res = robot.AddStone(stone);
            res = robot.AddStone(stone);

            Assert.AreEqual(robot.Baggage.Count, 2);
            Assert.AreEqual(robot.GetBaggageInfo(), "Total weight: " + 20 + ", total cost: " + 40 + ", free space: " + 80);

            res = robot.DropStone();
            Assert.AreEqual(res, "Succesfully droped: " + stone.GetInfo());
            Assert.AreEqual(robot.Baggage.Count, 1);

            res = robot.DropStone();
            Assert.AreEqual(res, "Succesfully droped: " + stone.GetInfo());
            Assert.AreEqual(robot.Baggage.Count, 0);

            res = robot.DropStone();
            Assert.AreEqual(res, "Baggage already empty");
            Assert.AreEqual(robot.Baggage.Count, 0);
        }

        [TestMethod]
        public void ScientistDecryptedStoneComplete()
        {
            var robot = CreateScientistRobot();

            List<Stone> stones = new List<Stone>();
            for (int i = 0; i < 100; i++)
            {
                Stone stone = new ValuableStone(Convert.ToDouble(0.1));
                stone = new DecryptionStone(stone);
                stones.Add(stone);
                var res = robot.AddStone(stone);
            }

            Assert.IsTrue(robot.Baggage.Count == 100);
        }

        [TestMethod]
        public void ScientistTurnCheck()
        {
            var robot = CreateScientistRobot();

            Assert.AreEqual(robot.BatteryCharge, 50);

            var res = robot.Turn();
            Assert.AreEqual(res, "Battery charge: " + 49 + ", battery lost: " + 1 + ", ");

            Stone stone = new ValuableStone(Convert.ToDouble(10));
            stone = new PoisonedStone(5, stone);
            res = robot.AddStone(stone);

            Assert.AreEqual(res, stone.GetInfo() + "\r\n Succesfully added");
        }

        #endregion
    }
}
