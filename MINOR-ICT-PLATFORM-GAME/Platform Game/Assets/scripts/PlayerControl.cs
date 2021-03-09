using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour
{
    public int walkSpeed = 5;
    public int turnSpeed = 70;
    
    public Vector3 jumpV;
    public float jumpForce = 35.0f;

    Rigidbody rb;

    private const string HORIZONTAL_MOVEMENT = "Horizontal";
    private const string VERTICAL_MOVEMENT = "Vertical";

    private float horizontalArrowKeys;
    private float verticalArrowKeys;

    public Vector3 storeVector;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpV = new Vector3(0.0f, 2.7f, 0.0f);
    }

    void Update()
    {
        horizontalArrowKeys = Input.GetAxisRaw(HORIZONTAL_MOVEMENT);
        verticalArrowKeys = Input.GetAxisRaw(VERTICAL_MOVEMENT);
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            jump();
        }
    }

    void FixedUpdate()
    {
        movePlayer();
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag.Equals("playerGravityInversion"))
        {
            GetComponent<Rigidbody>().useGravity = false;
            rb.AddForce(jumpV * jumpForce);
        }
    }

    void OnTriggerExit(Collider collider)
    {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().AddForce(new Vector3());
    }

    private bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, 1);
     }

    void movePlayer()
    {
        Vector3 movement = new Vector3(horizontalArrowKeys, 0.0f, verticalArrowKeys);
        if (verticalArrowKeys != 0)
        {
            transform.Translate(movement * walkSpeed * Time.deltaTime);
            transform.Rotate(new Vector3(0, horizontalArrowKeys * turnSpeed * Time.deltaTime, 0));
        }
        if (horizontalArrowKeys != 0)
        {
            transform.Rotate(new Vector3(0, horizontalArrowKeys * turnSpeed * Time.deltaTime, 0));
        }
    }

    void jump()
        {
            rb.AddForce(jumpV* jumpForce, ForceMode.Impulse);
        }
}