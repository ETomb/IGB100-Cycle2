using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killCube : MonoBehaviour {


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void kill()
    {
        Destroy(this.gameObject);
    }
}
