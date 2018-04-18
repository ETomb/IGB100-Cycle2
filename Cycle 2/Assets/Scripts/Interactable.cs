using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour, IRaycastEventHandler {

    public float radius = 1f;   // reach radius
    public Transform interactionTransform;   // transform of the interaction point

    bool isActive = false;
    bool isInteracting = false;
    bool isFocus = false;
    Transform player;

    private void Start() {
        // Assign player transform
        player = GetComponent<PlayerController>().transform;
    }

    // This method is meant to be overwritten
    public virtual void Interact() {
        Debug.Log("Interacting with" + interactionTransform.name);

        // Set interaction state to be false
        isInteracting = false;
    }

    // This method is meant to be overwritten
    public virtual void Tooltip() {
        Debug.Log("Tooltip active for" + interactionTransform.name);
    }

    // Set whether the object is being interacted with
    public void OnInteracted() {
        isInteracting = true;
    }

    private void Update() {
        // Check if the object is able to be interacted with
        if (isActive && isFocus) {
            // Find distance between player and object
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            // Check if distance between is within the radius
            if (distance <= radius) {
                // Show tooltip
                Tooltip();
                // Check if player is interacting with object
                if (isInteracting) {
                    Interact();
                    // Set object ot be inactive
                    isActive = false;
                }
            }
        }
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
