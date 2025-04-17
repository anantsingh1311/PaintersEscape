using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RedEnemyAI : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D redEnemyAI;

    private bool movingRight;

    public float rightLimit;
    public float leftLimit;

    public float velocity;

    private enum rStates{idle, attack, patrol};
    private rStates redEnemyStates;

    public ColorcollectibleScript collectible;

    //player object reference
    private Transform player;


    void Start()
    {   
        if(redEnemyAI != null){
        redEnemyAI = GetComponent<Rigidbody2D>();
        velocity = 5f;
        movingRight = true;
        }
    }

    // Update is called once per frame
    void Update()
    {


        if(collectible.getColorTexelCollected() && player == null){
            redEnemyStates = rStates.patrol;
        }
        else if(collectible.getColorTexelCollected() && player != null){
            redEnemyStates = rStates.attack;
        }
        else{
            redEnemyStates = rStates.idle;
            player = null;
        }

      


        switch(redEnemyStates){
            case rStates.idle:
            //apply idle
            Idle();
            break;

            case rStates.attack:
            //apply attack logic
            Attack();
            break;

            case rStates.patrol:
            //apply patrol logic
            Patrol();
            break;
        }
        


        
    }

    //Attack logic
    private void Attack(){
        if(player == null){
            redEnemyStates = rStates.patrol;
        }
        float chaseSpeed = 5f * velocity;
        // Vector2 dierection = (player.position - transform.position).normalized;
        // redEnemyAI.velocity = new Vector2(chaseSpeed*dierection.x, chaseSpeed*dierection.y);
        Vector2 direction = (player.position - transform.position).normalized;
        Vector2 targetPosition = (Vector2)transform.position + chaseSpeed * Time.deltaTime * direction;
        redEnemyAI.MovePosition(targetPosition);

    }

    //Stay Idle logic
    private void Idle(){

        redEnemyAI.velocity = new Vector2(0,0);

    }

    // Patrolling Logic 
    private void Patrol (){

        redEnemyAI.velocity = new Vector2(movingRight?velocity:-velocity,redEnemyAI.velocityY);

        if(transform.position.x >= rightLimit){
            movingRight = false;
        }
        if(transform.position.x <= leftLimit){
            movingRight = true;
        }

    }



    //For attack logic, to check if the player is within the reach of the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            Debug.Log("Attack the Player");
            player = collision.transform;
            redEnemyStates = rStates.attack;
        }
        
    
    }

    //For attack logic to check if the player left or not
    private void OnTriggerExit2D(Collider2D collision)
    {
         if(collision.CompareTag("Player")){
            Debug.Log("Leave The Player Alone");
            player = null;
            redEnemyStates = rStates.patrol;
        }
    }






}
