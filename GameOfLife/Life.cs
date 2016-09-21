using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameOfLife
{
    /// <summary>
    /// An implementation of Conway's game of life.
    /// </summary>
    public class Life
    {
        /// <summary>
        /// Set containing all living cells.
        /// </summary>
        /// <remarks>
        /// This was chosen instead of a 2d array in order to support an arbitarily large grid with negative indices.
        /// Using a set is not as efficient as other datastructures, but it is probably the most simple.
        /// </remarks>
        private HashSet<Point> liveCells = new HashSet<Point>();

        public bool this[int x, int y]
        {
            get { return GetCell(new Point(x, y)); }
            set { SetCell(new Point(x, y), value); }
        }

        public Life()
        {
        }

        /// <summary>
        /// Perform one iteration of the game of life.
        /// </summary>
        public void Step()
        {
            HashSet<Point> next = new HashSet<Point>();
            foreach (Point p in GetRelaventCells())
            {
                int count = NeighborCount(p);
                /*Check if a point should be alive.  
                 * We don't explicitly check the rule for overcrowding */
                if (count == 3)
                {
                    next.Add(p);
                }
                else if (count == 2 && liveCells.Contains(p))
                {
                    next.Add(p);
                }
            }
            liveCells = next;
        }

        /// <summary>
        /// Returns the set of cells that are alive or have living neighbors.  
        /// In other words, find all the cells that can potentially change state in the next step.
        /// </summary>
        private HashSet<Point> GetRelaventCells()
        {
            HashSet<Point> relaventCells = new HashSet<Point>();
            foreach (Point p in liveCells)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        relaventCells.Add(new Point(p.X - 1 + i, p.Y - 1 + j));
                    }
                }
            }
            return relaventCells;
        }

        /// <summary>
        /// Assign a cell as either alive or dead.
        /// </summary>
        public void SetCell(Point cell, bool isAlive)
        {
            if (isAlive)
            {
                liveCells.Add(cell);
            }
            else
            {
                liveCells.Remove(cell);
            }
        }

        /// <summary>
        /// Returns whether a cell is alive or dead.
        /// </summary>
        public bool GetCell(Point cell)
        {
            return liveCells.Contains(cell);
        }

        /// <summary>
        /// Returns the number of living neighbors for the provided cell.
        /// </summary>
        public int NeighborCount(Point cell)
        {
            int liveNeighborCount = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == 1 && j == 1)
                    {
                        continue;
                    }
                    if (liveCells.Contains(new Point(cell.X - 1 + i, cell.Y - 1 + j)))
                    {
                        liveNeighborCount++;
                    }
                }
            }
            return liveNeighborCount;
        }

        /// <summary>
        /// Returns a list of all alive cells.
        /// </summary>
        /// <returns></returns>
        public List<Point> GetAllCells()
        {
            return liveCells.ToList();
        }
    }
}
