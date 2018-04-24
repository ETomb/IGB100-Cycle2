using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[SerializeField]
public class Interactable : MonoBehaviour, IRaycastEventHandler {

    public float radius = 1f;   // reach radius
    public Transform interactionTransform;   // transform of the interaction point

    public bool isActive = false;
    bool isFocus = false;
    GameObject player;

    //Indicator for play test
    public GameObject ball;

    private void Start() {
        // Assign player transform
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    // This method is meant to be overwritten
    public virtual void Interact(float distance) {
        // If the object isn't active or out of range, do nothing
        if (!isActive || distance > radius) {
            return;
        }

        Debug.Log("Interacting with " + transform.name);
    }

    // This method is meant to be overwritten
    public virtual void Tooltip() {
        Debug.Log("Tooltip active for" + interactionTransform.name);
    }

    // Set the interactable to be active
    public void Activate() {
        isActive = true;
        ball.SetActive(true);
    }

    // Set the interactable to not be active
    public void Deactivate() {
        isActive = false;
    }

    private void Update() {
        // Check if the object is able to be interacted with
        if (isActive && isFocus) {
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
