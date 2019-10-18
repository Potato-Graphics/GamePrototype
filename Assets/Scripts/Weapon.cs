using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;
    public Rigidbody bulletPrefab;
    public float speed = 20;
    public bool canShoot = true;
    Player player = new Player();
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void HandleShootDelay()
    {

    }

    void Shoot()
    {
        if (!canShoot)
            return;

        Rigidbody test = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation) as Rigidbody;
        int directionChange = player.movingRight ? 90 : 180;
        Vector2 direction = (new Vector2(180, directionChange));
        Debug.Log("the heading angle is: " + direction);
        test.velocity = transform.TransformDirection(direction.x, direction.y, speed);
        HandleShootDelay();
    }
}