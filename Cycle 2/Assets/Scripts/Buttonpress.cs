using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttonpress : Interactable {

    private void Start()
    {
        Activate();
    }

    public override void Interact(float distance)
    {
        base.Interact(distance);
        if (distance <= radius)
        {
            //Deactivate();
            GameManager.instance.DeactivateController(this);
        }
    }

    public override void Tooltip()
    {
        base.Tooltip();
    }
}
