namespace Game.of.Life.V2
{
    using System.Linq;
    using System.Collections.Generic;

    public class Grid
    {
        public Grid(int width, int length)
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
            var startX = cell.X - 1 < 0 ? 0 : cell.X - 1;
            var startY = cell.Y - 1 < 0 ? 0 : cell.Y - 1;
            for (int x = startX; IsXInNeighbourRange(cell, x); x++)
            {
                for (int y = startY; IsYInNeighbourRange(cell, y); y++)
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
