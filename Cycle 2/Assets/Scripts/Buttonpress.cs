using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttonpress : Interactable {

    public int red = 1;
    public override void Interact()
    {
        base.Interact();
        red = 2;
        Deactivate();
    }

    public override void Tooltip()
    {
        base.Tooltip();
    }
}
