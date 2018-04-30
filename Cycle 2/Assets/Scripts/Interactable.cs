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
    public GameObject _light;

    public virtual void Start() {
        // Assign player transform
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    public virtual void Update() {
    }

    // This method is meant to be overwritten
    public virtual void Interact() {
        Debug.Log("Interacting with " + transform.name);
    }

    // Set the interactable to be active
    public virtual void Activate() {
        isActive = true;
        /// Set indicator to be active
        _light.GetComponent<Indicator>().TurnOn();
        Instantiate(alarm);
    }

    // Set the interactable to not be active
    public virtual void Deactivate() {
        isActive = false;
        _light.GetComponent<Indicator>().DestroySelf();
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
