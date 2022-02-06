using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D playerRigidbody2D;
    //Material _material= new Material(Shader.Find("Custom/SpriteOutline"));
    Material _onclick, _unclick;
    bool _OnMouseDown=false;
    float vel = 3f;

    //Custom/SpriteOutline
    //Sprites/Default

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            vel = 7f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            vel = 3f;
        }

        if(Input.GetKey(KeyCode.UpArrow))
        {
            playerRigidbody2D.velocity = new Vector2(0, vel);
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            playerRigidbody2D.velocity = new Vector2(0, -vel);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            playerRigidbody2D.velocity = new Vector2(-vel,0);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            playerRigidbody2D.velocity = new Vector2(vel,0);
        }
        else
        {
            playerRigidbody2D.velocity = Vector2.zero;
        }
    }

    

    void OnMouseDown()
    {
        _OnMouseDown=!_OnMouseDown;
        if(_OnMouseDown)
        {
            GetComponent<Renderer>().material = _onclick;
        }
        else
        {
            GetComponent<Renderer>().material = _unclick;
        }
        
    }




    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        //GetComponent<Renderer>().material = _onclick;
        _onclick = new Material(Shader.Find("Custom/SpriteOutline"));
        _unclick = new Material(Shader.Find("Sprites/Default"));
        GetComponent<Renderer>().material = _unclick;
        vel = 3f;

    } 

    void Update()
    {
        if(Input.GetKeyDown((KeyCode.Escape))){ SceneManager.LoadScene("MainMenu"); }
        Movement();
    }
}
