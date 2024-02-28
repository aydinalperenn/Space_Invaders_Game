using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsEnemyCollision : MonoBehaviour
{

    public Player player;
    public ShipStats shipStats;




    /* private void OnCollisionEnter2D(Collision2D other)
     {
         if (other.gameObject.CompareTag("Alien")){
             //other.gameObject.SetActive(false);
             
             //shipStats.currentLifes--;
             //UIManager.UpdateLives(shipStats.currentLifes);
         }
     }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Alien"))
        {
            Set.currentSet.Remove(collision.gameObject);
            collision.gameObject.SetActive(false);
            player.TakeDamage();
        }
        if (Set.currentSet.Count == 0)
        {
            GameManager.SpawnNewWave();
        }



    }
}
