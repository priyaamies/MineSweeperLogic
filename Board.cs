using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MineSweeperLogic
{
    public class Board
    {
	public static IList<CellRow> CreateBoard(int size, float mineRate)
        {
            var total = size * size;
            var mineNum = total * mineRate;
            var random = new Random();
            var mineIndixe = new List<int>();
            // Create blank cells, and adds the location of mines
            for (var i = 0; i < mineNum; i++)
            {
                while (true)
                {
                    var index = random.Next(total + 1);
                    if (!mineIndixe.Contains(index))
                    {
                        mineIndixe.Add(index);
                        break;
                    }
                }
            }
            // Update the cells with mines and blanks
            var cells = new List<Cell>();
            for (var i = 0; i < total; i++)
            {
                if (mineIndixe.Contains(i))
                {
                    cells.Add(new Cell(CellType.Mine));
                }
                else
                {
                    cells.Add(new Cell(CellType.None));
                }
            }
            //Manipulate Cell into CellRow
            var rows = new List<CellRow>();
            for (var i = 0; i < size; i++)
            {
                var rowCells = cells.Skip(i * size).Take(size);
                var cellRow = new CellRow { Cells = rowCells.ToList(), };
                foreach (var b in rowCells)
                {
                    b.Row = cellRow;
                }
                rows.Add(cellRow);
                cellRow.Rows = rows;
            }

            return rows;
        }
    }
}
