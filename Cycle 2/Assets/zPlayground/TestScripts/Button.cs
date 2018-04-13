using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    int identifier;
    public bool active = false;
    public GameObject timer;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Pressed()
    {
        if (active)
        {
            timer.GetComponent<Timing>().EndTimer();
            active = false;
        }
    }
    public void Activate()
    {
        active = true;
    }
    public void Deactivate()
    {
        active = false;
    }
}
