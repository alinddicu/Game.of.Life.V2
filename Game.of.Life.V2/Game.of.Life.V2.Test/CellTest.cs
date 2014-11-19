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
            var cell = new Cell();
            cell.AddNeighbour(new Cell());
            cell.Mutate();

            Check.That(cell.State).Equals(CellState.Dead);
        }

        [TestMethod]
        // TDD - useless test as it's green when designed
        public void Given1DeadCellWith1AliveNeighboursWhenMutateThenCellStaysDead()
        {
            var cell = new Cell();
            cell.State = CellState.Dead;
            cell.AddNeighbour(new Cell());
            cell.Mutate();

            Check.That(cell.State).Equals(CellState.Dead);
        }

        [TestMethod]
        // TDD - 2nd rule -> useless test as it's green when designed
        public void Given1AliveCellWithMoreThan3AliveNeighboursWhenMutateThenCellDies()
        {
            var cell = new Cell();
            cell.AddNeighbour(new Cell(), new Cell());
            cell.Mutate();
            Check.That(cell.State).Equals(CellState.Alive);

            cell.AddNeighbour(new Cell());
            cell.Mutate();
            Check.That(cell.State).Equals(CellState.Alive);
        }

        [TestMethod]
        // TDD - 3rd rule -> good test as it's red when designed
        public void Given1AliveCellWith2Or3AliveNeighbourWhenMutateThenCellStaysAlive()
        {
            var cell = new Cell();
            cell.AddNeighbour(new Cell(), new Cell(), new Cell(), new Cell());
            cell.Mutate();

            Check.That(cell.State).Equals(CellState.Dead);
        }

        [TestMethod]
        // TDD - 4th rule -> good test as it's red when designed
        public void Given1AliveCellWith3AliveNeighbourWhenMutateThenCellBecomesAlive()
        {
            var cell = new Cell();
            cell.State = CellState.Dead;
            cell.AddNeighbour(new Cell(), new Cell(), new Cell());
            cell.Mutate();

            Check.That(cell.State).Equals(CellState.Alive);
        }
    }
}
