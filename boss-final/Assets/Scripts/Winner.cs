﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winner : MonoBehaviour
{
    public void Game()
    {
        FindObjectOfType<AudioManager>().Play("button");
        SceneManager.LoadScene("Menu");
    }
}