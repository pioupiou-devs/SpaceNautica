using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationObject : MonoBehaviour
{
    private const float GRAVITY = 667.4f;

    internal static List<GravitationObject> gravitationObjects;

    private Rigidbody rb;

    [SerializeField]
    private bool canBeAttracted = true;

    private void Awake()
    {
        rb = transform.parent.GetComponentInChildren<Rigidbody>();
        rb.useGravity = false;
    }

    private void Start()
    {
        foreach (GravitationObject gravitationObject in gravitationObjects)
        {
            if (gravitationObject != this && canBeAttracted)
            {
                InitVelocityWith(gravitationObject);
            }
        }
    }

    private void OnEnable()
    {
        if (gravitationObjects == null)
            gravitationObjects = new List<GravitationObject>();

        gravitationObjects.Add(this);
    }

    private void OnDisable()
    {
        gravitationObjects.Remove(this);
    }

    private void FixedUpdate()
    {
        foreach (GravitationObject gravitationObject in gravitationObjects)
        {
            if (gravitationObject != this && gravitationObject.canBeAttracted)
            {
                Attract(gravitationObject);
            }
        }
    }

    private void InitVelocityWith(GravitationObject gravitationObject)
    {
        float distance = Vector3.Distance(transform.position, gravitationObject.transform.position);
        float velocityMagnitude = Mathf.Sqrt(GRAVITY * gravitationObject.rb.mass / distance);

        // Turn the planet around its axis
        transform.LookAt(gravitationObject.transform);

        // Change the velocity to initiate the orbit
        rb.velocity = transform.right * velocityMagnitude;

    }

    private void Attract(GravitationObject objToAttract)
    {
        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distanceSquared = direction.sqrMagnitude;

        if (distanceSquared == 0f)
            return;

        float forceMagnitude = GRAVITY * (rb.mass * rbToAttract.mass) / distanceSquared;
        Vector3 force = direction.normalized * forceMagnitude;
        if (objToAttract.name == "Planet")
            Debug.Log($"[{name} to {objToAttract.name}] => Force: {force}");

        rbToAttract.AddForce(force);
    }

    private void OnDrawGizmos()
    {
        if (gravitationObjects != null)
        {
            foreach (GravitationObject gravitationObject in gravitationObjects)
            {
                if (gravitationObject != this)
                {
                    //Select the Gizmo Color depending on the distance between the objects
                    float distance = Vector3.Distance(transform.position, gravitationObject.transform.position);
                    if (distance < 50)
                        Gizmos.color = Color.red;
                    else if (distance < 100)
                        Gizmos.color = Color.yellow;
                    else
                        Gizmos.color = Color.green;
                    Gizmos.DrawLine(transform.position, gravitationObject.transform.position);

                    //Draw the distance between the objects
                    Vector3 middle = (transform.position + gravitationObject.transform.position) / 2;
                    UnityEditor.Handles.Label(middle, distance.ToString("0.00"));
                }
            }
        }
    }
}
