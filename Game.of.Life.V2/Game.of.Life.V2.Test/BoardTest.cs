namespace Game.of.Life.V2.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;

    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void WhenCreateBoardThenAllCellsAreDead()
        {
            var board = new Board(2, 2);

            Check.That(board.Cells.Count(c => c.State == CellState.Dead)).Equals(board.Width * board.Length);
        }

        [TestMethod]
        public void WhenInitAliveCellsOnBoardThenOnlyTheseCellAreAlive()
        {
            var board = new Board(3, 3);
            var aliveCells = new[] { new Cell(0, 0), new Cell(1, 1), new Cell(2, 2) };
            board.Init(aliveCells);

            Check.That(board.Cells.Where(c => c.State == CellState.Alive)).ContainsExactly(aliveCells);
        }

        [TestMethod]
        public void WhenCreateBoardThenEachCellKnowsItsNeighbours()
        {
            var board = new Board(3, 3);
            var cell11 = board.Cells.Single(c => c.X == 1 && c.Y == 1);
            var cell11Neighbours = board.Cells.Except(new[] { cell11 });

            Check.That(cell11.Neighbours).ContainsExactly(cell11Neighbours);
        }
    }
}
