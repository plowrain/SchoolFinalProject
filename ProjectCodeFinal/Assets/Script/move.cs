using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public Vector2 a1,a2;
    public Vector3 a3;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.name != "Main Camera")
        {
            if (transform.position != new Vector3(a1.x, a1.y, 0))
                transform.position = Vector2.MoveTowards(transform.position, a1, speed * Time.deltaTime);
            else transform.position = a2;
        }
        else if (gameObject.transform.name == "Main Camera")
        {
            if (transform.position != a3)
            {
                transform.position = Vector3.MoveTowards(transform.position, a3, speed * Time.deltaTime);
                Debug.Log(Time.time);

            }


        }


    }

}
