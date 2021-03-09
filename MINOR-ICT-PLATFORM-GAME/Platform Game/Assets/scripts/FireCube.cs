using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCube : MonoBehaviour
{
    public Rigidbody bulletPrefab;
    public float shootSpeed = 300;

    private bool fire;

    private GameObject[] bullets;
    int bulletCount;


    private void Update()
    {
        shootBullet();
    }
    
    private void shootBullet()
    {
        bullets = GameObject.FindGameObjectsWithTag("bullet");
        bulletCount = bullets.Length;

        if (Input.GetKeyDown(KeyCode.Mouse0) & bulletCount < 3)
        {
            Rigidbody projectile = Instantiate(bulletPrefab, transform.position + (transform.forward * 2) + (transform.up * 1.1f), transform.rotation);
            projectile.AddForce(transform.forward * shootSpeed, ForceMode.Impulse);
        }
    }
}
