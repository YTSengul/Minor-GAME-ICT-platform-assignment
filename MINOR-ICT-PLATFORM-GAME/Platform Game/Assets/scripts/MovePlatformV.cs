using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformV : MonoBehaviour
{
    public float moveLimit = 2.5f;
    public float speed = 2.0f;
    private int direction = 1;

    Vector3 startMovement;
    Vector3 movementPlane;
    
    private void Start()
    {
        startMovement = transform.position;
    }
    
    private void FixedUpdate()
    {
        if (transform.position.y > startMovement.y + moveLimit)
        {
            direction = -1;
        }
        else if (transform.position.y<startMovement.y - moveLimit)
        {
            direction = 1;
        }

        GetComponent<Rigidbody>().MovePosition(transform.position + transform.up* direction * Time.fixedDeltaTime);
    }
}
