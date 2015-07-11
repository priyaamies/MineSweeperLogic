using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeperLogic;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeperUnitTests
{
    [TestClass]
    public class MineSweeperTests
    {
        [TestMethod]
        public void CreateBoardTest()
        {
            IEnumerable<CellRow> actualCellRows = InitializeBoard();
            Assert.IsTrue(actualCellRows.Count() == 3, "Number of Cell Rows matched");
            Assert.IsTrue(actualCellRows.Where(x => x.Cells.Any(i => i.CellType == CellType.Mine)).Count() == 1, "There was one mine found");
        }
        
        [TestMethod]
        public void ChangeFlagTypeTest()
        {
            Cell cell = new Cell();
            var flagType = PassDifferentFlagType(cell,FlagType.None);
            Assert.IsTrue(flagType == FlagType.Mine, "Flag Type is changed to Mine when None is flagged");

            flagType = PassDifferentFlagType(cell, FlagType.Mine);
            Assert.IsTrue(flagType == FlagType.Flag, "Flag Type is changed to Flag a Mine is flagged?");

            flagType = PassDifferentFlagType(cell, FlagType.Flag);
            Assert.IsTrue(flagType == FlagType.None, "Flag Type is changed to None when flagged?");
        }
        
        [TestMethod()]
        public void CalculateFlippedCellTest()
        {
            Cell flipCell = null;
            IEnumerable<CellRow> actualCellRows = InitializeBoard();   
         
            var allCells = actualCellRows.First().Rows[1].Cells[1].AllCells;
            //Verify that there is one mine
            Assert.IsTrue(allCells.Any(i => i.CellType == CellType.Mine), "There is one mine in the board");
            
            //Pick the center cell to pass for calculation
            foreach (var cell in allCells)
            {
                if (cell.ColumnIndex==1 && cell.RowIndex==1)
                {
                    cell.CellType = CellType.None;
                    flipCell = cell;                    
                    break;
                }
            }          
            var resultblock = Board.CalculateFlippedCell(flipCell);   
        
            //Verify that atleast there are minecount with value 1 adjacent to current block
            Assert.IsTrue(resultblock.Count(i => i.AllAdjacentCells.Any(j => j.MineCount==1))>1,"There is one mine surrounded");
            
        }
 #region HelperMethods

        private static IEnumerable<CellRow> InitializeBoard()
        {
            int size = 3;
            float mineRate = 0.05F;
            IEnumerable<CellRow> actualCellRows;
            actualCellRows = Board.CreateBoard(size, mineRate);
            return actualCellRows;
      	}

 #endregion
    }
}
