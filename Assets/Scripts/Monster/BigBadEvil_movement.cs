using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BigBadEvil_movement : MonoBehaviour
{
    // From tuto for movement
    public float CircleRadius = 1;
    public float TurnChance = 0.05f;
    public float MaxRadius = 5;

    public float Mass = 15;
    public float MaxSpeed = 3;
    public float MaxForce = 15;

    private Vector3 velocity;
    private Vector3 wanderForce;
    private Vector3 target;
    // end of tuto things

    [Header("Movement")]
    [SerializeField, Range(0, 10)]

    private Rigidbody _headRigidbody; // for collisions

    List<Transform> tail = new List<Transform>(); // Keep Track of Tail, for snake structure
    public GameObject _monsterSegment; // segment of the monster (not the head)

    // Event that occurs before loading level/instancier les items
    void Awake()
    {
        _headRigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Wandering movement
        velocity = Random.onUnitSphere;
        wanderForce = GetRandomWanderForce();

        // Update segments every 300ms
        //InvokeRepeating("Move", 0.3f, 0.3f); 
        InvokeRepeating("Move", 0.3f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        var desiredVelocity = GetWanderForce();
        desiredVelocity = desiredVelocity.normalized * MaxSpeed;

        var steeringForce = desiredVelocity - velocity;
        steeringForce = Vector3.ClampMagnitude(steeringForce, MaxForce);
        steeringForce /= Mass;

        velocity = Vector3.ClampMagnitude(velocity + steeringForce, MaxSpeed);
        transform.position += velocity * Time.deltaTime;
        transform.forward = velocity.normalized;

        Debug.DrawRay(transform.position, velocity.normalized * 2, Color.green);
        Debug.DrawRay(transform.position, desiredVelocity.normalized * 2, Color.magenta);
    }

    // Généralement pour la physique car très régulier (dépend de la clock du PC)
    void FixedUpdate()
    {
        //Move();   // Move based on random
        // TODO 
        // ref: https://gamedev.stackexchange.com/questions/106737/wander-steering-behaviour-in-3d
    }

    // Move our monster based on random values
    void Move()
    {
        // ref: https://noobtuts.com/unity/2d-snake-game
        // Save current position
        Vector3 v = transform.position;

        // Until we reach the end of the snake length, add a new element to our monster
        if (tail.Count < 10)
        {
            // Load element in game
            GameObject g = (GameObject)Instantiate(_monsterSegment,
                                                v,
                                                Quaternion.identity);
            // Keep track of it => add it to list
            tail.Insert(0, g.transform);
        }

        // Move last tail element to where the head was
        tail.Last().position = v;

        // Add to front of list, remove from the back
        tail.Insert(0, tail.Last());
        tail.RemoveAt(tail.Count - 1);

        // /*
        //     fonction pour suivre un mouvement clair
        // */
        // float horizontal = Random.Range(-360, 360);
        // float vertical =  Random.Range(-360, 360);
        // float profondeur =  Random.Range(-360, 360);

        // Vector3 direction = new Vector3(horizontal, profondeur, vertical).normalized;

        // if (direction.magnitude >= 0.1f)
        // {
        //     _headRigidbody.AddForce(direction);
        // }
    }

    // Get a random force to make the head wander around
    private Vector3 GetWanderForce()
    {
        if (transform.position.magnitude > MaxRadius)
        {
            var directionToCenter = (target - transform.position).normalized;
            wanderForce = velocity.normalized + directionToCenter;
        }
        else if (Random.value < TurnChance)
        {
            wanderForce = GetRandomWanderForce();
        }

        return wanderForce;
    }

    // Random force pour wander
    private Vector3 GetRandomWanderForce()
    {
        var monsterCenter = velocity.normalized;
        var randomPoint = Random.insideUnitCircle;

        var displacement = new Vector3(randomPoint.x, randomPoint.y) * CircleRadius;
        displacement = Quaternion.LookRotation(velocity) * displacement;

        var wanderForce = monsterCenter + displacement;
        return wanderForce;
    }
}
