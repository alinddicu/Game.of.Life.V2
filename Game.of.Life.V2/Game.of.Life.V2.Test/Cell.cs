namespace Game.of.Life.V2.Test
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    
    public class Cell
    {
        private List<Cell> _neighbours;

        public Cell()
        {
            _neighbours = new List<Cell>();
        }

        public CellState State { get; set; }

        public void AddNeighbour(params Cell[] cells)
        {
            _neighbours.AddRange(cells);
        }

        public void Mutate()
        {
            var aliveNeighboursCount = _neighbours.Count(n => n.State == CellState.Alive);
            if (aliveNeighboursCount < 2 || aliveNeighboursCount > 3)
            {
                State = CellState.Dead;
            }

            if (State == CellState.Dead && aliveNeighboursCount == 3)
            {
                State = CellState.Alive;
            }
        }
    }
}
