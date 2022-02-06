using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSoldier : Tower
{

    // Start is called before the first frame update
    private Transform[] enemyPosition;//場上敵人位置
    [SerializeField] private GameObject atkPrefab;
    private bool[] atkNum;//距離內可攻擊的敵人數量
    [SerializeField] private float atkDistance;
    [SerializeField] private float reAtkTime;
    [SerializeField] private float slowlySpeed;
    [SerializeField] private float slowlyTime;
    protected override void Start()
    {
        //setability(_maxHealth: 10, _atk: 5, _atkSpeed: 2, _def: 0, _fixDef: 1, _penetration: 0, _exp: 0, _spell: 3);
        atkDistance = 2f;
        reAtkTime = atkSpeed;
        type = Type.Tower;
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        reAtkTime -= Time.deltaTime;
        if (reAtkTime <= 0)
        {

            AtkEnemy();
            reAtkTime = atkSpeed;
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
                idamageable.TakenDamage(15, 0, "water", 0);
                ISkill iskill = Spwaner.enemy[i].GetComponent<ISkill>();
                iskill.ChangeSpeed(slowlySpeed, true,slowlyTime);

                
            }

        }



    }

    protected override void LevelUpAdd()
    {
        if (level == 5) slowlySpeed *= 1.3f;
        if (level == 8) slowlyTime += 0.5f;
    }

}
