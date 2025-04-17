using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorcollectibleScript : MonoBehaviour
{

    private bool colorTexelCollected;

    private void Start()
    {
        colorTexelCollected = false;
        
    }







    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            Debug.Log("Collected");
            colorTexelCollected = true;

            //get SpriteRenderer component from the player
            SpriteRenderer sprite = collision.gameObject.GetComponent<SpriteRenderer>();
            SpriteRenderer collectibleObject = GetComponent<SpriteRenderer>();

            if(sprite != null){
                sprite.color = collectibleObject.color; // HotPink
            }

             gameObject.SetActive(false);

            
        }
    }

    public bool getColorTexelCollected(){
        return colorTexelCollected;
    }




}
