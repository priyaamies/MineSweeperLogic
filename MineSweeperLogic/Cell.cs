using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MineSweeperLogic
{
    public enum CellType
    {
        None,
        Mine,
    }

    public enum FlagType
    {
        None,
        Flag,
        Mine,
        Flip,
    }

    public class Cell
    {
        public Cell() { }

        public Cell(CellType cellType)
        {
            this.CellType = cellType;
        }

        #region Declarations

        public CellRow Row { get; set; }

        public int ColumnIndex { get { return Row.Cells.IndexOf(this); } }

        public int RowIndex { get { return Row.RowIndex; } }        

        public IEnumerable<CellRow> Rows { get { return Row.Rows; } }

        public IEnumerable<Cell> AllCells { get { return Rows.SelectMany(x => x.Cells); } }
        
        public int MineCount
        {
            get
            {
                var c = AllAdjacentCells.Where(x => x.CellType == CellType.Mine).Count();
                return c;
            }

        }

        public CellType CellType;

        public FlagType FlagType;

        #region AdjacentCellProperties

        public int Size { get { return Row.Cells.Count; } }

        public Cell Left
        {
            get
            {
                if (ColumnIndex == 0)
                {
                    return null;
                }
                return Row.Cells[ColumnIndex - 1];
            }
        }

        public Cell TopLeft
        {
            get
            {
                if (ColumnIndex == 0 || RowIndex == 0)
                {
                    return null;
                }
                return Row.Rows[RowIndex - 1].Cells[ColumnIndex - 1];
            }
        }

        public Cell Top
        {
            get
            {
                if (RowIndex == 0)
                {
                    return null;
                }
                return Row.Rows[RowIndex - 1].Cells[ColumnIndex];
            }
        }

        public Cell TopRight
        {
            get
            {
                if (ColumnIndex == Size - 1 || RowIndex == 0)
                {
                    return null;
                }
                return Row.Rows[RowIndex - 1].Cells[ColumnIndex + 1];
            }
        }

        public Cell Right
        {
            get
            {
                if (ColumnIndex == Size - 1)
                {
                    return null;
                }
                return Row.Cells[ColumnIndex + 1];
            }
        }

        public Cell BottomRight
        {
            get
            {
                if (ColumnIndex == Size - 1 || RowIndex == Size - 1)
                {
                    return null;
                }
                return Row.Rows[RowIndex + 1].Cells[ColumnIndex + 1];
            }
        }

        public Cell Bottom
        {
            get
            {
                if (RowIndex == Size - 1)
                {
                    return null;
                }
                return Row.Rows[RowIndex + 1].Cells[ColumnIndex];
            }
        }

        public Cell BottomLeft
        {
            get
            {
                if (ColumnIndex == 0 || RowIndex == Size - 1)
                {
                    return null;
                }
                return Row.Rows[RowIndex + 1].Cells[ColumnIndex - 1];
            }
        }

        public IEnumerable<Cell> AllAdjacentCells { get { return (new Cell[] { Left, TopLeft, Top, TopRight, Right, BottomRight, Bottom, BottomLeft }).Where(x => x != null); } }

        #endregion AdjacentCellProperties

        //Adjacent Blocks does not contain any mines
        public bool IsFlippable
        {
            get
            {
                return CellType != CellType.Mine && (FlagType == FlagType.None || FlagType == FlagType.Flag)
                    && AllAdjacentCells.Where(y => y.CellType == CellType.Mine).Count() == 0;
            }
            
        }

        //Adjacent Blocks contains mines
        public bool IsCountable
        {
            get
            {
                return CellType != CellType.Mine && (FlagType == FlagType.None || FlagType == FlagType.Flag)
                    && AllAdjacentCells.Where(y => y.CellType == CellType.Mine).Count() != 0;
            }
            
        }      

        #endregion Properties

        #region Methods

        /*   Method to open a current cell, 
         *   if Mine -> Flip
         *   if Not Mine -> Calculate adjacent cells
         * */
        public static void OpenCurrentCell(Cell cell)
        {
            if (cell.CellType == CellType.Mine)
            {
                cell.FlagType = FlagType.Flip;
                
            }
            else if (cell.FlagType == FlagType.None)
            {
                Board.CalculateFlippedCell(cell);               
            }            
        }
        
        public static FlagType ChangeFlagType(Cell cell)
        {
            switch (cell.FlagType)
            {
                case FlagType.None:
                    cell.FlagType = FlagType.Mine;
                    break;
                case FlagType.Mine:
                    cell.FlagType = FlagType.Flag;
                    break;
                case FlagType.Flag:
                    cell.FlagType = FlagType.None;
                    break;
                default:
                    break;
            }
            return cell.FlagType;
        }

        #endregion Methods
    }
}
