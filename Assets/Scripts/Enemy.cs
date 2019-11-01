using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int distanceToCharge = 6; // distanced required for the enemy to charge
    [SerializeField]
    private const int MAX_HEALTH = 25;
    private int currentHealth; // enemys current health
    [SerializeField]
    private float walkSpeed = 10.0f; // charge speed
    [SerializeField]
    private float chargeSpeed = 50.0f;
    [SerializeField]
    private Transform playerObject; // the player transform object
    [SerializeField]
    private Vector3 playerPosition; // players position
    private Vector3 startPosition;
    [SerializeField]
    private float distance; // distance between enemy and player
    [SerializeField]
    private Vector3 enemyPosition; // enemy position
    [SerializeField]
    private State currentState; // enemys current state
    [SerializeField]
    private Player player; // the player object
    [SerializeField]
    private float idleWalkDistance = 5.0f;
    [SerializeField]
    private EnemyType enemyType; //the enemy type
    [SerializeField] private bool movingRight = true;
    Rigidbody2D rb;
    private float timePassed;
    private Vector3 localScale;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = MAX_HEALTH;
        //Sets the enemy to idle on start
        localScale = transform.localScale;
        startPosition = transform.position;
        
        rb = GetComponent<Rigidbody2D>();
        SetState(State.Idle);

    }

    // Update is called once per frame
    void Update()
    {
        // Initialising the player object
        playerObject = GameObject.FindGameObjectWithTag("Player").transform;
        //Initiliasing the players position
        playerPosition = playerObject.transform.position;
        //Initiliasing the enemy position
        enemyPosition = transform.position;
        //Initiliasing the distance between the enemy and player vector
        distance = Vector3.Distance(playerPosition, enemyPosition);
        timePassed += Time.deltaTime;

        if(GetState() == State.Charging)
            if(timePassed > 5)
                SetState(State.Idle);

        if(GetState() == State.Charging)
        {
            if (playerPosition.x > enemyPosition.x)
            {
                localScale.x = 1;
                transform.localScale = localScale;
            }
            if (playerPosition.x <= enemyPosition.x)
            {
                localScale.x = -1;
                transform.localScale = localScale;
            }
        }
        if (GetState() == State.Idle)
        {
            if (enemyPosition.x > startPosition.x + idleWalkDistance)
                movingRight = false;
            if (enemyPosition.x <= startPosition.x - idleWalkDistance)
                movingRight = true;
            if (movingRight)
                MoveRight();
            else
                MoveLeft();
        }
        // if the player is in range of the enemy
        if (distance < distanceToCharge)
        {
            if (GetState() == State.CoolDown)
                return;
            if (GetState() == State.Idle)
            {
                //If the player is idle and in range the enemy starts to attack.
                SetState(State.Attacking);
            }
        }
        else if (distance >= distanceToCharge)
        {
            //if the player is out of range the enemy is set to idle.
            SetState(State.Idle);
        }
        if (currentHealth <= 0)
            //if the enemy has no remaining health the enemy is set to dead.
            SetState(State.Dead);
    }

    //Handles the enemys state
    public enum State
        {
            Idle,
            Attacking,
            Charging,
            CoolDown,
            Dead
        }

    //Handles the type of the enemy.
    public enum EnemyType
    {
        Boss,
        ChargeNPC
    }

    public EnemyType GetEnemyType()
    {
        return enemyType;
    }

    /* Sets the state of the enemy
     * Params: the new state being assigned to enemy
     */
    public void SetState(State state)
    {
        currentState = state;
        HandleNewState(state);
    }

    //returns the enemys current state
    public State GetState()
    {
        return currentState;
    }

    //Handles the enemys new state
    private void HandleNewState(State state)
    {
        //switch statement to handle various states
        switch(state)
        {
            //if the enemys state is attacking
            case State.Attacking:
                if (GetEnemyType() == EnemyType.ChargeNPC)
                    Charge(); // enemy does the charge attack if it's the charge npc
                    break;
                //if the enemy is on cooldown
            case State.CoolDown:
                rb.velocity = Vector2.zero;
                StartCoroutine(DamageDelay()); // The enemys damage delay is started.
                break;
                //if the enemy is dead
            case State.Dead:
                Destroy(this.gameObject); // The enemy is destroyed.
                break;
        }
    }
    
    //returns the enemys current health
    public int GetHealth()
    {
        return currentHealth;
    }

    /**
     * Updates the enemy health
     * Params: the amount the health is changed.
     */
    public void UpdateHealth(int amount)
    {
        currentHealth += amount;
        print("Enemy Health: " + GetHealth());
    }
    //Handles the delay when the enemy is set to cool down.
    IEnumerator DamageDelay()
    {
        //Enemy is set to idle after waiting for 2 seconds.
        yield return new WaitForSeconds(2);
        SetState(State.Idle);
    }

    private void MoveRight()
    {
        movingRight = true;
        localScale.x = 1;
        transform.localScale = localScale;
        rb.velocity = new Vector2(localScale.x * (walkSpeed * Time.deltaTime), rb.velocity.y);

    }

    private void MoveLeft()
    {
        movingRight = false;
        localScale.x = -1;
        transform.localScale = localScale;
        rb.velocity = new Vector2(localScale.x * (walkSpeed * Time.deltaTime), rb.velocity.y);

    }

    //Handles the enemy charge attack.
    private void Charge()
    {
        //if the enemy is on cool down return
        if (GetState() == State.CoolDown)
            return;
        //if the enemy is charging return
        if (GetState() == State.Charging)
            return;
        timePassed = 0;
        //Moves the enemy towards the player
         rb.velocity = new Vector2(playerPosition.x * (chargeSpeed * Time.deltaTime), rb.velocity.y);
        //Enemy is set to the charging state
        SetState(State.Charging);
    }
}
