using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Voltas1 : MonoBehaviour
{
    Text textComp;

    void Start()
    {
        textComp = GetComponent<Text>();
    }
    
    void Update()
    {
        textComp.text = $"ADVERSÁRIO {CarNpcController.voltasNPC}/3";
    }
}