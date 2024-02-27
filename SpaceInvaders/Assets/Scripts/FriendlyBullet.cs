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
        if (collision.gameObject.CompareTag("Alien"))   // d��manla kar��la��rsa
        {
            //Destroy(gameObject);    // mermiyi yok et  // yapmak yanl�� ��nk� havuzdan �ekiyoruz

            collision.gameObject.GetComponent<Alien>().Kill();
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("EnemyBullet"))     // d��man mermiyle �arp���rsa
        {
            //Destroy(collision.gameObject);  // d��man mermiyi yok et   // objeyi havuzdan �ekmemiz sebebiyle yanl��
            //Destroy(gameObject);    // kendi mermini yok et          // objeyi havuzdan �ekmemiz sebebiyle yanl��

            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

    }
}
