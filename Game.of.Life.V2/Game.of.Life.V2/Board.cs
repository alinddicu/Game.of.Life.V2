namespace Game.of.Life.V2
{
    using System.Linq;
    using System.Collections.Generic;

    public class Board
    {
        public Board(int width, int length)
        {
            Width = width;
            Length = length;
            Cells = new List<Cell>();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Length; y++)
                {
                    Cells.Add(new Cell(x, y, CellState.Dead));
                }
            }

            Cells.ForEach(SetCellNeighbours);
        }

        public int Width { get; private set; }

        public int Length { get; private set; }

        public List<Cell> Cells { get; private set; }

        public void Init(params Cell[] aliveCells)
        {
            aliveCells.ToList().ForEach(aliveCell => { Cells.Single(cell => cell == aliveCell).State = aliveCell.State; });
        }

        private void SetCellNeighbours(Cell cell)
        {
            for (int x = cell.X - 1; IsXInNeighbourRange(cell, x); x++)
            {
                for (int y = cell.Y - 1; IsYInNeighbourRange(cell, y); y++)
                {
                    AddNeighbourToCell(cell, x, y);
                }
            }
        }

        private void AddNeighbourToCell(Cell cell, int x, int y)
        {
            var neighBour = Cells.Single(c => c.X == x && c.Y == y);
            if (neighBour != cell)
            {
                cell.AddNeighbours(neighBour);
            }
        }

        private bool IsYInNeighbourRange(Cell cell, int y)
        {
            return y >= 0 && y < Length && y <= cell.Y + 1;
        }

        private bool IsXInNeighbourRange(Cell cell, int x)
        {
            return x >= 0 && x < Width && x <= cell.X + 1;
        }
    }
}
