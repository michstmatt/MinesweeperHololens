using UnityEngine;
using System;

public class MinesweeperView : MonoBehaviour {


    Cell[,] Board;
    public GameAct Action;

	// Use this for initialization
	void Start () {
        Action = GameAct.Select;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public MinesweeperView(int rows,int cols) { NewGame(rows, cols); }

   // public event EventHandler CellClicked;

    public void NewGame(int rows, int cols)
    {
        Board = new Cell[rows, cols];


        for (int r = 0; r < rows; r++)
        {
          
            for (int c = 0; c < cols; c++)
            {
                GameObject obj = Instantiate(Resources.Load("Cell")) as GameObject;


                Board[r, c] = (obj.GetComponent(typeof(Cell)) as Cell);
                //var cell = Instantiate(Resources.Load("Cell"));
                //UnityEngine.Debug.Log(cell);
                Board[r, c].transform.position = new Vector3(c, 0.5f, r);
                Board[r, c].Position = new Tuple<int, int>(r, c);
                //Board[r, c].CellClicked += CellClicked;
            }

        }
    }





}
