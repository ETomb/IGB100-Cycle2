using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : Interactable {
    public override void Interact() {
        base.Interact();
        if (_distance <= radius) {
            Instantiate(activated);
            GameManager.instance.DeactivateController(this);
            /// Set indicator to be inactive
        }
    }

    public override void Tooltip(bool active) {
        tooltipText.GetComponent<Text>().text = defaultTooltip;
        base.Tooltip(active);
    }
}
