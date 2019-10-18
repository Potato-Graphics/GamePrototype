using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_onCollision : MonoBehaviour
{
    
    private void OnCollisionEnter( Collision col )
    {
        //TODO: Handle damage
        Destroy(gameObject);
    }

}
