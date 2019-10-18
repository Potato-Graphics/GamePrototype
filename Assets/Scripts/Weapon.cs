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

        Rigidbody test = Instantiate<Rigidbody>(bulletPrefab, firePoint.position, firePoint.rotation);
        Vector3 direction = transform.right * (speed * Time.deltaTime) * transform.localScale.x;
        test.velocity = transform.TransformDirection(direction);
        HandleShootDelay();

    }
}