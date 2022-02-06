using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CamerAdjust : MonoBehaviour
{
    public float baseWidth = 1024;
    public float baseHeight = 768;
    




    void Update()
    {
        if (Input.GetKeyDown((KeyCode.Escape))) { SceneManager.LoadScene("MainMenu"); }
       
    }
}
