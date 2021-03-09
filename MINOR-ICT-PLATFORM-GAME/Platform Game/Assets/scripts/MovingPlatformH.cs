using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformH : MonoBehaviour
{
    public float moveLimit = 2.5f;
    public float speed = 2.0f;
    private int direction = 1;

    Vector3 startMovement;
    Vector3 movementPlane;
    
    void Start()
    {
        startMovement = transform.position;
    }
    
    void FixedUpdate()
    {
        if (transform.position.x > startMovement.x + moveLimit)
        {
            direction = -1;
        }
        else if (transform.position.x < startMovement.x - moveLimit)
        {
            direction = 1;
        }

        GetComponent<Rigidbody>().MovePosition(transform.position + transform.right * direction * Time.fixedDeltaTime);
    }
}
