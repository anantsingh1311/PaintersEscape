using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemyAI : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D redEnemyAI;

    private bool movingRight;

    public float rightLimit;
    public float leftLimit;

    public float velocity;
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
        
        redEnemyAI.velocity = new Vector2(movingRight?velocity:-velocity,redEnemyAI.velocityY);

        if(transform.position.x >= rightLimit){
            movingRight = false;
        }
        if(transform.position.x <= leftLimit){
            movingRight = true;
        }
        
    }
}
