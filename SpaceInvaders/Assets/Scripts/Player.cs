using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;

    //  Ekran�n sa��na ve soluna gidebilece�i max noktalar
    private const float maxX = 13.655f;
    private const float minX = -13.655f;

    //private float speed = 5f;     // shipstats'ten �ekece�iz h�z�

    // ate� etmesi i�in de�i�kenler
    private bool isShooting = false;
    //private float cooldown = 0.5f;    // shipStats. fireRate kulland�k


    [SerializeField] private ObjectPool objectPool = null;  // mermi objectpoolu i�in referans olu�turduk

    public ShipStats shipStats;     // ship stats i�in referans
    
    private Vector2 offScreenPos = new Vector2 (0, -20f);
    private Vector2 startPos = new Vector2(0.068f, -7.243f);

    public int highscore;


    private AudioSource audioSource;
    [SerializeField] private AudioClip shootClip;

    bool canTakeDamage = true;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        shipStats.currentHealth = shipStats.maxHealth;
        shipStats.currentLifes= shipStats.maxLifes;
        transform.position = startPos;

        //Ba�lang��ta UIManager de�erlerine atama yap�yoruz
        UIManager.UpdateHealthBar(shipStats.currentHealth);
        UIManager.UpdateLives(shipStats.currentLifes);
    }

    void Update()
    {
        // sa�a sola kontrol
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if(transform.position.x > minX)
            {
                transform.Translate(Vector2.left * Time.deltaTime * shipStats.shipSpeed);
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if(transform.position.x < maxX)
            {
                transform.Translate(Vector2.right * Time.deltaTime * shipStats.shipSpeed);
            }
        }

        // Ate� etme
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if (!isShooting)
            {
                StartCoroutine(Shoot());
            }
        }
#endif
        
    }


    public void AddHealth()
    {
        if(shipStats.currentHealth == shipStats.maxHealth)  // max candaysak ama can pickup�n� alm��sak puan kazanal�m
        {
            UIManager.UpdateScore(250);
        }
        else
        {
            shipStats.currentHealth++;
            UIManager.UpdateHealthBar(shipStats.currentHealth);
        }
    }

    public void AddLife()
    {
        if (shipStats.currentLifes == shipStats.maxLifes)  // max candaysak ama can pickup�n� alm��sak puan kazanal�m
        {
            UIManager.UpdateScore(1000);
        }
        else
        {
            shipStats.currentLifes++;
            UIManager.UpdateLives(shipStats.currentLifes);
        }
    }



    private IEnumerator Shoot() // ate� etme fonksiyonu
    {
        isShooting = true;
        audioSource.PlayOneShot(shootClip);
        //Instantiate(bulletPrefab, transform.position, Quaternion.identity); // mermi olu�turma    //  bu kod s�rekli olu�turmak i�in, a�a��daki daha optimize
        GameObject obj = objectPool.GetPooledObject();  // havuzdaki  objeyi �ekti
        obj.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(shipStats.fireRate);  // cooldown
        isShooting= false;

    }

    private IEnumerator Respawn()   // respawn fonk.
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            transform.position = offScreenPos;  // gemiyi d��ar� al�caz

            yield return new WaitForSeconds(2);     // 2 sn beklesin
            shipStats.currentHealth = shipStats.maxHealth;  // respawn oldu�unda sa�l���n� yeniden 3 yaps�n (maks health yaps�n)
            transform.position = startPos;      // gemiyi geri oyun alan�na ald�k

            UIManager.UpdateHealthBar(shipStats.currentHealth);     // u� g�ncellemesi 
            canTakeDamage = true;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet")) // e�er enemybullet ile �arp���rsak
        {
            Debug.Log("Player hit!..");
            collision.gameObject.SetActive(false);
            TakeDamage();
        }
    }


    public void TakeDamage()
    {
        shipStats.currentHealth--;
        UIManager.UpdateHealthBar(shipStats.currentHealth);     // U� g�ncellemesi

        if (shipStats.currentHealth <= 0)
        {
            shipStats.currentLifes--;
            UIManager.UpdateLives(shipStats.currentLifes);      // u� g�ncellemesi

            if (shipStats.currentLifes <= 0)
            {
                int.TryParse(UIManager.instance.scoreText.text, out highscore);
                if (PlayerPrefs.GetInt("HIGHSCORE") < highscore)
                {
                    PlayerPrefs.SetInt("HIGHSCORE", highscore);
                }
                Set.currentSet.Clear();
                SceneManager.LoadScene(0);
                
            }
            else
            {
                //Debug.Log("Respawn");
                StartCoroutine(Respawn());
            }
        }      
    }

}
