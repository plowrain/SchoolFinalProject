using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLV1 : Tower
{

    // Start is called before the first frame update

    [SerializeField] private GameObject atkPrefab;
    private bool[] atkNum;//距離內可攻擊的敵人數量
    [SerializeField] private float atkDistance;
    [SerializeField] private float reAtkTime;
    protected override void Start()
    {
        //setability(_maxHealth: 10, _atk: 5, _atkSpeed :2, _def:0, _fixDef:1, _penetration:0, _exp:0, _spell : 3) ;
        atkDistance = 5f;
        reAtkTime = atkSpeed;
        type = Type.Tower;
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        StartCoroutine(atkTimer());

    }



    IEnumerator atkTimer()
    {

        reAtkTime += Time.deltaTime;
        yield return 0;
        if (reAtkTime >= atkSpeed)
        {
            reAtkTime = 0;
            for (int i = 0; i <= Spwaner.enemy.Length - 1; i++)
            {
                //

                if (Spwaner.enemy[i] == true && Vector2.Distance(Spwaner.enemy[i].transform.position, transform.position) <= atkDistance)
                {
                    Vector2 pos = new Vector2(Spwaner.enemy[i].transform.position.x, Spwaner.enemy[i].transform.position.y);
                    GameObject _bullet = (GameObject)Instantiate(atkPrefab,pos,transform.rotation, gameObject.transform.parent);
                    _bullet.GetComponent<KeepDamBillet>().taget = Spwaner.enemy[i];
                    _bullet.transform.parent = gameObject.transform;
                    _bullet.GetComponent<KeepDamBillet>().atkDamage = atk;
                    
                    //atk.transform.position = Spwaner.enemy[i].transform.position;
                }

            }

        }



    }



}
