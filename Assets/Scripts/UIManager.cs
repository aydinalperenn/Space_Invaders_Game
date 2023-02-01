using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI scoreText;
    private int score;
    public TextMeshProUGUI highScoreText;
    private int highScore;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI waveText;
    private int wave;
    public Image[] lifeSprites;
    public Image healthBar;
    public Sprite[] healthBars;

    private Color32 active = new Color(1,1,1,1);
    private Color32 inactive = new Color(1,1,1,0.25f);  // rgbA (A deðerini) deðiþtiriyoruz, saydamlýk veriyoruz inaktif olduðunda

    private void Awake()
    {
        highScoreText.text = PlayerPrefs.GetInt("HIGHSCORE").ToString();

        // bu obje (panel) varsa yok et, yoksa oluþtur. Bu yaygýn bir pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // daha hýzlý eriþmek için static tanýmladým
    public static void UpdateLives(int l)
    {
        foreach(Image i in instance.lifeSprites)
        {
            i.color = instance.inactive;
        }
        for(int i = 0; i<l; i++)
        {
            instance.lifeSprites[i].color = instance.active;
        }

    }

    public static void UpdateHealthBar(int h)
    {
        instance.healthBar.sprite = instance.healthBars[h];
    }

    public static void UpdateScore(int s)
    {
        instance.score += s;
        instance.scoreText.text = instance.score.ToString("00000");
    }

    public static void updateHighScore()
    {
        //TODO
    }

    public static void UpdateWave()
    {
        instance.wave++;
        instance.waveText.text = instance.wave.ToString();
    }

    public static void updateCoins()
    {
        instance.coinsText.text = Inventory.currentCoins.ToString();
    }
}
