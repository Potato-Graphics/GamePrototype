using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBoss : MonoBehaviour
{

    private Player thePlayer;
    public float moveSpeed;
    public float playerRange;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
    }
}
