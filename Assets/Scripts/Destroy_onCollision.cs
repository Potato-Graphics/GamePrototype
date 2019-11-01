using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_onCollision : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] int tier1Boost = 1, tier2Boost = 2, tier3Boost = 3, tier4Boost = 4, tier5Boost = 5;

    /* Handles the bullet collision
     */
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player" || col.gameObject.tag.Contains("Bullet"))
        {
            //if the bullet collides with an enemy deal damage to said enemy
            if (col.gameObject.tag == "Enemy")
                col.gameObject.GetComponent<Enemy>().UpdateHealth(-5);

            switch(col.gameObject.tag)
            {
                case "Enemy":
                    col.gameObject.GetComponent<Enemy>().UpdateHealth(-5);
                    break;
                case "ScoreTier1":
                    player.UpdateScore(tier1Boost);
                    break;
                case "ScoreTier2":
                    player.UpdateScore(tier2Boost);
                    break;
                case "ScoreTier3":
                    player.UpdateScore(tier3Boost);
                    break;
                case "ScoreTier4":
                    player.UpdateScore(tier4Boost);
                    break;
                case "ScoreTier5":
                    player.UpdateScore(tier5Boost);
                    break;
            }
            //destroy the bullet.
            Destroy(gameObject);
        }
    }
}
