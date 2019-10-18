using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        public int distanceToCharge = 0;
        public Rigidbody enemy;
        public Vector3 direction;
        public int health = 20;
        
    }

    // Update is called once per frame
    void Update()
    {
        var playerObject = GameObject.Find("Player");
        Vector3 playerPosition = playerObject.transform.position;
        Vector3 enemyPosition = transform.position;
        Vector3 distance = Vector3.distance(playerPosition, enemyPosition);
        if(distance.x < distanceToCharge)
            Charge();
        if(health <= 0)
            Destroy(enemy);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
        player.UpdateHealth(-5);
        }
    }

    private void Charge()
    {
        direction = player.transform.position * (speed * Time.deltaTime) * transform.localScale.x;
        enemy.velocity = transform.TransformDirection(direction);
    }
}
