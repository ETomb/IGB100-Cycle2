using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float encumSpeed = 2.5f;
    [SerializeField] float xSensitivity = 2f;
    [SerializeField] float ySensitivity = 2f;
    [SerializeField] float maxReach = 1f;
    [SerializeField] Camera _camera;

    PlayerMotor motor;
    float speed;

    GameObject previous;

    private void Start() {
        // Find PlayerMotor
        motor = GetComponent<PlayerMotor>();
        // Set initial speed
        speed = walkSpeed;
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        /// Character movement
        // Calculate movement velocity as a 3d vector
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        // Final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        // Apply movement
        motor.Move(_velocity);

        /// Character Rotation
        // Calculate rotation as a Quaternion
        float _yRotation = Input.GetAxisRaw("Mouse X") * xSensitivity;

        Quaternion _rotation = Quaternion.Euler(0f, _yRotation, 0f);

        // Apply rotation
        motor.Rotate(_rotation);

        /// Camera Rotation
        // Calculate rotation as a Quaternion
        float _xRotation = Input.GetAxisRaw("Mouse Y") * ySensitivity;

        Quaternion _cameraRotation = Quaternion.Euler(-_xRotation, 0f, 0f);

        // Apply camera rotation
        motor.RotateCamera(_cameraRotation);

        /// Raycast control
        RaycastHit hit;

        // Send raycast and see if it hits an object within reach
        if (Physics.Raycast(_camera.transform.position, transform.forward, out hit, maxReach)) {
            GameObject current = hit.collider.gameObject;

            // Only execute events if a different object is being looked at
            if (previous != current) {
                // Execute event on previous object
                ExecuteEvents.Execute<IRaycastEventHandler>(previous, null, (x, y) => x.OnRaycastExit());
                // Execute event on current object
                ExecuteEvents.Execute<IRaycastEventHandler>(current, null, (x, y) => x.OnRaycastEnter());
                // Set previous object to the current object
                previous = current;
            } 
        } else {
            // Execute event on previous object
            ExecuteEvents.Execute<IRaycastEventHandler>(previous, null, (x, y) => x.OnRaycastExit());
            // Since no object is within range, set the previous object to null
            previous = null;
        }
    }
}
