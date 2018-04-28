using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] float[] stageFailureTimings; // Amount of seconds before the player is considered to have failed to interact with the active controller
    [SerializeField] float gameLength;
    [SerializeField] GameObject[] failIndicators = new GameObject[3];
    [SerializeField] Slider progressSlider;
    [SerializeField] Image failImage;
    [SerializeField] Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    [SerializeField] float flashSpeed = 5f;
    [SerializeField] AudioSource warningSource;



    int maxFailures = 4;
    int currentFailures = 0;
    float failTime;
    bool justFailed = false;
    float nextActiavtionTime = 0;
    bool controllersAreActive;
    List<Interactable> activeControllers = new List<Interactable> { };
    Color lerpedColour;

    public GameObject failsound;

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
        nextActiavtionTime = initialTimingDelay;
        stageEndTimings[stageEndTimings.Length - 1] = gameLength;
    }

    private void Update() {
        // Stage switching
        if (Time.time >= stageEndTimings[currentStageIndex]) {
            if (currentStageIndex < stageEndTimings.Length - 1) {
                currentStageIndex++;
            } else {
                // Load victory scene and destroy this singleton
                SceneManager.LoadScene("Victory");
                Destroy(gameObject);
            }
        }

        if (Time.time >= initialTimingDelay) {
            // Update colour of the flight background
            lerpedColour = ColourShift(lerpedColour, materialColourStages[currentStageIndex + 1]);
            flightBackgroundRenderer.material.color = lerpedColour;

            // Activate controllers at set rate
            if (!controllersAreActive && Time.time >= nextActiavtionTime) {
                RandomlyActivateControllers();
                // Set failure time
                failTime = Time.time + stageFailureTimings[currentStageIndex];

                nextActiavtionTime += stageActivationRates[currentStageIndex];
            }
           

            // Check for failure at set rate
            if (Time.time >= failTime && controllersAreActive)
                Fail();         
        }

        // Check if the player failed
        if (justFailed) {
            // Set colour of the flash image
            failImage.color = flashColour;
            // Record that the player has stopped failing
            justFailed = false;
        } else {
            // Revert colour gradually back to clear
            failImage.color = Color.Lerp(failImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Update progress bar
        if (progressSlider != null) {
            progressSlider.value = Time.time / gameLength;
        }
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

    // Deactivate specified controller (public as it's called by outside scripts
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
        Debug.Log("Checking Active State");
        if (activeControllers.Count != 0) {
            controllersAreActive = true;
        } else {
            controllersAreActive = false;
        }
    }

    // Fail condition
    private void Fail() {
        // Increase number of failures
        currentFailures = currentFailures + 1;

        // Set boolean
        justFailed = true;

        // Play warning sound
        if (warningSource != null) {
            warningSource.Play();
        }

        // Activate indicators
        if (currentFailures < maxFailures) {
            failIndicators[currentFailures - 1].SetActive(true);
        }

        // Check for fail state
        if (currentFailures >= maxFailures) {
            // Load defeat scene and destroy this singleton
            SceneManager.LoadScene("Defeat");
            Destroy(gameObject);
        }

        // Deactivate controllers
        foreach (Interactable controller in activeControllers) {
            DeactivateController(controller);
        }
        //Play fail sound
        Instantiate(failsound);
    }

    // Randomly select a specifies amount of controllers
    private void RandomlyActivateControllers() {
        // Exit the function if the array is of size 0
        if (interactableControllers.Length == 0) {
            return;
        }
        // Get a random index
        int index = Random.Range(0, interactableControllers.Length - 1);
        // Activate controller at selected index
        ActivateController(interactableControllers[index]);
    }
}

