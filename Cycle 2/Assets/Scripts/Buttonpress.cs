using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : Interactable {
    public override void Interact(float distance)
    {
        base.Interact(distance);
        if (distance <= radius)
        {
            GameManager.instance.DeactivateController(this);
            ball.SetActive(false);
        }
    }

    public override void Tooltip()
    {
        base.Tooltip();
    }
}
