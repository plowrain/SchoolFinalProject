using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightDam : MonoBehaviour
{
    public GameObject target;
    public GameObject[] towerTarget;

    // Start is called before the first frame update
    void Start()
    {
        

        target = GameObject.Find("生成塔");
        towerTarget = new GameObject[target.transform.childCount];
        for (int i=0;i<= target.transform.childCount -1;i++)
        {
            towerTarget[i] = target.transform.GetChild(i).gameObject;
        }

        for (int i=0; i<=towerTarget.Length - 1;i++)
        {
            if(Vector2.Distance(transform.position , towerTarget[i].transform.position ) <=2 )
            {
                IDamageable idamageable = towerTarget[i].GetComponent<IDamageable>();
                idamageable.KeepTakenDamage(2f,3f);
            }
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
