using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject panel;
    public static bool pausado;

    public AudioMixer mixer;
    public Slider musicaSlider;
    public Slider efeitosSlider;

    private float musicaVol;
    private float efeitosVol;

    void Awake()
    {
        panel.SetActive(false);
        pausado = false;

        mixer.GetFloat("music", out musicaVol);
        musicaSlider.value = musicaVol;
        mixer.GetFloat("effects", out efeitosVol);
        efeitosSlider.value = efeitosVol;
    }

    public void ActivatePause()
    {
        FindObjectOfType<AudioManager>().Play("button");
        Time.timeScale = 0f;
        panel.SetActive(true);
        pausado = true;
    }

    public void DeactivatePause()
    {
        FindObjectOfType<AudioManager>().Play("button");
        Time.timeScale = 1f;
        panel.SetActive(false);
        pausado = false;
    }

    public void MenuButton()
    {
        FindObjectOfType<AudioManager>().Play("button");
        Time.timeScale = 1f;
        pausado = false;
        SceneManager.LoadScene("Menu");
    }

    public void SetVolumeMusica(float volume)
    {
        mixer.SetFloat("music", volume);
    }

    public void SetVolumeEfeitos(float volume)
    {
        mixer.SetFloat("effects", volume);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if (pausado == false)
            {
                ActivatePause();
            } else
            {
                DeactivatePause();
            }
        }
    }
}