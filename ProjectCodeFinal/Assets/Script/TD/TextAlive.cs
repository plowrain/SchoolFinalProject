using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAlive : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnEnable()
    {
        Start();
    }

    void Start()
    {
        StartCoroutine(SetActivefalse() );   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SetActivefalse()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
