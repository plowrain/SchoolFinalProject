using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Botton : MonoBehaviour
{
    // Start is called before the first frame update
    public Text playerName;////(1)
    public void EnterPlayerName(Text enterText)
    {////(2)
        playerName.text = enterText.text;////(3)
    }

        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
