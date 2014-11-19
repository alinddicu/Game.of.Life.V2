namespace Game.of.Life.V2.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;

    [TestClass]
    // 1. Any live cell with less than two live neighbours dies, as if caused by under-population.
    // 2. Any live cell with two or three live neighbours lives on to the next generation.
    // 3. Any live cell with more than three live neighbours dies, as if by overcrowding.
    // 4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
    public class CellTest
    {
        [TestMethod]
        // TDD - 1st rule -> 1st test
        public void Given1AliveCellWith1AliveNeighbourWhenMutateThenCellDies()
        {
            var cell = new Cell(1, 1);
            cell.AddNeighbours(new Cell(1, 2));
            cell.Mutate();

            Check.That(cell.State).Equals(CellState.Dead);
        }

        [TestMethod]
        // TDD - useless test as it's green when designed
        public void Given1DeadCellWith1AliveNeighboursWhenMutateThenCellStaysDead()
        {
            var cell = new Cell(1, 1);
            cell.State = CellState.Dead;
            cell.AddNeighbours(new Cell(1, 2));
            cell.Mutate();

            Check.That(cell.State).Equals(CellState.Dead);
        }

        [TestMethod]
        // TDD - 2nd rule -> useless test as it's green when designed
        public void Given1AliveCellWithMoreThan3AliveNeighboursWhenMutateThenCellDies()
        {
            var cell = new Cell(1, 1);
            cell.AddNeighbours(new Cell(0, 1), new Cell(1, 0));
            cell.Mutate();
            Check.That(cell.State).Equals(CellState.Alive);

            cell.AddNeighbours(new Cell(1, 2));
            cell.Mutate();
            Check.That(cell.State).Equals(CellState.Alive);
        }

        [TestMethod]
        // TDD - 3rd rule -> good test as it's red when designed
        public void Given1AliveCellWith2Or3AliveNeighbourWhenMutateThenCellStaysAlive()
        {
            var cell = new Cell(0, 0);
            cell.AddNeighbours(new Cell(-1, -1), new Cell(-1, 0), new Cell(1, 0), new Cell(0, 1));
            cell.Mutate();

            Check.That(cell.State).Equals(CellState.Dead);
        }

        [TestMethod]
        // TDD - 4th rule -> good test as it's red when designed
        public void Given1AliveCellWith3AliveNeighbourWhenMutateThenCellBecomesAlive()
        {
            var cell = new Cell(0, 0);
            cell.State = CellState.Dead;
            cell.AddNeighbours(new Cell(-1, 0), new Cell(-1, -1), new Cell(1, 1));
            cell.Mutate();

            Check.That(cell.State).Equals(CellState.Alive);
        }

        [TestMethod]
        public void CheckCellEquality()
        {
            var cell11 = new Cell(1, 1);
            var cell00 = new Cell(0, 0);
            Check.That(Equals(cell11, cell00)).IsFalse(); 
            Check.That(cell11 == cell00).IsFalse();
            Check.That(Equals(cell00, cell00)).IsTrue();
            Check.That(cell00 == cell00).IsTrue();
            Check.That(Equals(cell11, null)).IsFalse();
            Check.That(Equals(null, cell11)).IsFalse();
        }

        [TestMethod]
        public void GivenNewCellWhenGetStateTheAlive()
        {
            Check.That(new Cell(0, 1).State).IsEqualTo(CellState.Alive);
        }

        [TestMethod]
        public void GivenNewCellWithCoordinatesWhenGetCoordinatesThenCoordinatesAreCorrect()
        {
            var cell = new Cell(1, 1);

            Check.That(cell.X).Equals(1);
            Check.That(cell.Y).Equals(1);
            Check.That(cell.State).IsEqualTo(CellState.Alive);
        }
    }
}