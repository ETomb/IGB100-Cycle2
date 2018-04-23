using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteract : Interactable {

    private void Update() {
        Activate();
    }

    public override void Interact(float distance) {
        base.Interact(distance);
    }

    public override void Tooltip() {
        base.Tooltip();
    }
}
