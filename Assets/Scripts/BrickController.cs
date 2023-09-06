using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    private GameController gameController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball") 
        {
            gameController = GameObject.FindObjectOfType<GameController>();
            gameController.UpdateScore();
            Destroy(gameObject);
        }
        
    }
}
