namespace Game.of.Life.V2
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class Program
    {
        public static void Main(string[] args)
        {
            var cells = new List<Cell>
                            {
                                new Cell(11, 9), new Cell(11, 10), new Cell(11, 11), new Cell(11, 12),
                                                 new Cell(12, 10), new Cell(13, 10), new Cell(14, 10),
                                                 new Cell(12, 11), new Cell(13, 11), new Cell(14, 11),
                                                 new Cell(12, 12), new Cell(13, 12), new Cell(14, 12)
                            };

            var grid = new Grid(27, 27);
            grid.Init(cells.ToArray());

            while (true)
            {
                PrintToConsole(grid.Print());
                grid.MutateAndCompleteAllCellsMutation();
                PrintToConsole(grid.Print());

                Thread.Sleep(1000);
                //Console.ReadLine();
                Console.Clear();
            }
        }

        private static void PrintToConsole(IEnumerable<string> boardLines)
        {
            foreach (var line in boardLines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
