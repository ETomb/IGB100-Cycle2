using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : MonoBehaviour {

    int click = 0;
    public GameObject hand;
    public GameObject reach;

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {

        if(click == 0 && Input.GetMouseButton(0))
        {
            RaycastHit press;
            if (Physics.Raycast(hand.transform.position, -(hand.transform.position - reach.transform.position).normalized, out press, 1.0f))
            {
                if(press.transform.tag == "Button")
                {
                    press.transform.GetComponent<killCube>().kill();
                }
            }
        }
		
	}
}
