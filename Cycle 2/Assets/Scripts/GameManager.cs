using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    [SerializeField] Renderer flightBackgroundRenderer;
    [SerializeField] float colourChangeSpeed = 1f;
    [SerializeField] Color[] materialColourStages;
    [SerializeField] Interactable[] interactableControllers;
    [SerializeField] float initialTimingDelay = 0;
    [SerializeField] int currentStageIndex = 0;
    [SerializeField] float[] stageEndTimings;
    [SerializeField] float[] stageActivationRates; // Activates controllers every X seconds
    [SerializeField] int[] stageActivationNumbers; // Number of controllers to activate
    [SerializeField] float[] stageFailiureTimings; // Amount of seconds before the player is considered to have failed to interact with the active controller
    [SerializeField] int maxFailiures = 3;

    int currentFailiures = 0;
    float failTime;
    float timer = 0;
    bool controllersAreActive;
    List<Interactable> activeControllers;
    Color lerpedColour;

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
        lerpedColour = materialColourStages[0];
    }

    private void Update() {
        // Stage switching
        if (timer >= stageEndTimings[currentStageIndex]) {
            if (currentStageIndex < stageEndTimings.Length - 1) {
                currentStageIndex++;
            } else {
                /// End Game State
            }
        }

        if (timer >= initialTimingDelay) {
            // Update colour of the flight background
            lerpedColour = ColourShift(lerpedColour, materialColourStages[currentStageIndex + 1]);
            flightBackgroundRenderer.material.color = lerpedColour;

            // Activate controllers at set rate
            if ((int)Mathf.Repeat(timer, stageActivationRates[currentStageIndex]) == 0) {
                // Randomly activate controllers
                RandomlyActivateControllers(stageActivationNumbers[currentStageIndex]);
                // Set fail time
                failTime = timer + stageFailiureTimings[currentStageIndex];
            }
            // Check for failiure at set rate
            if (timer == failTime && controllersAreActive)
                Fail();         
        }

        // Increment timer
        timer += Time.deltaTime;
    }

    private Color ColourShift(Color currentColour, Color targetColour) {
        // Check if the colors are equal
        if(currentColour == targetColour) {
            return currentColour;
        }

        // Shift colour
        return Color.Lerp(currentColour, targetColour, Time.deltaTime * colourChangeSpeed);
    }

    // Activate specified controller
    private void ActivateController(Interactable controller) {
        // Set to active
        controller.Activate();
        // Add to active list
        activeControllers.Add(controller);
        // Update active status if need be
        CheckActiveStatus();
    }

    // Deactivate specified controller
    public void DeactivateController(Interactable controller) {
        // Set to not active
        controller.Deactivate();
        // Remove from active list
        activeControllers.Remove(controller);
        // Update active status if need be
        CheckActiveStatus();
    }

    // Check if any controllers are active
    private void CheckActiveStatus() {
        if (activeControllers.Count != 0) {
            controllersAreActive = true;
        } else {
            controllersAreActive = false;
        }
    }

    // Fail condition
    private void Fail() {
        // Deactivate controllers
        foreach (Interactable controller in activeControllers) {
            DeactivateController(controller);
        }

        // Increase number of failiures
        currentFailiures++;

        // Check for fail state
        if (currentFailiures >= maxFailiures) {
            /// Fail state
        }
    }

    // Randomly select a specifies amount of controllers
    private void RandomlyActivateControllers(int amount) {
        float i = 0;
        List<int> selectedIndices = null;
        int index;

        // Loop until enough controllers are chosen
        while (i < amount) {
            // Randomly choose an index
            index = Random.Range(0, interactableControllers.Length - 1);
            // Check if this index has been used in the loop previously
            if (!selectedIndices.Contains(index)) {
                // Add index to the list of used indices
                selectedIndices.Add(index);
                // Activate controller at selected index
                ActivateController(interactableControllers[index]);
                // Increment coutner by 1
                i++;
            }           
        }
    }
}

