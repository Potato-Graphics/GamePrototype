using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_onCollision : MonoBehaviour
{

    /* Handles the bullet collision
     */
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player" || col.gameObject.tag.Contains("Bullet"))
        {
            //if the bullet collides with an enemy deal damage to said enemy
            if (col.gameObject.tag == "Enemy")
                col.gameObject.GetComponent<Enemy>().UpdateHealth(-5);
            //destroy the bullet.
            Destroy(gameObject);
        }
    }
}
