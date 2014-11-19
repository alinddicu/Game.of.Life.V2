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

        private void SetCellNeighbours(Cell currentCell)
        {
            var startX = currentCell.X - 1 < 0 ? 0 : currentCell.X - 1;
            var startY = currentCell.Y - 1 < 0 ? 0 : currentCell.Y - 1;
            for (int x = startX; IsXInNeighbourRange(currentCell, x); x++)
            {
                for (int y = startY; IsYInNeighbourRange(currentCell, y); y++)
                {
                    AddNeighbourToCell(currentCell, x, y);
                }
            }
        }

        private void AddNeighbourToCell(Cell cell, int x, int y)
        {
            var neighbour = Cells.Single(c => c.X == x && c.Y == y);
            if (neighbour != cell)
            {
                cell.AddNeighbours(neighbour);
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
