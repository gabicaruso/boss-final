using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winner : MonoBehaviour
{
    public void Start()
    {
        FindObjectOfType<AudioManager>().Play("winner");
    }
    
    public void Happy()
    {
        FindObjectOfType<AudioManager>().Play("button");
        SceneManager.LoadScene("Menu");
    }
}