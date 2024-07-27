using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    [SerializeField] private Slider slider;

    AudioSource musicSound;

    Music music;

    private void Start()
    {
        music = Music.Instance;
        musicSound = music.GetComponent<AudioSource>();
        slider.value = musicSound.volume;
    }

    public void StartButtonClick()
    {
        Set.currentSet.Clear();
        GameManager.ResetGameManager();
        SceneManager.LoadScene(1);
    }
    public void ExitButtonClick()
    {
        Application.Quit();
    }

    public void ChangeSoundLevel()
    {
        musicSound.volume = slider.value;
    }

}
