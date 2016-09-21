using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife;
using System.Drawing;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class LifeTests
    {
        #region Get/Set Tests
        [TestMethod]
        public void GetSetTest0()
        {
            Life life = new Life();
            life[0, 0] = true;

            Assert.IsTrue(life.GetCell(new Point(0, 0)));
        }

        [TestMethod]
        public void GetSetTest1()
        {
            Life life = new Life();
            life[0, 0] = false;

            Assert.IsFalse(life.GetCell(new Point(0, 0)));
        }

        [TestMethod]
        public void GetSetTest2()
        {
            Life life = new Life();
            life[0, 0] = true;
            life[0, 0] = false;

            Assert.IsFalse(life.GetCell(new Point(0, 0)));
        }

        [TestMethod]
        public void GetSetTest3()
        {
            Life life = new Life();
            Point p = new Point(-123124, 99000);
            life.SetCell(p, true);

            Assert.IsTrue(life.GetCell(p));
        }
        #endregion

        #region NeighborCount tests
        /// <summary>
        /// Test that each neighbor cell is detected when alive.
        /// </summary>
        [TestMethod]
        public void NeighborCountTest0()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == 1 && j == 1)
                    {
                        continue;
                    }

                    Life life = new Life();
                    life[i - 1, j - 1] = true;
                    life[0, 0] = true;

                    Assert.IsTrue(life.NeighborCount(new Point(0, 0)) == 1);
                }
            }
        }

        /// <summary>
        /// Cell 1,1 is surrounded by living cells so it should have 8 live neighbors.
        /// </summary>
        [TestMethod]
        public void NeighborCountTest1()
        {
            Life life = new Life();
            //Set a 3x3 region as alive.
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    life[i, j] = true;
                }
            }

            Assert.IsTrue(life.NeighborCount(new Point(1, 1)) == 8);
        }

        /// <summary>
        /// A cell in the middle of nowhere should have no live neighbors.
        /// </summary>
        [TestMethod]
        public void NeighborCountTest2()
        {
            Life life = new Life();

            Assert.IsTrue(life.NeighborCount(new Point(123, -352)) == 0);
        }
        #endregion

        #region Step tests
        /// <summary>
        /// Create a 2x2 square.  This should stay the same after a step.
        /// </summary>
        [TestMethod]
        public void StepTest0()
        {
            Life life = new Life();
            life[0, 0] = true;
            life[1, 0] = true;
            life[1, 1] = true;
            life[0, 1] = true;

            life.Step();

            Assert.IsTrue(
                life[0, 0] &&
                life[1, 0] &&
                life[1, 1] &&
                life[0, 1]);
        }

        /// <summary>
        /// Create a 1x3 column of cells.  This should oscillate between 3x1 and 1x3 states.
        /// </summary>
        [TestMethod]
        public void StepTest1()
        {
            Life life = new Life();
            life[0, 1] = true;
            life[1, 1] = true;
            life[2, 1] = true;

            life.Step();

            Assert.IsTrue(
                life.GetCell(new Point(1, 0)) &&
                life.GetCell(new Point(1, 1)) &&
                life.GetCell(new Point(1, 2)));

            life.Step();

            Assert.IsTrue(
                life.GetCell(new Point(0, 1)) &&
                life.GetCell(new Point(1, 1)) &&
                life.GetCell(new Point(2, 1)));
        }

        /// <summary>
        /// Create a glider traveling south-east.  
        /// After 4 steps it should repeat its pattern and move down and right by one unit.
        /// The glider looks like this:
        /// .x...
        /// ..x..
        /// xxx..
        /// </summary>
        [TestMethod]
        public void StepTest2()
        {
            //Store the glider pattern in a list.
            List<Point> glider = new List<Point>() {
                new Point(1, 0),
                new Point(2, 1),
                new Point(0, 2),
                new Point(1, 2),
                new Point(2, 2)
            };

            Life life = new Life();
            //Add the glider to the game of life.
            foreach (Point p in glider)
            {
                life.SetCell(p, true);
            }

            for (int i = 0; i < 4; i++)
            {
                life.Step();
            }

            /*Shift the glider down and right by one unit.  
             * This is the expected position of the glider.*/
            List<Point> expected = new List<Point>();
            foreach (Point p in glider)
            {
                expected.Add(new Point(p.X + 1, p.Y + 1));
            }

            //Check a 10x10 region to make sure the glider is where it should be and everything else is dead.
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Point cell = new Point(i, j);
                    Assert.IsTrue(life.GetCell(cell) == expected.Exists(point => point == cell));
                }
            }
        }
        #endregion
    }
}
