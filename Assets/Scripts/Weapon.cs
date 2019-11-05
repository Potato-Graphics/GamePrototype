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

    WaitForSeconds cooldownTime;


    void Start()
    {
        cooldownTime = new WaitForSeconds(0.1f);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0.0f)
        {
            print("test");
            canShoot = true;
            Shoot(true);
        }

        if (Input.GetAxis("Fire1") <= 0.0f)
        {
            canShoot = false;
            Shoot(false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShootDiagonally();
        }
    }

    void Shoot(bool Shoot)
    {
        print("delay " + delay);
        print("can shoot: " + canShoot);
        /*
         * NB Why a while?  PH
        while (!delay)
        {
            canShoot = Shoot;
            if (!canShoot || delay)
                return;
            //Rigidbody test = Instantiate<Rigidbody>(bulletPrefab, firePoint.position, firePoint.rotation);
            Vector3 direction = player.rotation ? transform.up * (speed * Time.deltaTime) * transform.localScale.z : transform.right * (speed * Time.deltaTime) * transform.localScale.x;
            print("transform up: " + transform.up);
            //test.velocity = transform.TransformDirection(direction);
            ShootUpwards();
            delay = true;
            StartCoroutine(ShootPause());
        }
        */
       if (!delay)
        {
            canShoot = Shoot;
            if (!canShoot || delay)
                return;
            Rigidbody test = Instantiate<Rigidbody>(bulletPrefab, firePoint.position, firePoint.rotation);
            Vector3 direction = player.rotation ? transform.up * (speed * Time.deltaTime) * transform.localScale.z : transform.right * (speed * Time.deltaTime) * transform.localScale.x;
           // test.transform.TransformDirection(direction);
            print("transform up: " + transform.up);
            test.velocity = transform.TransformDirection(direction);
            ShootUpwards();
            delay = true;
            StartCoroutine(ShootPause());
        }

    }

    void ShootUpwards()
    {
        rotation = !rotation;
    }

    void ShootDiagonally()
    {
            Rigidbody thisBullet =  Instantiate(bulletPrefab, firePoint.position, firePoint.rotation) as Rigidbody;
        //  Vector3 newRotation = transform.eulerAngles;
        //newRotation.x = 
        //thisBullet.transform.eulerAngles = new Vector3(0, 45, 0);
        Vector3 rotation = new Vector3(0, 45, 0);
        Quaternion rotationAngle = Quaternion.Euler(rotation);
        Vector3 velocity = rotationAngle * (speed * Time.deltaTime) * transform.localScale.x;
        
        thisBullet.velocity = 
            print("shooting diag");
           // thisBullet.transform.rotation = newRotation;
            
    }

    IEnumerator ShootPause()
    {
        yield return cooldownTime;
        delay = false;
    }
}