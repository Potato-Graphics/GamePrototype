using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;
    public Rigidbody bulletPrefab;
    public float speed = 20;
    public bool canShoot = true;
    public bool delay = false;

    public bool rotation = false;

    public Player player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0.0f)
        {
            canShoot = true;
            Shoot(true);
        }

        if (Input.GetAxis("Fire1") <= 0.0f)
        {
            canShoot = false;
            Shoot(false);
        }
    }

    void HandleShootDelay()
    {

    }

    void Shoot(bool Shoot)
    {
        while (!delay)
        {
            canShoot = Shoot;
            if (!canShoot || delay)
                return;
            Rigidbody test = Instantiate<Rigidbody>(bulletPrefab, firePoint.position, firePoint.rotation);
            Vector3 direction = player.rotation ? transform.up * (speed * Time.deltaTime) * transform.localScale.z : transform.right * (speed * Time.deltaTime) * transform.localScale.x;
            print("transform up: " + transform.up);
            test.velocity = transform.TransformDirection(direction);
            HandleShootDelay();
            ShootUpwards();
            delay = true;
            StartCoroutine(ShootPause());
        }
    }

    void ShootUpwards()
    {
        rotation = !rotation;
    }

    IEnumerator ShootPause()
    {
        yield return new WaitForSeconds(.1f);
        delay = false;
    }
}