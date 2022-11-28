using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{



    [Header("Movement")]
    [SerializeField, Range(0, 10)]
    private float _speed = 5f;
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

        // move player in function of the camera with addforce
        _movement = (horizontal * _camera.transform.right + vertical * _camera.transform.forward).normalized;
        _movement.y = ascend;

        if (gravity)
        {
            _playerRigidbody.AddForce(_movement * _speed, ForceMode.Acceleration);
        }
        else
        {
            _playerRigidbody.AddForce(_movement * _speed, ForceMode.Acceleration);
            _playerRigidbody.velocity = _playerRigidbody.velocity * 0.96f;
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
