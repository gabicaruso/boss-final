using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public void Start()
    {
        FindObjectOfType<AudioManager>().Play("loser");
    }
    
    public void End()
    {
        FindObjectOfType<AudioManager>().Play("button");
        SceneManager.LoadScene("Menu");
    }
}