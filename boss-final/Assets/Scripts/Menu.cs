using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Start()
    {
        FindObjectOfType<AudioManager>().Play("menu");
    }
    
    public void Game()
    {
        FindObjectOfType<AudioManager>().Play("button");
        SceneManager.LoadScene("Drift Track");
    }
}