using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MineSweeperLogic
{
    public class CellRow
    {
        public IList<CellRow> Rows { get; set; }

        public IList<Cell> Cells { get; set; }

        public int RowIndex { get { return Rows.IndexOf(this); } }
 
    }
}
