using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip : MonoBehaviour
{
    public int scoreValue;
    private const float MAX_LEFT = -18;
    private float speed = 15f;

    
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);
        if(transform.position.x<=MAX_LEFT)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FriendlyBullet"))
        {
            UIManager.UpdateScore(scoreValue);      // uý güncellemesi
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
