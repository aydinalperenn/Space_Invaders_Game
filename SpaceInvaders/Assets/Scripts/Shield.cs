using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    public Sprite[] states;
    private int health;
    private SpriteRenderer sr;


    void Start()
    {
        health= 4;
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("FriendlyBullet"))    // d��man veya bizim mermimiz �arparsa
        {
            collision.gameObject.SetActive(false);  // mermi false
            health--;
            if(health<=0)
            {
                Destroy(gameObject);
            }
            else
            {
                sr.sprite = states[health - 1   ];
            }
        }
    }


}
