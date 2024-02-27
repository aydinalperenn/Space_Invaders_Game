using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBullet : MonoBehaviour
{
    private float speed = 10f;


    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Alien"))   // düþmanla karþýlaþýrsa
        {
            //Destroy(gameObject);    // mermiyi yok et  // yapmak yanlýþ çünkü havuzdan çekiyoruz

            collision.gameObject.GetComponent<Alien>().Kill();
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("EnemyBullet"))     // düþman mermiyle çarpýþýrsa
        {
            //Destroy(collision.gameObject);  // düþman mermiyi yok et   // objeyi havuzdan çekmemiz sebebiyle yanlýþ
            //Destroy(gameObject);    // kendi mermini yok et          // objeyi havuzdan çekmemiz sebebiyle yanlýþ

            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

    }
}
