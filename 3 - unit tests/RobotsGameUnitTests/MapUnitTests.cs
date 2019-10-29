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
    public class MapUnitTests
    {
        [TestMethod]
        public void MapGeneration()
        {
            var map = new Map();

            Assert.AreEqual(map.Stones.Count, 100);
        }

        [TestMethod]
        public void MapCloneTest()
        {
            var map = new Map();
            var mapCopy = (Map)map.Clone();

            Assert.AreEqual(map.Stones.Count, mapCopy.Stones.Count);
            Assert.AreEqual(map.Stones[0].Weight, mapCopy.Stones[0].Weight);

            mapCopy.Stones[0] = new ValuableStone(15);

            Assert.AreNotEqual(map.Stones[0].Weight, mapCopy.Stones[0].Weight);
        }
    }
}
