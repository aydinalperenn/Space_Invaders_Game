using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public int scoreValue;  // her bir farklý alien için farklý puan
    public GameObject explosion; // alien vurduðumuzdaki animasyon için
    public GameObject coinPrefab;
    public GameObject lifePrefab;
    public GameObject healthPrefab;

    private const int LIFE_CHANCE = 15;
    private const int HEALTH_CHANCE = 30;
    private const int COIN_CHANCE = 300;

    public void Kill()
    {
        UIManager.UpdateScore(scoreValue);
        AlienMaster.allAliens.Remove(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);

        int ran = Random.Range(0, 1000);
        if (ran <= LIFE_CHANCE)
        {
            Instantiate(lifePrefab, transform.position, Quaternion.identity);
        }
        else if (ran <= HEALTH_CHANCE)
        {
            Instantiate(healthPrefab, transform.position, Quaternion.identity);
        }
        else if (ran <= COIN_CHANCE)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }

        if(AlienMaster.allAliens.Count == 0)
        {
            GameManager.SpawnNewWave();
        }

        gameObject.SetActive(false);
    }


}
