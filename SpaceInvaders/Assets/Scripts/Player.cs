using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;

    //  Ekranýn saðýna ve soluna gidebileceði max noktalar
    private const float maxX = 13.655f;
    private const float minX = -13.655f;

    //private float speed = 5f;     // shipstats'ten çekeceðiz hýzý

    // ateþ etmesi için deðiþkenler
    private bool isShooting = false;
    //private float cooldown = 0.5f;    // shipStats. fireRate kullandýk


    [SerializeField] private ObjectPool objectPool = null;  // mermi objectpoolu için referans oluþturduk

    public ShipStats shipStats;     // ship stats için referans
    
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

        //Baþlangýçta UIManager deðerlerine atama yapýyoruz
        UIManager.UpdateHealthBar(shipStats.currentHealth);
        UIManager.UpdateLives(shipStats.currentLifes);
    }

    void Update()
    {
        // saða sola kontrol
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

        // Ateþ etme
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
        if(shipStats.currentHealth == shipStats.maxHealth)  // max candaysak ama can pickupýný almýþsak puan kazanalým
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
        if (shipStats.currentLifes == shipStats.maxLifes)  // max candaysak ama can pickupýný almýþsak puan kazanalým
        {
            UIManager.UpdateScore(1000);
        }
        else
        {
            shipStats.currentLifes++;
            UIManager.UpdateLives(shipStats.currentLifes);
        }
    }



    private IEnumerator Shoot() // ateþ etme fonksiyonu
    {
        isShooting = true;
        audioSource.PlayOneShot(shootClip);
        //Instantiate(bulletPrefab, transform.position, Quaternion.identity); // mermi oluþturma    //  bu kod sürekli oluþturmak için, aþaðýdaki daha optimize
        GameObject obj = objectPool.GetPooledObject();  // havuzdaki  objeyi çekti
        obj.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(shipStats.fireRate);  // cooldown
        isShooting= false;

    }

    private IEnumerator Respawn()   // respawn fonk.
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            transform.position = offScreenPos;  // gemiyi dýþarý alýcaz

            yield return new WaitForSeconds(2);     // 2 sn beklesin
            shipStats.currentHealth = shipStats.maxHealth;  // respawn olduðunda saðlýðýný yeniden 3 yapsýn (maks health yapsýn)
            transform.position = startPos;      // gemiyi geri oyun alanýna aldýk

            UIManager.UpdateHealthBar(shipStats.currentHealth);     // uý güncellemesi 
            canTakeDamage = true;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet")) // eðer enemybullet ile çarpýþýrsak
        {
            Debug.Log("Player hit!..");
            collision.gameObject.SetActive(false);
            TakeDamage();
        }
    }


    public void TakeDamage()
    {
        shipStats.currentHealth--;
        UIManager.UpdateHealthBar(shipStats.currentHealth);     // Uý güncellemesi

        if (shipStats.currentHealth <= 0)
        {
            shipStats.currentLifes--;
            UIManager.UpdateLives(shipStats.currentLifes);      // uý güncellemesi

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
