using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float encumSpeed = 2.5f;
    [SerializeField] float xSensitivity = 2f;
    [SerializeField] float ySensitivity = 2f;

    PlayerMotor motor;
    float speed;

    private void Start() {
        // Find PlayerMotor
        motor = GetComponent<PlayerMotor>();
        // Set initial speed
        speed = walkSpeed;
    }

    private void Update() {
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

    }

}
