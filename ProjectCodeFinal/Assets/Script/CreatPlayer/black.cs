using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class black : MonoBehaviour
{
    // Start is called before the first frame update

    public SpriteRenderer BoySpriteRenderer;
    public SpriteRenderer GirlSpriteRenderer;
    Rigidbody2D rigid;

    void Start()
    {
        rigid = this.gameObject.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        Debug.Log("被點選");
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
        GirlSpriteRenderer.color = new Color(255, 255, 255, 1);
    }
}
