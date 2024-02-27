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
        SpawnNewWave();
    }

    public static void SpawnNewWave()
    {
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
