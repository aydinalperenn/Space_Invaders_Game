using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMaster : MonoBehaviour
{

    [SerializeField] private ObjectPool objectPool = null; 

    public GameObject bulletPrefab;     // düþmanlarýn mermisi

    //ekran sýnýrlarý için
    private Vector3 hMoveDistance = new Vector3(0.2f, 0, 0);   // yatay hareketler için
    private Vector3 vMoveDistance = new Vector3(0, 0.25f, 0);   // dikey hareketler için

    private const float MAX_LEFT = -13.30f;
    private const float MAX_RIGHT = 13.17f;

    public static List<GameObject> allAliens = new List<GameObject>();  // alien list

    private bool isMovingRight;
    private float moveTimer = 0.01f;
    private float moveTime = 0.005f;

    private const float MAX_MOVE_SPEED = 0.02f; // max hareket hýzýný belirledik

    // Düþmanlarýn ateþ etmesi için
    private float shootTimer = 3f;
    private const float shootTime = 3f;

    // mothership için
    public GameObject motherShipPrefab;
    private Vector3 motherShipSpawnPos = new Vector3(15.305f, 3.343f, 0);  // spawn olduðu pozisyon
    private float motherShipTimer = 15f; // 60 f olacak
    private const float MOTHERSHIP_MIN = 5f;   // 15 sn ile 60 sn arasýnda rastgele üretilecek
    private const float MOTHERSHIP_MAX = 15f;

    // GameManager scripti üzerinden alien set spawný için gereken koordinatlar
    private const float START_Y = -2.1f;
    private bool entering = true;
    
    

    void Start()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Alien"))   // tüm düþmanlarý oyun baþý listeye ekliyor
        {
            allAliens.Add(go);
        }
    }


    void Update()
    {
        if (entering)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 10);
            
            if(transform.position.y <= START_Y)
            {
                entering = false;
            }
        }
        else
        {
            if (moveTimer <= 0)
            {
                MoveEnemies();
            }
            moveTimer -= Time.deltaTime;

            if (shootTimer <= 0)
            {
                Shoot();
            }
            shootTimer -= Time.deltaTime;

            if (motherShipTimer <= 0)
            {
                SpawnMotherShip();
            }
            motherShipTimer -= Time.deltaTime;
        }       
    }

    private void SpawnMotherShip()
    {
        Instantiate(motherShipPrefab, motherShipSpawnPos , Quaternion.identity);
        motherShipTimer = Random.Range(MOTHERSHIP_MIN, MOTHERSHIP_MAX);
    }

    private void Shoot()
    {
        Vector2 pos = allAliens[Random.Range(0, allAliens.Count)].transform.position;
        //Instantiate(bulletPrefab, pos, Quaternion.identity);  // sürekli oluþturmak yerine pool içerisinden çekeceðiz
        GameObject obj = objectPool.GetPooledObject();
        obj.transform.position = pos;
        shootTimer = shootTime;
    }

    private void MoveEnemies()
    {
        int hitMax = 0;     // maks noktaya geldiðimizi kontrol etmek için
        if (allAliens.Count > 0) // düþmanlarýmýz varsa
        {
            for (int i = 0; i < allAliens.Count; i++)
            {
                if (isMovingRight)  // saða gidiyorsa
                {
                    allAliens[i].transform.position += hMoveDistance;
                }
                else    // sola gidiyorsa
                {
                    allAliens[i].transform.position -= hMoveDistance;
                }

                if (allAliens[i].transform.position.x > MAX_RIGHT || allAliens[i].transform.position.x < MAX_LEFT)  // maks noktalar arasýndaysa
                {
                    hitMax++;
                }

            }

            if (hitMax > 0) // maks noktanýn üzerine çýkarsa (enlemesine oyun sahnesinin) bir alta insin
            {
                for (int i = 0; i < allAliens.Count; i++)
                {
                    allAliens[i].transform.position -= vMoveDistance;
                }
                isMovingRight = !isMovingRight;
            }
            moveTimer = getMoveSpeed();
        }
    }

    private float getMoveSpeed()    // düþman sayýsý azaldýkça hýzlanmasý lazým
    {
        float f = allAliens.Count * moveTime;

        if (f < MAX_MOVE_SPEED)     
        {
            return MAX_MOVE_SPEED;
        }
        else
        {
            return f;
        }
        
    }
}
