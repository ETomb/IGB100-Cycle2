using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[SerializeField]
public class Interactable : MonoBehaviour, IRaycastEventHandler {

    public float radius = 1f;                // reach radius
    public float _distance;                  // distance of the player
    public Transform interactionTransform;   // transform of the interaction point

    public bool isActive = false;
    bool isFocus = false;

    [HideInInspector] public GameObject player;
    public GameObject alarm, activated;
    public Text tooltipText;
    public string defaultTooltip = "Press 'E' to interact";

    public virtual void Start() {
        // Assign player transform
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        // Assign tooltip text
        tooltipText = GameObject.Find("TooltipText").GetComponent<Text>();
    }

    public virtual void Update() {
        if (isActive && _distance <= radius) {
            Tooltip(true);
        } else {
            Tooltip(false);
        }
    }

    // This method is meant to be overwritten
    public virtual void Interact() {
        Debug.Log("Interacting with " + transform.name);
    }

    // This method is meant to be overwritten
    public virtual void Tooltip(bool active) {
        // Set the active state of the tooltip
        tooltipText.enabled = active;
        if (active) {
            Debug.Log("Tooltip active for " + transform.name);
        }
    }

    // Set the interactable to be active
    public virtual void Activate() {
        isActive = true;
        /// Set indicator to be active
        Instantiate(alarm);
    }

    // Set the interactable to not be active
    public virtual void Deactivate() {
        isActive = false;
    }

    // Debugging Gizmo
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    /// Event message handling
    public void OnRaycastEnter() {
        isFocus = true;
    }

    public void OnRaycastExit() {
        isFocus = false;
    }
}
