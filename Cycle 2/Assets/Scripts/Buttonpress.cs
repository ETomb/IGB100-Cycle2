using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : Interactable {
    public override void Interact(float distance)
    {
        base.Interact(distance);
        if (distance <= radius)
        {
            Instantiate(activated);
            GameManager.instance.DeactivateController(this);
            /// Set indicator to be inactive
        }
    }

    public override void Tooltip()
    {
        base.Tooltip();
    }
}
