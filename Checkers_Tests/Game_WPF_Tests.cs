using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers;

namespace Checkers_Tests
{
    [TestClass]
    public class Game_WPF_Tests
    {
        Window1 Game;
        public Game_WPF_Tests()
        {
            Game = new Window1();
        }

        [TestMethod]
        public void Count_All_Stounes_Return_24()
        {
            int count = 0;

            count += Game.Blues.Count;
            count += Game.Reds.Count;

            Assert.IsTrue(count == 24);
        }

        [TestMethod]
        public void Time_Is_0_At_Start()
        {
            Assert.IsTrue(Game.stopky == TimeSpan.FromSeconds(0));
        }

        [TestMethod]
        public void No_Queen_At_Start()
        {
            bool isQueen = false;

            foreach (var item in Game.Reds)
            {
                if (item.Tag.ToString() == "queen") isQueen = true;
            }
            foreach (var item in Game.Blues)
            {
                if (item.Tag.ToString() == "queen") isQueen = true;
            }

            Assert.IsFalse(isQueen);
        }

        [TestMethod]
        public void Test_For_Test()
        {
            int i = 0;

            i++;

            Assert.IsTrue(i == 1);
        }
    }
}
