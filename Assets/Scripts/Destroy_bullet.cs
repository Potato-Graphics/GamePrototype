using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_bullet : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DestroyTimer());
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
