using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Voltas : MonoBehaviour
{
    Text textComp;

    void Start()
    {
        textComp = GetComponent<Text>();
    }
    
    void Update()
    {
        textComp.text = $"{CarController.voltas}/3";
    }
}