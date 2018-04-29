using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class KeyTerminal : Interactable {

    [SerializeField] int seqSize_Min = 3;                   // Minimum size of the sequence
    [SerializeField] int seqSize_Max = 6;                   // Maximum size of the sequence
    [SerializeField] GameObject inputText;                  
    [SerializeField] GameObject displayText;
    [SerializeField] GameObject underscoreText;

    float _distance;                                        // The distance the player is from the terminal
    List<string> sequence = new List<string> { };           // The number sequence the player needs to input
    List<string> inputSequence = new List<string> { };      // The sequence the player is inputing
    List<string> inputSeqReset = new List<string> { };      // Copy of the initial input sequence
    int inputIndex = 0;                                     // Index where the player is currently inputting
    bool isInteracting = false;                             // Is the player currently interacting with the terminal?
    KeyCode[] keyCodes = {                                  // Input keycodes
         KeyCode.Alpha0,
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9
     };

    public override void Start() {
        // Start as normal
        base.Start();
        // Disable textboxes
        displayText.SetActive(false);
        inputText.SetActive(false);
        underscoreText.SetActive(false);
    }

    private void Update() {
        if (isActive && isInteracting) {
            InteractionCheck();
            CheckInput();
            CheckSequence();
        }
    }

    public override void Activate() {
        // Generate a random number sequence
        GenerateSequence();
        // Ensable textboxes
        displayText.SetActive(true);
        inputText.SetActive(true);
        underscoreText.SetActive(true);
        // Activate as normal
        base.Activate();
    }

    public override void Interact(float distance) {
        // Set boolean to true
        if (isActive && distance <= radius)
            isInteracting = true;
        // Interact as normal
        base.Interact(distance);
    }

    void GenerateSequence() {
        // Make sure the sequence is already empty
        sequence.Clear();
        // Randomly generate the size of the sequence
        int seqSize = Random.Range(seqSize_Min, seqSize_Max);
        // Randomly generate the target sequence, and fill input sequence with _
        for (int i = 0; i < seqSize; i++) {
            sequence.Add(Random.Range(0, 9).ToString() + " ");
            inputSequence.Add("_ ");
        }
        // Set the input sequence reset list to that of the input sequence
        inputSeqReset = inputSequence.ToList();
        // Write the sequences to the text
        WriteToTextBox(sequence, displayText);
        WriteToTextBox(inputSequence, inputText);
        WriteToTextBox(inputSeqReset, underscoreText);
    }

    void InteractionCheck() {
        // Find the vector position of the closest point on the player
        Vector3 closestPoint = player.GetComponent<CharacterController>().ClosestPoint(interactionTransform.position);
        // Find distance to the player
        _distance = Vector3.Distance(closestPoint, interactionTransform.position);
        // Check if this distance is not within the interaction radius
        if (_distance > radius) {
            // Set boolean to false
            isInteracting = false;
        } 
    }

    void CheckInput() {
        // Only check if the player is interacting with the terminal
        if (!isInteracting) {
            return;
        }
        // Check for the player's key inputs
        for (int i = 0; i < keyCodes.Length; i++) {
            if (Input.GetKeyDown(keyCodes[i])) {
                string numberPressed = i.ToString() + " ";
                // Write the input key to the input sequence
                WriteToSequence(numberPressed);
            }
        }
    }

    void CheckSequence() {
        // Check if the input sequence is equivalent to the target sequence
        if (inputSequence.SequenceEqual(sequence)) {
            // Reset the input index
            inputIndex = 0;
            // Reset boolean
            isInteracting = false;
            // Disable textboxes
            displayText.SetActive(false);
            inputText.SetActive(false);
            underscoreText.SetActive(false);
            // Clear lists
            sequence.Clear();
            inputSequence.Clear();
            inputSeqReset.Clear();
            // Deactivate this
            GameManager.instance.DeactivateController(this);
        }
    }

    void WriteToSequence(string inputValue) {
        // Set target value
        string targetValue = sequence[inputIndex];
        // Check if the input value is equivalent to the target value
        if (inputValue == targetValue) {
            // Remove the _
            inputSequence.RemoveAt(inputIndex);
            // Insert input value
            inputSequence.Insert(inputIndex, inputValue);
            // Iterate input index
            inputIndex++;
        } else {
            // Reset input index
            inputIndex = 0;
            // Reset the input sequence
            inputSequence.Clear();
            inputSequence = inputSeqReset.ToList();
        }
        // Write the sequence to the text box
        WriteToTextBox(inputSequence, inputText);
    }

    void WriteToTextBox(List<string> writeSequence, GameObject TextBox) {
        // Set local variable to store the string
        string writeString = "";
        // Write to the local string variable
        foreach(string value in writeSequence) {
            writeString += value; 
        }
        // Write to the text box
        TextBox.GetComponent<Text>().text = writeString;

    }
}
