using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    [SerializeField] GameObject flightBackground;
    [SerializeField] float colourChangeSpeed = 1f;
    [SerializeField] Color[] materialColourStages;
    [SerializeField] Interactable[] interactableControllers;
    [SerializeField] float[] stageEndTimings;


    float failTime;
    List<Interactable> activeControllers;
    Color lerpedColour;
    Material flightBackgroundMaterial;

    private void Awake() {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        // Set Flight Background Material variable if a Flight Background has been assigned
        if (flightBackground != null) {
            flightBackgroundMaterial = flightBackground.GetComponent<MeshRenderer>().material;
        }
    }

    private void ColourShift(Color currentColour, Color targetColour) {
        // Check if the colors are equal
        if(currentColour == targetColour) {
            return;
        }

        // Shift colour by one stage
        Color.Lerp(currentColour, targetColour, Time.deltaTime);
    }

    // Activate specified controller
    private void ActivateController(Interactable controller) {
        // Set to active
        controller.Activate();
        // Add to active list
        activeControllers.Add(controller);
    }

    // Deactivate specified controller
    private void DeactivateController(Interactable controller) {
        // Set to not active
        controller.Deactivate();
        // Remove from active list
        activeControllers.Remove(controller);
    }
}

