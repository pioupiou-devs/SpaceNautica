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

        Vector3 direction = new Vector3(horizontal, ascend, vertical)/*.normalized*/;

        //Debug.Log("direction " + direction); 

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
        Vector3 front = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        //Debug.Log("front " + front); 
        Vector3 moveDirection = direction * front.x;

        //Debug.Log("move direction " + moveDirection);



        if (gravity)
        {
            _playerRigidbody.AddForce(direction * 20f);
        }
        else
        {
            _playerRigidbody.AddForce(direction * 20f);
            _playerRigidbody.velocity = _playerRigidbody.velocity * 0.96f;
        }

        /*if (direction.magnitude >= 0.1f)
        {  
            //_playerRigidbody.AddForce(direction);
            //float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
            //Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //_playerRigidbody.MovePosition(transform.position + moveDirection.normalized * _speed * Time.deltaTime);
            
        }*/

        //https://docs.unity3d.com/ScriptReference/Rigidbody.AddTorque.html add torque -> rotate on himself
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
