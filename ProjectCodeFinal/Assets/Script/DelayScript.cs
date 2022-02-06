using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DelayScript : MonoBehaviour
{
    // Start is called before the first frame update

    public static IEnumerator DelayToInvokeDo(Action action, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        action();
    }

    /*
     StartCoroutine(DelayScript.DelayToInvokeDo(() =>
            {

                Fire(bullet_Prefab, bullet_Spawn);
            }, 2f)); 
            https://www.itread01.com/content/1547401338.html

    */

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
