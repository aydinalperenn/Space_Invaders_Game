using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] allAlienSets;       // tüm düþmanlar için set
    private GameObject currentSet;          // var olan set
    private Vector2 spawnPos = new Vector2 (0, 10);

    public static GameManager instance;

    public static int highscore;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Inventory.currentCoins = PlayerPrefs.GetInt("Coin", 0);
        UIManager.updateCoins();
        SpawnNewWave();
    }

    public static void SpawnNewWave()
    {
        int.TryParse(UIManager.instance.scoreText.text, out highscore);
        if (PlayerPrefs.GetInt("HIGHSCORE", 0) < highscore)
        {
            UIManager.instance.highScoreText.text = highscore.ToString();
            Debug.Log("Girdi");
        }


        instance.StartCoroutine(instance.SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        if(currentSet != null)
        {
            Destroy(currentSet);
        }
        yield return new WaitForSeconds(2);
        currentSet = Instantiate(allAlienSets[Random.Range(0, allAlienSets.Length)], spawnPos, Quaternion.identity);
        UIManager.UpdateWave();
    }
}
