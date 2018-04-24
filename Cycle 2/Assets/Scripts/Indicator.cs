using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour {

    private Vector3 position;
	public void Start()
    {
        gameObject.SetActive(false);
    }
    public void DestroySelf()
    {
        gameObject.SetActive(false);
    }
}
