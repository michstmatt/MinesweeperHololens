using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float x=0, z=0;

        
        if(Input.GetKey(KeyCode.UpArrow))
        {
            z = .1f;
        }else if(Input.GetKey(KeyCode.DownArrow))
        {
            z = -.1f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            x = .1f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            x = -.1f;
        }


        transform.localPosition += new Vector3(x, 0, z);


    }
}
