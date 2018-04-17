using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField] Camera _camera;

    Vector3 velocity = Vector3.zero;
    Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
    Quaternion cameraRotation = Quaternion.Euler(0f, 0f, 0f);

    Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        PerformMovement();
        PerformRotation();
    }

    // Gets a movement vector
    public void Move(Vector3 _velocity) {
        velocity = _velocity;
    }

    // Gets character rotation
    public void Rotate(Quaternion _rotation) {
        rotation = _rotation;
    }

    // Gets camera rotation
    public void RotateCamera(Quaternion _cameraRotation) {
        cameraRotation = _cameraRotation;
    }

    // Perform movement based on velocity vector
    private void PerformMovement() {
        if(velocity != Vector3.zero){
            rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        }
    }

    // Perform rotation
    private void PerformRotation() {
        rb.MoveRotation(rb.rotation * rotation);
        if(_camera != null) {
            _camera.transform.rotation = cameraRotation;
        }
    }
}
