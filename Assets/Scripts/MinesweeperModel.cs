using UnityEngine;
using System.Collections;
using System;


public enum GameAct
{
    Flag,
    Select
}
public enum GameStatus
{
    Playing,
    Won,
    Lost
}



public class MinesweeperModel : MonoBehaviour
{

    private Cell[,] Board;
    private int Rows;
    private int Cols;
    private int MineCount;
    private int FlagCorrectCount;
    private int FlagIncorrectCount;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewGame(int _rows, int _cols)
    {
        Rows = _rows; Cols = _cols;
        Board = new Cell[Rows, Cols];
        MineCount = -1;
        for (int r = 0; r < Rows; r++)
            for (int c = 0; c < Cols; c++)
            {
                GameObject obj = Instantiate(Resources.Load("Cell")) as GameObject;


                Board[r, c] = (obj.GetComponent(typeof(Cell)) as Cell);
                //var cell = Instantiate(Resources.Load("Cell"));
                //UnityEngine.Debug.Log(cell);
                Board[r, c].transform.position = new Vector3(c, 0.5f, r);
                Board[r, c].Position = new Tuple<int, int>(r, c);
                Board[r, c].Status = CellStatus.Unkown;
            }
    }

    public Tuple<int, int> GetDimensions() { return new Tuple<int, int>(Rows, Cols); }

    public Cell GetCellAt(int row, int col) { return Board[row, col]; }

    public bool GameComplete()
    {
        return (MineCount == FlagCorrectCount) && (FlagIncorrectCount == 0);
    }
    public bool GameStarted()
    {
        return MineCount != -1;
    }



    public void GenerateMines(int sRow, int sCol)
    {
        MineCount = (int)Math.Sqrt(Rows * Cols);
        System.Random generator = new System.Random(DateTime.Now.Millisecond);

        int row, col;

        for (int i = 0; i < MineCount; i++)
        {
            //generate a random row and column, make sure it isnt a mine and isnt our starting coordinate
            while (Board[row = (int)(generator.NextDouble() * Rows), col = (int)(generator.NextDouble() * Cols)].Status == CellStatus.Mine || (row == sRow && col == sCol)) { }
            Board[row, col].Status = CellStatus.Mine; //set flag
            Debug.Log("generating Mine");
        }
    }

    public int GetMinesRemaining()
    {
        return (MineCount) - (FlagCorrectCount + FlagIncorrectCount);
    }

    public GameStatus UpdateTileCount(int row, int col)
    {
        int mineCount = 0;
        int flagCount = 0;

        if (row < 0 || row >= Rows || col < 0 || col >= Cols)
        {
            return GameStatus.Playing;
        }

        // iterate through all neighboring cells and count up the Mines
        for (int r = row - 1; r <= row + 1; r++)
        {
            for (int c = col - 1; c <= col + 1; c++)
            {
                if (!((r == row) && (c == col)) && (c >= 0) && (c < Cols) && (r >= 0) && (r < Rows))
                {

                    if (Board[r, c].Status == CellStatus.Mine)
                    {
                        mineCount++;
                    }
                    if (Board[r, c].Status == CellStatus.FlagCorrect)
                    {
                        flagCount++;
                        mineCount++;
                    }
                    if (Board[r, c].Status == CellStatus.FlagIncorrect)
                    {
                        flagCount++;
                        mineCount--;
                    }

                }
            }
        }

        if (flagCount > mineCount)
        {
            return GameStatus.Lost;
        }
        Board[row, col].Count = mineCount;
        Board[row, col].Status = CellStatus.Clear;

        // if the count of mines are 0 then we should check all neighboring cells
        if (mineCount == 0 || mineCount == flagCount)
        {
            for (int r = row - 1; r <= row + 1; r++)
            {
                for (int c = col - 1; c <= col + 1; c++)
                {
                    if (!((r == row) && (c == col)) && (c >= 0) && (c < Cols) && (r >= 0) && (r < Rows))
                    {
                        if (Board[r, c].Status == CellStatus.Unkown)
                            UpdateTileCount(r, c);
                    }
                }
            }
        }
        return GameStatus.Playing;
    }

    private void SetFlag(int row, int col)
    {

        /*if /*(Board[row,col].Status == CellStatus.Unkown || Board[row,col].Status == CellStatus.Clear )
			{
			}*/

        if (Board[row, col].Status == CellStatus.Clear)
        {

        }

        else if (Board[row, col].Status == CellStatus.FlagCorrect)
        {
            //remove flag, set back to mine
            Board[row, col].Status = CellStatus.Mine;
            FlagCorrectCount--;
        }
        else if (Board[row, col].Status == CellStatus.FlagIncorrect)
        {
            //remove flag, set back to UNKOWN
            Board[row, col].Status = CellStatus.Unkown;
            FlagIncorrectCount--;
        }
        else if (Board[row, col].Status == CellStatus.Mine)
        {
            Board[row, col].Status = CellStatus.FlagCorrect;
            FlagCorrectCount++;
        }
        else
        {
            Board[row, col].Status = CellStatus.FlagIncorrect;
            FlagIncorrectCount++;
        }
    }

    public GameStatus SelectTile(int row, int col, GameAct act)
    {
        Cell cell = Board[row, col];

        if (act == GameAct.Flag)
        {
            SetFlag(row, col);
            if (GameComplete())
            {
                return GameStatus.Won;
            }

        }
        else
        {
            if (cell.Status == CellStatus.Mine)
            {
                cell.Status = CellStatus.Clear;
                //game over
                cell.Status = CellStatus.MineTripped;
                for (int r = 0; r < Rows; r++)
                    for (int c = 0; c < Cols; c++)
                        Board[r, c].Explode();

                        return GameStatus.Lost;
            }
            else
            {
                return UpdateTileCount(row, col);

            }
        }
        return GameStatus.Playing;
    }


}







