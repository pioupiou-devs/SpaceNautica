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

    void Move()
    {
        /*_movement.Set(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        _movement = _movement.normalized * _speed * Time.deltaTime;
        _playerRigidbody.MovePosition(transform.position + _movement);*/

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _playerRigidbody.MovePosition(transform.position + moveDirection.normalized * _speed * Time.deltaTime);
        }
    }

    void Rotate()
    {
        rotationX += -Input.GetAxis("Mouse Y") * _rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, -_rotationXLimit, _rotationXLimit);

        _camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _rotationSpeed, 0);
    }
}
