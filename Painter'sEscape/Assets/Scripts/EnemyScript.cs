using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D enemy;
    //velocity of the enemy moving left and right
    private float velocity;


    //to check if the enemy is moving right
    private bool movingRight;

    // to set the boundary of movement towards left 
    public float leftLimit;
    // to set the boundary on the right 
    public float rightLimit;

    //refrence of the color collectible script to check if the color was collected 
    public ColorcollectibleScript collectible;

    //3 different enemy states 
    public enum States{idle, patrol, attack}

    // to check the current enemyState
    private States curr_enemyState;

    //private refrence to the player
    private Transform Player;

    //slightly faster than the patrol state
    float velocityChase;

    
    void Start()
    {
        velocity = 5f;
        velocityChase = 1.2f*velocity;
    }

    // Update is called once per frame
    void Update()
    {
        //The enemy attacks you if you collect the color texel
        if(collectible.getColorTexelCollected() && Player == null){
            curr_enemyState = States.patrol;
        }
        else if (collectible.getColorTexelCollected() && Player != null){
            curr_enemyState = States.attack;
        }
        else{
            
            curr_enemyState = States.idle;
            Player = null;
        }


          switch(curr_enemyState){
            case States.idle:
            Idle();
            break;

            case States.attack:
            Attack();
            break;

            case States.patrol:
            Patrol();
            break;

            default:

            break;


        }
        
    }

    //State-1 Idle
    private void Idle(){
        enemy.velocity = new Vector2(0,0);
    }


    //State-2 Patrol
    private void Patrol(){

        //updating enemy position between two movement ranges
        enemy.velocity = new Vector2(movingRight ? velocity : -velocity, enemy.velocity.y);

        // if the enemy reaches right limit moving right is set false
        if(transform.position.x >= rightLimit){

            movingRight = false;

        }
        // if enemy reaches left limit, set moving right true
        else if(transform.position.x <= leftLimit){

            movingRight = true;

        }

    }

    //State-3
    private void Attack(){
        
        Vector2 dierection = (Player.position - transform.position).normalized;
        //chase the player
        enemy.velocity = new Vector2(velocityChase*dierection.x,enemy.velocity.y);


    }


    //If the enemy and the user are near each other, the enemy detects the user and runs behind it to chase it
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            Debug.Log("Player Detected, AttackNow");
            //when the player detects enemy, set the state to chase
            Player = collision.transform;
            curr_enemyState = States.attack;
        }
    }

     private void OnTriggerExit2D(Collider2D collison) {
        if(collison.CompareTag("Player")){
            //The enemy starts patrolling when the player leaves
             Debug.Log("Leave Player alone");
            Player = null;
            curr_enemyState = States.patrol;
        }
    }




}
