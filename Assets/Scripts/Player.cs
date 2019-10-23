using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]

public class Player : MonoBehaviour
{
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;

    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 8;
    float gravity;
    float jumpVelocity;
    [SerializeField]
    private const int MAX_HEALTH = 3;
    public int currentHealth;
    float velocityXSmoothing;
    public bool rotation = false;
    public bool isAttackable = true;

    public float direction;

    Vector3 velocity;

    Controller2D controller;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MAX_HEALTH;
        controller = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + " Jump Velocity: " + jumpVelocity);
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void UpdateHealth(int amount)
    {
        currentHealth += amount;

    }

    public void DealDamage(int amount)
    {
        if (!isAttackable)
            return;
        UpdateHealth(-amount);
        print("Player Health: " + GetHealth());
        isAttackable = false;
    }


    IEnumerator DamagedDelay()
    {
        yield return new WaitForSeconds(2);
        isAttackable = true;
    }

    void Update()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        if (!isAttackable)
            StartCoroutine(DamagedDelay());

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //print(input);

        if(input.x < 0)
        {
            direction = -1;
        }
        else if(input.x > 0)
        {
            direction = 1;
        }
        else { direction = 0; }

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeGrounded);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.W))
        {
            rotation = true;
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            rotation = false;
        }
    }
}
