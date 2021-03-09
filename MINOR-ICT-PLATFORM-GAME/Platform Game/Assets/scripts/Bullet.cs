using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 RotateAmount = new Vector3(250, 100, 250); 

    public DurationBullet lifetime;

    void Start()
    {
        Destroy(gameObject, lifetime.duration);
    }
    
    void FixedUpdate()
    {
        transform.Rotate(RotateAmount * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().removeHealth();
            Destroy(gameObject);
        }
    }
}