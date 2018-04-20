using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteract : Interactable {

    public override void Interact() {
        base.Interact();
        Destroy(this);
    }

    public override void Tooltip() {
        base.Tooltip();
    }
}
