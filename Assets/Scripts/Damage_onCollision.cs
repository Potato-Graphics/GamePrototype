using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_onCollision : MonoBehaviour
{
    public Enemy enemy;
    public Player player;

    /*Handles the enemy collision
     * Params: the collider the enemy collided with
     */ 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if the collider is a player
        if (collision.gameObject.tag == "Player")
        {
            //if the enemy isn't on cooldown
            if (enemy.GetState() != Enemy.State.CoolDown)
            {
                //the enemy damages the player and goes into cool down.
                player.UpdateHealth(-5);
                enemy.SetState(Enemy.State.CoolDown);
            }
        }
    }
}
