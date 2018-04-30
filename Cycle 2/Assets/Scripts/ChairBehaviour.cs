using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class ChairBehaviour : Interactable {

    [SerializeField] AudioClip audioClip;

    Transform playerCam;
    Transform playerTransform;
    AudioSource source;
    bool hasPlayer = false;
    bool beingCarried = false;
    bool touched = false;
    bool isInteracted = false;

	// Use this for initialization
	public override void Start () {
        base.Start();
        // Assign transforms
        playerTransform = player.transform;
        playerCam = player.GetComponentInChildren<Camera>().transform;

	}
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
        // Set has player boolean
        hasPlayer = HasPlayer();
        // Check if carrying has been initiated
        if (hasPlayer && isInteracted) {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam;
            beingCarried = true;
            isInteracted = false;
            player.GetComponent<Characters.FirstPersonController>().notEncum = false;
        }
        // Behaviour when the chair is being carried
        if (beingCarried) {
            // Behaviour for when the carried object collides with another
            if (touched) {
                transform.parent = null;
                beingCarried = false;
                touched = false;
                player.GetComponent<Characters.FirstPersonController>().notEncum = true;
            }
            // Place behaviour
            if (Input.GetAxisRaw("Throw") == 1) {
                transform.parent = null;
                beingCarried = false;
                player.GetComponent<Characters.FirstPersonController>().notEncum = true;
            }
        }
	}

    public override void Interact() {
        base.Interact();
        // set bool to true
        isInteracted = true;
    }

    // Determines if the player is able to interact
    bool HasPlayer() {
        // If the player is within the interaction radius, return true
        if (_distance <= radius) {
            return true;
        }
        // Otherwise return false
        return false;
    }

    private void OnTriggerEnter() {
        if (beingCarried)
            touched = true;
    }
}
