using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAlarm : MonoBehaviour {

    public GameObject alarm;
	// Use this for initialization
	void Start ()
    {
        Instantiate(alarm, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
