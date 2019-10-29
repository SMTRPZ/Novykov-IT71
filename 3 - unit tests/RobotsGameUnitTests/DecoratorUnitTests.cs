using ClassLib;
using ClassLib.Decorator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsGameUnitTests
{
    [TestClass]
    public class DecoratorUnitTests
    {
        #region Single Stone

        [TestMethod]
        public void OrdinaryStoneTest()
        {
            var stone = new OrdinaryStone();

            Assert.AreEqual(stone.GetInfo(), "Description: Ordinary stone, nothing cost");
            Assert.AreEqual(stone.Weight, 0);
            Assert.AreEqual(stone.StoneHealth, 0);
            Assert.AreEqual(stone.Collapses, false);
            Assert.AreEqual(stone.Decryption, false);
            Assert.AreEqual(stone.GetCost(), 0);
            Assert.AreEqual(stone.Damage, 0);
            Assert.AreEqual(stone.Description, "Ordinary stone, nothing cost");
        }

        [TestMethod]
        public void ValuableStoneTest()
        {
            Stone stone = new ValuableStone(5);

            Assert.AreEqual(stone.GetInfo(), "Cost: " + 5 + ", weight: " + 5 + ", description: Valuable stone");
            Assert.AreEqual(stone.Weight, 5);
            Assert.AreEqual(stone.StoneHealth, 0);
            Assert.AreEqual(stone.Collapses, false);
            Assert.AreEqual(stone.Decryption, false);
            Assert.AreEqual(stone.GetCost(), stone.Weight);
            Assert.AreEqual(stone.Damage, 0);
            Assert.AreEqual(stone.Description, "Valuable stone");
        }

        [TestMethod]
        public void DecryptionStoneTest()
        {
            Stone stone = new ValuableStone(5);
            stone = new DecryptionStone(stone);

            Assert.AreEqual(stone.GetInfo(), "Cost: " + 12.5 + ", weight: " + 5 + ", description: Valuable stone, need to be decrypted");
            Assert.AreEqual(stone.Weight, 5);
            Assert.AreEqual(stone.StoneHealth, 0);
            Assert.AreEqual(stone.Collapses, false);
            Assert.AreEqual(stone.Decryption, true);
            Assert.AreEqual(stone.GetCost(), stone.Weight * 2.5);
            Assert.AreEqual(stone.Damage, 0);
            Assert.AreEqual(stone.Description, "Valuable stone, need to be decrypted");
        }

        [TestMethod]
        public void CollapseStoneTest()
        {
            Stone stone = new ValuableStone(5);
            stone = new CollapseStone(5, stone);

            Assert.AreEqual(stone.GetInfo(), "Cost: " + 7.5 + ", weight: " + 5 + ", description: Valuable stone, will destroy on every turn, stone health: " + 5);
            Assert.AreEqual(stone.Weight, 5);
            Assert.AreEqual(stone.StoneHealth, 5);
            Assert.AreEqual(stone.Collapses, true);
            Assert.AreEqual(stone.Decryption, false);
            Assert.AreEqual(stone.GetCost(), stone.Weight * 1.5);
            Assert.AreEqual(stone.Damage, 0);
            Assert.AreEqual(stone.Description, "Valuable stone, will destroy on every turn");

            stone.DecreaseHealth();

            Assert.AreEqual(stone.StoneHealth, 4);
        }

        [TestMethod]
        public void PoisonedStoneTest()
        {
            Stone stone = new ValuableStone(5);
            stone = new PoisonedStone(5, stone);

            Assert.AreEqual(stone.GetInfo(), "Cost: " + 10 + ", weight: " + 5 + ", description: Valuable stone, will harm on every turn (ciborgs only), damage: " + 5);
            Assert.AreEqual(stone.Weight, 5);
            Assert.AreEqual(stone.StoneHealth, 0);
            Assert.AreEqual(stone.Collapses, false);
            Assert.AreEqual(stone.Decryption, false);
            Assert.AreEqual(stone.GetCost(), stone.Weight * 2);
            Assert.AreEqual(stone.Damage, 5);
            Assert.AreEqual(stone.Description, "Valuable stone, will harm on every turn (ciborgs only)");
        }

        #endregion

        #region 2x Stone

        [TestMethod]
        public void DecryptionPoisonedStoneTest()
        {
            Stone stone = new ValuableStone(5);
            stone = new PoisonedStone(5, stone);
            stone = new DecryptionStone(stone);

            Assert.AreEqual(stone.GetInfo(), "Cost: " + 25 + ", weight: " + 5 + ", description: Valuable stone" +
                ", will harm on every turn (ciborgs only), need to be decrypted, damage: " + 5);
            Assert.AreEqual(stone.Weight, 5);
            Assert.AreEqual(stone.StoneHealth, 0);
            Assert.AreEqual(stone.Collapses, false);
            Assert.AreEqual(stone.Decryption, true);
            Assert.AreEqual(stone.GetCost(), stone.Weight * 5);
            Assert.AreEqual(stone.Damage, 5);
            Assert.AreEqual(stone.Description, "Valuable stone, will harm on every turn (ciborgs only), need to be decrypted");
        }

        [TestMethod]
        public void DecryptionCollapsedStoneTest()
        {
            Stone stone = new ValuableStone(5);
            stone = new CollapseStone(5, stone);
            stone = new DecryptionStone(stone);

            Assert.AreEqual(stone.GetInfo(), "Cost: " + 18.75 + ", weight: " + 5 + ", description: Valuable stone" +
                ", will destroy on every turn, need to be decrypted, stone health: " + 5);
            Assert.AreEqual(stone.Weight, 5);
            Assert.AreEqual(stone.StoneHealth, 5);
            Assert.AreEqual(stone.Collapses, true);
            Assert.AreEqual(stone.Decryption, true);
            Assert.AreEqual(stone.GetCost(), stone.Weight * 3.75);
            Assert.AreEqual(stone.Damage, 0);
            Assert.AreEqual(stone.Description, "Valuable stone, will destroy on every turn, need to be decrypted");

            stone.DecreaseHealth();

            Assert.AreEqual(stone.StoneHealth, 4);
        }

        [TestMethod]
        public void PoisonedCollapsedStoneTest()
        {
            Stone stone = new ValuableStone(5);
            stone = new PoisonedStone(5, stone);
            stone = new CollapseStone(5, stone);

            Assert.AreEqual(stone.GetInfo(), "Cost: " + 15 + ", weight: " + 5 + ", description: Valuable stone" +
                ", will harm on every turn (ciborgs only), will destroy on every turn, stone health: " + 5 + ", damage: " + 5);
            Assert.AreEqual(stone.Weight, 5);
            Assert.AreEqual(stone.StoneHealth, 5);
            Assert.AreEqual(stone.Collapses, true);
            Assert.AreEqual(stone.Decryption, false);
            Assert.AreEqual(stone.GetCost(), stone.Weight * 3);
            Assert.AreEqual(stone.Damage, 5);
            Assert.AreEqual(stone.Description, "Valuable stone, will harm on every turn (ciborgs only), will destroy on every turn");

            stone.DecreaseHealth();

            Assert.AreEqual(stone.StoneHealth, 4);
        }

        #endregion

        #region 3x Stone

        [TestMethod]
        public void CollapsedPoisonedDecryptionStone()
        {
            Stone stone = new ValuableStone(5);
            stone = new CollapseStone(5, stone);
            stone = new PoisonedStone(5, stone);
            stone = new DecryptionStone(stone);

            Assert.AreEqual(stone.GetInfo(), "Cost: " + 37.5 + ", weight: " + 5 + ", description: Valuable stone" +
                ", will destroy on every turn, will harm on every turn (ciborgs only), need to be decrypted, stone health: " + 5 + ", damage: " + 5);
            Assert.AreEqual(stone.Weight, 5);
            Assert.AreEqual(stone.StoneHealth, 5);
            Assert.AreEqual(stone.Collapses, true);
            Assert.AreEqual(stone.Decryption, true);
            Assert.AreEqual(stone.GetCost(), stone.Weight * 7.5);
            Assert.AreEqual(stone.Damage, 5);
            Assert.AreEqual(stone.Description, "Valuable stone, will destroy on every turn, will harm on every turn (ciborgs only), need to be decrypted");

            stone.DecreaseHealth();

            Assert.AreEqual(stone.StoneHealth, 4);
        }

        #endregion
    }
}
