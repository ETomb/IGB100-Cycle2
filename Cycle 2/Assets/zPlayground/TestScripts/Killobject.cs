using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killobject : MonoBehaviour {

    float life = 1.0f;
	// Use this for initialization
	void Start () {
        life += Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > life)
        {
            Destroy(this.gameObject);
        }
	}
}
