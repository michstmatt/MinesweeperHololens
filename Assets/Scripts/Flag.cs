using UnityEngine;
using System.Collections;

public class Flag : MonoBehaviour
{

    // Use this for initialization
    float yDelta;
    float yFinal;
    bool hide = false;
    void Start()
    {
        yFinal = 1.6f;
        yDelta = .2f;
    }

    // Update is called once per frame
    void Update()
    {
        yDelta = (yFinal - transform.localPosition.y) / 8;
        if(transform.localPosition.y!=yFinal)
        {
            transform.localPosition += new Vector3(0, yDelta, 0);
        }
        else if(hide)
        {
            Destroy(this);
        }

    }
    public void HideFlag()
    {
        yFinal = -yFinal;
        hide = true;
    }

    
}
