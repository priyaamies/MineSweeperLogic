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
        }
