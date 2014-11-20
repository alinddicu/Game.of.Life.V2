namespace Game.of.Life.V2
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using System;

    public class Grid
    {
        public Grid(uint width, uint heigth)
        {
            Width = width;
            Height = heigth;
            Cells = new List<Cell>();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Cells.Add(new Cell(x, y, CellState.Dead));
                }
            }

            Cells.ForEach(SetCellNeighbours);
        }

        public uint Width { get; private set; }

        public uint Height { get; private set; }

        public List<Cell> Cells { get; private set; }

        public void Init(params Cell[] aliveCells)
        {
            aliveCells.ToList().ForEach(aliveCell => { Cells.Single(cell => cell == aliveCell).CurrentState = aliveCell.CurrentState; });
        }

        public void MutateAndCompleteAllCellsMutation()
        {
            Cells.ForEach(cell => cell.Mutate());
            Cells.ForEach(cell => cell.CompleteMutation());
        }

        public IEnumerable<string> Print()
        {
            var line = string.Empty;
            for (int yGrid = 0; yGrid < Height; yGrid++)
            {
                line = string.Empty;
                for (int xGrid = 0; xGrid < Width; xGrid++)
                {
                    var cell = Cells.Single(c => c.X == xGrid && c.Y == yGrid);
                    line += cell.CurrentState == CellState.Alive ? "+" : " ";
                }
                yield return line;
                yield return Environment.NewLine;
            }
        }

        private void SetCellNeighbours(Cell currentCell)
        {
            var startX = currentCell.X - 1 < 0 ? 0 : currentCell.X - 1;
            var startY = currentCell.Y - 1 < 0 ? 0 : currentCell.Y - 1;
            for (int xNeighbour = startX; IsXInNeighbourRange(currentCell, xNeighbour); xNeighbour++)
            {
                for (int yNeighbour = startY; IsYInNeighbourRange(currentCell, yNeighbour); yNeighbour++)
                {
                    AddNeighbourToCell(currentCell, xNeighbour, yNeighbour);
                }
            }
        }

        private void AddNeighbourToCell(Cell cell, int xNeighbour, int yNeighbour)
        {
            var neighbour = Cells.Single(c => c.X == xNeighbour && c.Y == yNeighbour);
            if (neighbour != cell)
            {
                cell.AddNeighbours(neighbour);
            }
        }

        private bool IsYInNeighbourRange(Cell cell, int yGrid)
        {
            return yGrid >= 0 && yGrid < Height && yGrid <= cell.Y + 1;
        }

        private bool IsXInNeighbourRange(Cell cell, int xGrid)
        {
            return xGrid >= 0 && xGrid < Width && xGrid <= cell.X + 1;
        }
    }
}
