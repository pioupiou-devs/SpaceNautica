using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{



    [Header("Movement")]
    [SerializeField, Range(0, 100)]
    private float _speed = 50f;
    private Vector3 _movement;

    [Header("Camera")]
    [SerializeField, Range(0, 100)]
    private float _rotationSpeed = 5;

    [SerializeField, Range(1, 90)]
    private float _rotationXLimit = 45.0f;
    private float rotationX = 0;

    // General variables
    private Rigidbody _playerRigidbody;
    private Camera _camera;
    private bool gravity = false;

    public void enableGravity()
    {
        gravity = true;
    }

    public void disableGravity()
    {
        gravity = true;
    }



    void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private bool isInGravityRange()
    {
        return false;
    }


    private void Gravity()
    {
        if (isInGravityRange())
            enableGravity();
        else
            disableGravity();
    }


    void Move()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float ascend = Input.GetAxisRaw("Jump");

        Camera _camera = GetComponentInChildren<Camera>();

        // calculate the X force to apply
        float xForce = horizontal * _speed;
        // calculate the Z force to apply
        float zForce = vertical * _speed;
        // calculate the Y force to apply
        float yForce = ascend * _speed;

        // Change the direction in function of the camera
        Vector3 direction = _camera.transform.forward * zForce + _camera.transform.right * xForce + _camera.transform.up * yForce;
        // Apply the force

        if (gravity)
        {
            _playerRigidbody.AddForce(direction, ForceMode.Acceleration);
        }
        else
        {
            _playerRigidbody.AddForce(direction, ForceMode.Acceleration);
            _playerRigidbody.velocity *= 0.96f;
        }

    }

    void Rotate()
    {
        rotationX += -Input.GetAxis("Mouse Y") * _rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, -_rotationXLimit, _rotationXLimit);

        Quaternion deltaRotation = Quaternion.Euler(rotationX, 0, 0);

        _camera.transform.localRotation = deltaRotation;
        //_playerRigidbody.MoveRotation(_playerRigidbody.rotation * deltaRotation);

        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _rotationSpeed, 0);
    }
    void Go_up()
    {

    }
}
