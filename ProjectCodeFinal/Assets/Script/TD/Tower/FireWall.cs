using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : Tower
{
    // Start is called before the first frame update
    private Transform[] enemyPosition;//場上敵人位置
    private bool[] atkNum;//距離內可攻擊的敵人數量
    [SerializeField] private float atkDistance;
    private float reAtkTime;
    [SerializeField] private GameObject atkPrefab;
    protected override void Start()
    {
        //setability(_maxHealth: 60, _atk: 5, _atkSpeed: 1, _def: 5, _fixDef: 5, _penetration: 0, _exp: 0, _spell: 3);
        atkDistance = 1f;
        reAtkTime = atkSpeed;
        type = Type.Tower;
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        atkSpeed -= Time.deltaTime;
        if (atkSpeed <= 0)
        {

            AtkEnemy();
            atkSpeed = reAtkTime;
        }
        


    }




    private void AtkEnemy()
    {
        
        for (int i = 0; i <= Spwaner.enemy.Length - 1; i++)
        {
            //

            if (Spwaner.enemy[i] == true && Vector2.Distance(Spwaner.enemy[i].transform.position, transform.position) <= atkDistance)
            {
                
                IDamageable idamageable = Spwaner.enemy[i].GetComponent<IDamageable>();
                idamageable.KeepTakenDamage(atk,3);

            }

        }
       


    }
}
