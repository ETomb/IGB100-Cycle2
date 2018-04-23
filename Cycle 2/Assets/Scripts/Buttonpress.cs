using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : Interactable {

    public override void Interact(float distance)
    {
        base.Interact(distance);
        GameManager.instance.DeactivateController(this);
    }

    public override void Tooltip()
    {
        base.Tooltip();
    }
}
