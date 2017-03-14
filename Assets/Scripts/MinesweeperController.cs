using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class MinesweeperController : MonoBehaviour
{

    // Use this for initialization
    public static MinesweeperController Instance;
    void Start()
    {
        Instance = this;
        int rows = 10, cols = 10;

        Model = GetComponent(typeof(MinesweeperModel)) as MinesweeperModel;
        Model.NewGame(rows, cols);
        //View = view;

        Status = GameStatus.Playing;
       // NewGame();
    }

    // Update is called once per frame
    void Update()
    {
       
    }


   // private MinesweeperView View;
    private MinesweeperModel Model;
    private GameStatus Status;



    void NewGame()
    {
        var dims = Model.GetDimensions();
       // Model = new MinesweeperModel(dims.First, dims.Second);
      //  View.NewGame(dims.First, dims.Second);
        // View.CellClicked += View_CellClicked;
    }

    public void CellClicked(Cell sender,GameAct act)
    {
        var coors = (sender as Cell).Position;
        int row = coors.First, col = coors.Second;


        if (!Model.GameStarted())
        {
            Model.GenerateMines(row, col);
         
        }
        Debug.Log(act);
        Status = SelectTile(row, col,act);
       

        if (Status == GameStatus.Won)
        {   
            NewGame();
  
        }
        else if (Status == GameStatus.Lost)
        {
            // View.DisplayGameOver(Model);
            // View.UpdateBoard(Model, true);

            //GetComponentsInParent<Cell>().Select(c =>
            //{
            //    if (c != null)
            //        c.Explode();
            //    return c;
            //});


           // NewGame();
        }
    }






    GameStatus SelectTile(int row, int col, GameAct act)
    {
        return Model.SelectTile(row, col, act);
    }
}