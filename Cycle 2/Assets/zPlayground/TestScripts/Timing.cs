using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timing : MonoBehaviour {

	float timer = 10;//Time to press button
    float between = 5;//Time between buttons
    float totalTime = 5;//Time before initial button and timing variable

    bool active = false;
    bool pause = true;//Starts true to start timer

    public int lives = 3;//How many fails possible
    public int select;//Selecting the button

    public GameObject[] buttons = new GameObject[3];

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (active && totalTime <= Time.time)//If button isn't pressed
        {
            Fail();
        }
        if (pause && totalTime <= Time.time) //Time between active buttons
        {
            Selection();
            StartTimer();
        }
    }

    public void Selection()
    {
        pause = false;
        select = Random.Range(1, 4);
        buttons[select-1].GetComponent<Button>().Activate();
    }

    public void StartTimer()
    {
        totalTime = Time.time + timer;
        active = true;
    }
    public void EndTimer()
    {
        active = false;
        select = 0;
        BetweenTimer();
    }
    public void BetweenTimer()
    {
        totalTime = Time.time + between;
        pause = true;
    }

    private void Fail()
    {
        active = false;
        buttons[select - 1].GetComponent<Button>().Deactivate();//Resets button on fail
        lives--;
        if (lives == 0)
        {
            //Insert failure state
        }
        EndTimer();
    }
}
