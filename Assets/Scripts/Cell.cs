using System.Collections;
using System;
using UnityEngine;

public enum CellStatus : int
{
    Clear = 0,
    Unkown = -1,
    Mine = -2,
    FlagCorrect = -3,
    FlagIncorrect = -4,
    MineTripped = -5
}
public class Cell : MonoBehaviour
{

    /// <summary>
    /// Row,Col
    /// </summary>
    public Tuple<int, int> Position { get; set; }

    /// <summary>
    /// Number of neighboring Cells;
    /// </summary>
    public int Count { get; set; }

    private CellStatus _Status;
    private Flag CellFlag;

    public CellStatus Status
    {
        get
        {
            return _Status;
        }
        set
        {
            if (_Status != value)
            {
                if (_Status == CellStatus.FlagIncorrect || _Status == CellStatus.FlagCorrect)
                {
                    CellFlag.HideFlag();

                }
                _Status = value;
                if (value == CellStatus.Clear)
                {


                }
                else if (value == CellStatus.FlagIncorrect || value == CellStatus.FlagCorrect)
                {


                    CellFlag = (Instantiate(Resources.Load("Flag")) as GameObject).GetComponent<Flag>();
                    CellFlag.transform.localPosition = new Vector3(Position.Second, 0, Position.First);
                }
            }
        }
    }

    public event EventHandler CellClicked;

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MinesweeperController.Instance.CellClicked(this, GameAct.Select);

        }

        if (Input.GetMouseButtonDown(1))
        {

            MinesweeperController.Instance.CellClicked(this, GameAct.Flag);
        }

    }


    private float ShrinkHeight = .1f;
    void ShrinkToClear()
    {

        if (transform.localScale.y >= ShrinkHeight)
        {
            transform.localScale += new Vector3(0, -0.1f, 0f);
        }
        transform.localPosition = new Vector3(transform.localPosition.x, .05f, transform.localPosition.z);
    }

    private Vector3 ExplodeVelocity;
    private Vector3 ExplodeRotation;

    public void Explode()
    {
        
        ExplodeVelocity = new Vector3(UnityEngine.Random.Range(-.5f, .5f), UnityEngine.Random.Range(0, .5f), UnityEngine.Random.Range(-.5f, .5f));
        ExplodeRotation = new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f));
        Debug.Log(ExplodeVelocity);
        if(CellFlag!=null)
            CellFlag.HideFlag();

    }



    void OnCollisionEnter(Collision collision)
    {
    }




    // Use this for initialization
    void Start()
    {
        Count = -1;
        Status = CellStatus.Unkown;
    }

    // Update is called once per frame
    void Update()
    {



        TextMesh cellText = GetComponentInChildren<TextMesh>();
        //cellText.text = Count == 0 ? "" : Count.ToString();
        if (Status == CellStatus.Clear)
        {
            ShrinkToClear();
            cellText.text = Count < 1 ? "" : Count.ToString();
        }

        else
            cellText.text = "";



        this.transform.localPosition += ExplodeVelocity;
        this.transform.Rotate(ExplodeRotation);
    }
}
