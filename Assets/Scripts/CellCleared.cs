using UnityEngine;
using System.Collections;

public class CellCleared : Cell {
    public CellStatus Status { get { return CellStatus.Clear; } }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        TextMesh cellText = GetComponent<TextMesh>();
        cellText.text = Count == 0 ? "" : Count.ToString();
	}
}
